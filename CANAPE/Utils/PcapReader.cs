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
using System.IO;
using System.Net;
using CANAPE.DataFrames;
using CANAPE.Nodes;
using CANAPE.Properties;

namespace CANAPE.Utils
{
    /// <summary>
    /// Simple class to read a PCAP file into a CANAPE log
    /// Does not currently reassemble TCP streams
    /// </summary>
    public static class PcapReader
    {
        private class EthernetPacket
        {
            public byte[] Dstmac { get; set; }
            public byte[] Srcmac { get; set; }
            public ushort PacketType { get; set; }
            public byte[] Data { get; set; }

            public const ushort ETHERNET_TYPE_VLAN = 0x8100;
            public const ushort ETHERNET_TYPE_IPv4 = 0x0800;
            public const ushort ETHERNET_TYPE_IPv6 = 0x86DD;
        }

        private class IpPacket
        {
            public IPAddress SrcAddress { get; set; }
            public IPAddress DestAddress { get; set; }
            public int IpType { get; set; }
            public int DataLength { get; set; }

            public const int IP_TYPE_UDP = 17;
            public const int IP_TYPE_TCP = 6;
            public const int IP_TYPE_ICMP = 1;
        }

        private class TcpPacket 
        {
            public int SrcPort { get; set; }
            public int DestPort { get; set; }
            public byte[] Data { get; set; }
            public long SequenceNumber { get; set; }
        }

        private class UdpPacket
        {
            public int SrcPort { get; set; }
            public int DestPort { get; set; }
            public byte[] Data { get; set; }
        }

        /// <summary>
        /// Class to compare TCP connections against sequence number for ordering
        /// </summary>
        private class TcpComparer : IComparer<TcpPacket>
        {
            public TcpComparer()
            {
            }

            #region IComparer<TcpPacket> Members

            public int Compare(TcpPacket x, TcpPacket y)
            {
                if (x.SequenceNumber == y.SequenceNumber)
                {
                    return 0;
                }
                else if (x.SequenceNumber < y.SequenceNumber)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }

            #endregion
        }

        /// <summary>
        /// Connection key class for use in the Dictionary
        /// </summary>
        private class Connection
        {
            /// <summary>
            /// Source address
            /// </summary>
            public IPAddress _srcAddress;
            /// <summary>
            /// Destination address
            /// </summary>
            public IPAddress _destAddress;
            /// <summary>
            /// Source port
            /// </summary>
            public int _srcPort;
            /// <summary>
            /// Destination port
            /// </summary>
            public int _destPort;
            /// <summary>
            /// UDP Port
            /// </summary>
            public bool _udp;

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="srcAddress">Source address</param>
            /// <param name="srcPort">Source port</param>
            /// <param name="destAddress">Destination address</param>
            /// <param name="destPort">Destination port</param>
            /// <param name="udp"></param>
            public Connection(IPAddress srcAddress, int srcPort, IPAddress destAddress, int destPort, bool udp)
            {
                _srcAddress = srcAddress;
                _srcPort = srcPort;
                _destAddress = destAddress;
                _destPort = destPort;
                _udp = udp;
            }

            /// <summary>
            /// Override for object equals
            /// </summary>
            /// <param name="obj">The object to compare against</param>
            /// <returns>True if equal</returns>
            public override bool Equals(object obj)
            {
                bool ret = false;

                if (obj is Connection)
                {
                    Connection right = obj as Connection;

                    ret = _srcAddress.Equals(right._srcAddress) && _destAddress.Equals(right._destAddress) &&
                        (_srcPort == right._srcPort) && (_destPort == right._destPort) && (_udp == right._udp);
                }

                return ret;
            }

            /// <summary>
            /// Override of GetHashCode
            /// </summary>
            /// <returns>The object's hash code</returns>
            public override int GetHashCode()
            {
                return String.Format("{0}:{1}/{2}:{3}/{4}",
                    _srcAddress.ToString(), _srcPort,
                    _destAddress.ToString(), _destPort, _udp).GetHashCode();
            }

            public override string ToString()
            {
                return String.Format("{0}:{1} -> {2}:{3}",
                    _srcAddress, _srcPort, _destAddress, _destPort);
            }
        }

        private static EthernetPacket ReadEthernetPacket(DataReader reader)
        {
            EthernetPacket packet = new EthernetPacket();

            try
            {                           
                packet.Dstmac = reader.ReadBytes(6);
                packet.Srcmac = reader.ReadBytes(6);
                packet.PacketType = reader.ReadUInt16(false);

                if (packet.PacketType == EthernetPacket.ETHERNET_TYPE_VLAN)
                {
                    reader.ReadUInt16(false);
                    packet.PacketType = reader.ReadUInt16(false);
                }
            }
            catch (EndOfStreamException)
            {
                packet = null;
            }

            return packet;
        }

        private static IpPacket ReadIpv4Packet(DataReader reader)
        {
            IpPacket packet = new IpPacket();
            
            int headerLength = (reader.ReadByte() & 0xF) * 4;
            reader.ReadByte();                                
            packet.DataLength = reader.ReadUInt16(false) - headerLength;
            reader.ReadUInt32(false); // Fragment/flags
            reader.ReadByte(); // TTL
            packet.IpType = reader.ReadByte();
            reader.ReadUInt16(false); // Checksum

            packet.SrcAddress = new IPAddress(reader.ReadBytes(4));
            packet.DestAddress = new IPAddress(reader.ReadBytes(4));

            reader.ReadBytes(headerLength - 20); // Read out rest of options

            return packet;
        }

