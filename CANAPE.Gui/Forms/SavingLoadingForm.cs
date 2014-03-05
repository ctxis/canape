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
using System.ComponentModel;
using System.Windows.Forms;

namespace CANAPE.Forms
{
    internal partial class SavingLoadingForm : Form
    {
        string _fileName;
        bool _verifyVersion;
        bool _saving;

        public SavingLoadingForm(string fileName, bool saving, bool verifyVersion)
        {
            InitializeComponent();
            _fileName = fileName;
            _saving = saving;
            _verifyVersion = verifyVersion;
        }

        private void SavingLoadingForm_Load(object sender, EventArgs e)
        {
            if (_saving)
            {
                this.Text = String.Format(CANAPE.Properties.Resources.SavingLoadingForm_Saving, _fileName);
            }
            else
            {
                this.Text = String.Format(CANAPE.Properties.Resources.SavingLoadingForm_Loading, _fileName);
            }

            backgroundWorker.RunWorkerAsync();
        }

        void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Exception ret = null;

            try
            {
                if (_saving)
                {
                    CANAPE.Documents.CANAPEProject.Save(_fileName, 
                        Properties.Settings.Default.Compressed, Properties.Settings.Default.MakeBackup);
                }
                else
                {
                    CANAPE.Documents.CANAPEProject.Load(_fileName, _verifyVersion);
                }
            }
            catch (Exception ex)
            {
                ret = ex;
            }

            e.Result = ret;
        }

        public Exception Error { get; private set; }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {            
            if (e.Result != null) 
            {
                Error = (Exception)e.Result;          
                DialogResult = DialogResult.Cancel;
            }
            else
            {
                DialogResult = DialogResult.OK;
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (backgroundWorker.WorkerSupportsCancellation)
            {
                backgroundWorker.CancelAsync();
            }
        }
    }
}
