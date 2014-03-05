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
using System.IO;
using CANAPE.Nodes;

namespace CANAPE.Utils
{
    /// <summary>
    /// Class to log packets to a file
    /// </summary>
    public sealed class FilePacketLogger : IDisposable
    {
        Stream _stm;
        LogPacket[] _packets;
        int _currPos;

        const int MAX_PACKETS = 256;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stm">The stream to write to</param>
        public FilePacketLogger(Stream stm)
        {
            _stm = stm;
            _packets = new LogPacket[MAX_PACKETS];
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileName">The filename to write to</param>
        public FilePacketLogger(string fileName) : 
            this(File.Open(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
        {
        }

        /// <summary>
        /// Flush the current buffer to the stream
        /// </summary>
        public void Flush()
        {
            lock (_packets)
            {
                if (_currPos > 0)
                {
                    LogPacket[] arr = _packets;

                    if (_currPos < MAX_PACKETS)
                    {
                        arr = new LogPacket[_currPos];
                        Array.Copy(_packets, 0, arr, 0, _currPos);
                    }

                    GeneralUtils.SerializeLogPackets(arr, _stm);

                    Array.Clear(_packets, 0, _packets.Length);
                    _currPos = 0;
                }
            }
        }

        /// <summary>
        /// Add a packet to the buffer to write
        /// </summary>
        /// <param name="packet">The packet to write</param>
        public void AddPacket(LogPacket packet)
        {
            lock (_packets)
            {
                _packets[_currPos++] = packet;
                if (_currPos == MAX_PACKETS)
                {
                    Flush();
                }
            }
        }

        /// <summary>
        /// IDispose implementation
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);

            try
            {
                Flush();

                _stm.Close();
            }
            catch (IOException)
            {
            }
        }
    }
}