        private static TcpPacket ReadTcpPacket(DataReader reader, int dataLength)
        {
            TcpPacket packet = new TcpPacket();

            packet.SrcPort = reader.ReadUInt16(false);
            packet.DestPort = reader.ReadUInt16(false);
            packet.SequenceNumber = reader.ReadUInt32(false); 
            reader.ReadUInt32(false); // Ack No
            int headerLength = (reader.ReadByte() >> 4) * 4;
            reader.ReadBytes(headerLength - 13); // Read out rest of header
            packet.Data = reader.ReadBytes(dataLength - headerLength); // Read data

            return packet;
        }

        private static UdpPacket ReadUdpPacket(DataReader reader)
        {
            UdpPacket packet = new UdpPacket();

            int dataLength = reader.ReadInt16(false) - 8;
            packet.SrcPort = reader.ReadUInt16(false);
            packet.DestPort = reader.ReadUInt16(false);
            reader.ReadUInt16(); // Checksum

            packet.Data = reader.ReadBytes(dataLength);

            return packet;
        }

        private static LogPacket ReadPacket(DateTime packetTime, Guid netId, byte[] data)
        {
            MemoryStream stm = new MemoryStream(data);
            DataReader reader = new DataReader(stm);
            LogPacket ret = null;

            try
            {
                EthernetPacket eth = ReadEthernetPacket(reader);
                if (eth.PacketType == EthernetPacket.ETHERNET_TYPE_IPv4)
                {
                    IpPacket ip = ReadIpv4Packet(reader);

                    if (ip.IpType == IpPacket.IP_TYPE_TCP)
                    {
                        TcpPacket tcp = ReadTcpPacket(reader, ip.DataLength);

                        if (tcp.Data.Length > 0)
                        {
                            ret = new LogPacket("TCP", netId, Guid.NewGuid(), String.Format("{0}:{1} => {2}:{3}",
                                ip.SrcAddress, tcp.SrcPort, ip.DestAddress, tcp.DestPort), new DataFrame(tcp.Data),
                                new ColorValue(0xFF, 0xFF, 0xFF, 0xFF), packetTime);
                        }                            
                    }
                    else if (ip.IpType == IpPacket.IP_TYPE_UDP)
                    {
                        UdpPacket udp = ReadUdpPacket(reader);

                        ret = new LogPacket("UDP", netId, Guid.NewGuid(), String.Format("{0}:{1} => {2}:{3}",
                            ip.SrcAddress, udp.SrcPort, ip.DestAddress, udp.DestPort), new DataFrame(udp.Data),
                            new ColorValue(0xFF, 0xFF, 0xFF, 0xFF), packetTime);
                    }
                    else
                    {
                        // Do nothing
                    }
                }
            }
            catch (EndOfStreamException)
            {
                ret = null;
            }
            catch (ArgumentException)
            {
            }

            return ret;
        }

        /// <summary>
        /// Load a list of packets from a PCAP (really basic mode)
        /// </summary>
        /// <param name="fileName">The file to read from</param>l
        /// <param name="raw">Whether to import the raw data or parse for TCP/UDP data</param>
        /// <returns>The array of LogPackets</returns>
        public static LogPacket[] Load(string fileName, bool raw)
        {
            List<LogPacket> packets = new List<LogPacket>();
            using(Stream stm = File.OpenRead(fileName))
            {
                DataReader reader = new DataReader(stm);
                bool littleEndian = true;

                uint magic = reader.ReadUInt32(littleEndian);

                if (magic == 0xa1b2c3d4)
                {
                    // OK
                }
                else if (magic == 0xd4c3b2a1)
                {
                    littleEndian = false;
                }
                else
                {
                    throw new ArgumentException(Resources.PcapReader_InvalidMagic);
                }

                reader.ReadUInt16(littleEndian); // Major
                reader.ReadUInt16(littleEndian); // Minor
                reader.ReadInt32(littleEndian); // Zone
                reader.ReadUInt32(littleEndian); // Sig figures
                reader.ReadUInt32(littleEndian); // Snap length
                uint netType = reader.ReadUInt32(littleEndian);

                if (!raw && netType != 1)
                {
                    throw new ArgumentException(Resources.PcapReader_OnlyEthernet);
                }

                try
                {
                    Guid netId = Guid.NewGuid();
                    while (reader.DataLeft > 0)
                    {
                        int secs = reader.ReadInt32(littleEndian);
                        int usecs = reader.ReadInt32(littleEndian);
                        DateTime captureTime = GeneralUtils.FromUnixTime(secs).AddMilliseconds(usecs / 10);
                        int caplen = reader.ReadInt32(littleEndian);
                        int origlen = reader.ReadInt32(littleEndian);
                        byte[] data = reader.ReadBytes(caplen);

                        if (raw)
                        {
                            packets.Add(new LogPacket("PCAP Raw", netId, Guid.NewGuid(), "Unknown",
                                new DataFrame(data), new ColorValue(0xFF, 0xFF, 0xFF, 0xFF),
                                captureTime));
                        }
                        else
                        {
                            LogPacket p = ReadPacket(captureTime, netId, data);

                            if (p != null)
                            {
                                packets.Add(p);
                            }
                        }
                    }
                }
                catch (EndOfStreamException)
                {
                }
            }

            return packets.ToArray();
        }
    }
}
