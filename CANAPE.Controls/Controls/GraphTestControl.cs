//    CANAPE Network Testing Tool
//    Copyright (C) 2014 Context Information Security
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using CANAPE.DataAdapters;
using CANAPE.Documents.Data;
using CANAPE.Forms;
using CANAPE.Nodes;
using CANAPE.Utils;

namespace CANAPE.Controls
{
    public partial class GraphTestControl : UserControl
    {
        private const string PACKETLOGINPUT_CONFIG = "PacketLogControlInputConfig";
        private const string PACKETLOGOUTPUT_CONFIG = "PacketLogControlOutputConfig";

        TestDocument _document;

        public GraphTestControl()
        {                     
            InitializeComponent();
            eventLogControl.Logger.LogLevel = Logger.LogEntryType.All;
            if (components == null)
            {
                components = new Container();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TestDocument Document 
        {
            get { return _document; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                _document = value;

                PacketLogControlConfig config = _document.GetProperty(PACKETLOGINPUT_CONFIG) as PacketLogControlConfig;

                if (config != null)
                {
                    logPacketControlInput.Config = config;
                }

                config = _document.GetProperty(PACKETLOGOUTPUT_CONFIG) as PacketLogControlConfig;

                if (config != null)
                {
                    logPacketControlOutput.Config = config;
                }

                logPacketControlInput.SetPackets(_document.InputPackets);
                logPacketControlOutput.SetPackets(_document.OutputPackets);
            }
        }

        class EventDataAdapter : IDataAdapter
        {
            EventWaitHandle _eventHandle;

            public EventDataAdapter(EventWaitHandle eventHandle)
            {
                _eventHandle = eventHandle;
            }

            #region IDataAdapter Members

            public DataFrames.DataFrame Read()
            {
                throw new NotImplementedException();
            }

            public void Write(DataFrames.DataFrame frame)
            {
                if (frame == null)
                {
                    _eventHandle.Set();
                }
            }

            public void Close()
            {
                _eventHandle.Set();
            }

            public string Description
            {
                get { return "Wait Adapter"; }
            }

            /// <summary>
            /// Overriden ToString
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                return Description;
            }

            /// <summary>
            /// Reconnect the data adapter
            /// </summary>
            /// <exception cref="NotSupportedException">Always thrown</exception>
            public virtual void Reconnect()
            {
                throw new NotSupportedException();
            }

            public int ReadTimeout
            {
                get { throw new InvalidOperationException(); }
                set { throw new InvalidOperationException(); }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                _eventHandle.Set();
            }

            #endregion


            public bool CanTimeout
            {
                get { return false; }
            }
        }        

        private void RunTest()
        {          
            Logger logger = eventLogControl.Logger; 
            MetaDictionary globals = new MetaDictionary();    
            EventWaitHandle endEvent = new EventWaitHandle(false, EventResetMode.ManualReset);

            try
            {
                CancellationTokenSource tokenSource = new CancellationTokenSource();
                using (TestDocument.TestGraphContainer testGraph = Document.CreateTestGraph(logger, globals))
                {
                    testGraph.Graph.LogPacketEvent += new EventHandler<LogPacketEventArgs>(log_AddLogPacket);
                    testGraph.Graph.EditPacketEvent += new EventHandler<EditPacketEventArgs>(Graph_EditPacketEvent);

                    LogPacket[] packets = Document.GetInputPackets();

                    using (QueuedDataAdapter inputAdapter = new QueuedDataAdapter(tokenSource.Token))
                    {
                        foreach (LogPacket p in packets)
                        {
                            inputAdapter.Enqueue(p.Frame.CloneFrame());
                        }

                        inputAdapter.StopEnqueue();

                        testGraph.Graph.BindEndpoint(testGraph.Output.Uuid, new EventDataAdapter(endEvent));
                        testGraph.Graph.BindEndpoint(testGraph.Input.Uuid, inputAdapter);

                        IPipelineEndpoint inputEndpoint = testGraph.Input as IPipelineEndpoint;
                        inputEndpoint.Start();

                        // Sleep up to 100 seconds
                        if (!endEvent.WaitOne(100000))
                        {
                            logger.LogError("Test did not finish after 100 seconds");
                            tokenSource.Cancel();
                        }
                    }
                }
            }
            catch (EndOfStreamException)
            {
                // End of stream
            }
            catch (ThreadAbortException)
            {
            }
            catch (ObjectDisposedException)
            {
            }
            finally
            {
                if(endEvent != null)
                {
                    endEvent.Dispose();
                }
            }
        }

        void Graph_EditPacketEvent(object sender, EditPacketEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<EditPacketEventArgs>(Graph_EditPacketEvent), sender, e);
            }
            else
            {
                using (EditPacketForm frm = new EditPacketForm())
                {
                    frm.Frame = e.Frame;
                    frm.Selector = e.SelectPath;
                    frm.DisplayColor = ColorValueConverter.ToColor(e.Color);
                    frm.DisplayTag = e.Tag;
                    frm.ShowDisableEditor = true;
                    frm.ShowReadOnly = true;

                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        e.Frame = frm.Frame;
                    }

                    if (frm.DisableEditor)
                    {
                        if (e.Sender != null)
                        {
                            e.Sender.Enabled = false;
                        }
                    }
                }
            }
        }

        void log_AddLogPacket(object sender, LogPacketEventArgs e)
        {
            logPacketControlOutput.AddLogEntry(new LogPacket(e));
        }

        public void Run()
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);

            worker.RunWorkerAsync();

            components.Add(worker);
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (InvokeRequired)
            {                
                Invoke(new EventHandler<RunWorkerCompletedEventArgs>(worker_RunWorkerCompleted), sender, e);
            }
            else
            {
                Exception ex = e.Result as Exception;

                if(ex != null)
                {
                    MessageBox.Show(this, ex.Message, CANAPE.Properties.Resources.MessageBox_ErrorString, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                RunTest();
            }
            catch (Exception ex)
            {
                e.Result = ex;
            }
        }

        private void logPacketControlInput_ConfigChanged(object sender, EventArgs e)
        {
            if (_document != null)
            {
                _document.SetProperty(PACKETLOGINPUT_CONFIG, logPacketControlInput.Config);
            }
        }

        private void logPacketControlOutput_ConfigChanged(object sender, EventArgs e)
        {
            if (_document != null)
            {
                _document.SetProperty(PACKETLOGOUTPUT_CONFIG, logPacketControlOutput.Config);
            }
        }
    }
}
