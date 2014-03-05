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
using System.Net;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// A data value to store an IP address
    /// </summary>
    [Serializable]
    public class IPAddressDataValue : DataValue
    {
        private IPAddress _addr;

        /// <summary>
        /// Value property
        /// </summary>
        public override dynamic Value
        {
            get
            {
                return _addr;
            }
            set
            {
                if (value is IPAddress)
                {
                    IPAddress addr = value as IPAddress;

                    _addr = new IPAddress(addr.GetAddressBytes());
                }
                else if (value is string)
                {
                    _addr = IPAddress.Parse(value as string);
                }
                else
                {
                    _addr = new IPAddress((long)Convert.ChangeType(value, typeof(long)));
                }
            }
        }

        /// <summary>
        /// Whether this is a fixed length, always returns true
        /// </summary>
        public override bool FixedLength
        {
            get { return true; }
        }

        /// <summary>
        /// Convert address to bytes
        /// </summary>
        /// <returns>The bytes</returns>
        public override byte[] ToArray()
        {
            return _addr.GetAddressBytes();
        }

        /// <summary>
        /// Convert an address from bytes
        /// </summary>
        /// <param name="data">Data should be 4 bytes for IPv4 or 16 for IPv6</param>
        public override void FromArray(byte[] data)
        {
            _addr = new IPAddress(data);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        public IPAddressDataValue(string name, byte[] data)
            : base(name)
        {
            _addr = new IPAddress(data);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="addr"></param>
        public IPAddressDataValue(string name, IPAddress addr)
            : this(name, addr.GetAddressBytes())
        {
            
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name"></param>        
        public IPAddressDataValue(string name)
            : base(name)
        {
            _addr = IPAddress.Any;
        }
    }
}
