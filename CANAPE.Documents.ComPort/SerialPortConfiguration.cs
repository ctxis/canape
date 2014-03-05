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
using System.Collections.Generic;
using System.IO.Ports;
using CANAPE.Utils;

namespace CANAPE.Documents.ComPort
{
    [Serializable]
    public sealed class SerialPortConfiguration 
    {
        private string _port;
        private StopBits _stopbits;
        private int _databits;
        private int _baudrate;
        private Parity _parity;
        private Handshake _handshake;

        private IDocumentObject _parent;

        internal SerialPortConfiguration(string port, IDocumentObject parent)
        {
            _port = port;
            _parent = parent;
            _stopbits = StopBits.One;
            _parity = Parity.None;
            _handshake = Handshake.None;
            _databits = 8;
            _baudrate = 9600;
        }

        public SerialPortConfiguration Clone()
        {
            return GeneralUtils.CloneObject(this);
        }

        private void PropertyChanged<T>(ref T field, T value)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                _parent.Dirty = true;
            }
        }

        public string Port
        {
            get { return _port; }
            set { PropertyChanged(ref _port, value); }
        }

        public StopBits StopBits
        {
            get { return _stopbits; }
            set { PropertyChanged(ref _stopbits, value); }
        }

        public int DataBits
        {
            get { return _databits; }
            set { PropertyChanged(ref _databits, value); }
        }

        public int BaudRate
        {
            get { return _baudrate; }
            set { PropertyChanged(ref _baudrate, value); }
        }

        public Handshake FlowControl
        {
            get { return _handshake; }
            set { PropertyChanged(ref _handshake, value); }
        }

        public Parity Parity
        {
            get { return _parity; }
            set { PropertyChanged(ref _parity, value); }
        }

        internal SerialPort Create()
        {
            SerialPort ret = new SerialPort(_port, _baudrate, _parity, _databits, _stopbits);

            ret.Handshake = _handshake;

            ret.Open();

            return ret;
        }
    }
}
