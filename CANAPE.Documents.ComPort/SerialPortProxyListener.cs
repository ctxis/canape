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
using System.IO.Ports;
using System.Threading.Tasks;
using CANAPE.Net.Listeners;

namespace CANAPE.Documents.ComPort
{
    class SerialPortProxyListener : INetworkListener
    {
        SerialPortConfiguration _config;
        SerialPort _port;
        Task _task;

        internal SerialPortProxyListener(SerialPortConfiguration config)
        {
            _config = config;
        }

        private void CreateInstance()
        {
            EventHandler<ClientConnectedEventArgs> clientConnected = ClientConnected;
            if (clientConnected != null)
            {
                SerialPort port = _port;

                if (port != null)
                {
                    ClientConnectedEventArgs e = new ClientConnectedEventArgs(new SerialPortDataAdapter(port));

                    clientConnected(this, e);
                }
            }
        }

        public void Start()
        {
            if (_port == null)
            {
                _port = _config.Create();
                _task = Task.Factory.StartNew(CreateInstance);
            }
        }

        public void Stop()
        {
            if (_task != null)
            {
                _task.Dispose();
                _task = null;
            }

            if (_port != null)
            {
                _port.Dispose();
                _port = null;
            }            
        }

        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public void Dispose()
        {
            try
            {
                Stop();
            }
            catch
            {
            }
        }
    }
}
