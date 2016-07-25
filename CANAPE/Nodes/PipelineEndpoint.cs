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
using System.Diagnostics;
using System.Text;
using System.Threading;
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.Utils;

namespace CANAPE.Nodes
{
    /// <summary>
    /// A pipeline node which acts as a data adapter endpoint
    /// </summary>
    public class PipelineEndpoint : BasePipelineNode, IPipelineEndpoint
    {
        IDataAdapter _adapter;
        bool _isDisposed;
        Thread _thread;

        /// <summary>
        /// Default constructor
        /// </summary>
        public PipelineEndpoint()
        {
            _noWriteOutput = true;
        }        

        /// <summary>
        /// Allows the reading of frames to be overriden
        /// </summary>
        /// <returns>The read frame</returns>
        protected virtual DataFrame ReadDataFrame()
        {
            DataFrame ret = null;
            bool done = true;

            do
            {
                IDataAdapter adapter = _adapter;

                done = true;

                if (_adapter != null)
                {
                    ret = _adapter.Read();

                    if (ret == null)
                    {
                        if (!Interlocked.ReferenceEquals(_adapter, adapter))
                        {
                            done = false;
                        }
                    }
                }                
            }
            while (!done);

            return ret;
        }

        /// <summary>
        /// Shutdown the outputs of the node without actually going through the normal channels
        /// </summary>
        public new void ShutdownOutputs()
        {
            base.ShutdownOutputs();
        }

        private void WriteFrame(DataFrame frame)
        {
            if (DataRecieved != null)
            {
                DataRecieved.Invoke(this, new EventArgs());
            }

            WriteOutput(frame);
        }


        private void ReadThread()
        {
            try
            {
                bool done = false;

                do
                {
                    DataFrame frame = ReadDataFrame();                   

                    if(frame != null)
                    {
                        WriteFrame(frame);
                    }
                    else
                    {
                        done = true;
                    }
                }
                while (!done);
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                LogException(ex);
            }

            if (!_isDisposed)
            {
                ShutdownOutputs();
            }
        }

        /// <summary>
        /// Method called when new input is received
        /// </summary>
        /// <param name="frame"></param>
        protected override void OnInput(DataFrame frame)
        {
            try
            {
                IDataAdapter adapter = _adapter;

                if (adapter != null)
                {
                    adapter.Write(frame);
                }
            }
            catch (ObjectDisposedException)
            {
            }
            catch (ThreadAbortException)
            {
                throw;
            }                
            catch (Exception ex)
            {
                LogException(ex);
            }
        }

        /// <summary>
        /// Start the endpoint running
        /// </summary>
        public virtual void Start()
        {
            if (_thread == null)
            {
                if (_adapter != null)
                {
                    _thread = new Thread(ReadThread);
                    _thread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
                    _thread.Name = String.Format("Endpoint {0} Thread", Name);
                    _thread.IsBackground = true;
                    _thread.Start();
                }
            }
        }

        /// <summary>
        /// Stop the endpoint running
        /// </summary>
        public virtual void Stop()
        {
            if (_thread != null)
            {
                if (_thread.IsAlive)
                {
                    GeneralUtils.AbortThread(_thread);
                    _thread.Join(1000);
                }
                _thread = null;
            }
        }

        /// <summary>
        /// Function called when the the adapter is being shutdown
        /// </summary>
        protected override bool OnShutdown()
        {
            bool shutdown = true;

            try
            {
                if (_adapter != null)
                {
                    _adapter.Close();
                    _adapter = null;

                    // Shutdown if no thread to pick it up
                    if (_thread != null)
                    {
                        shutdown = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.GetSystemLogger().LogException(ex);
            }
            
            return shutdown;
        }

        /// <summary>
        /// Called to dispose the object
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (!_isDisposed)
            {
                _isDisposed = true;

                try
                {
                    if (_adapter != null)
                    {
                        _adapter.Dispose();
                    }
                }
                catch
                {                   
                }                
                
                _thread = null;
            }
        }

        /// <summary>
        /// Get or set adapter
        /// </summary>
        public IDataAdapter Adapter
        {
            get
            {
                return _adapter;
            }
            set
            {
                _adapter = value;
            }
        }

        /// <summary>
        /// Event signaled when data recieved
        /// </summary>
        public event EventHandler DataRecieved;
    }
}
