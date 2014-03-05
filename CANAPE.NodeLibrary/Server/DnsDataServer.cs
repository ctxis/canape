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
using System.ComponentModel;
using System.Net;
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.Net.Protocols.Parser;
using CANAPE.NodeLibrary.Parser;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Server
{
    [Serializable]
    public class DnsDataServerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        [LocalizedDescription("DnsDataServerConfig_ResponseAddressDescription", typeof(Properties.Resources)), TypeConverter(typeof(IPAddressConverter))]
        public IPAddress ResponseAddress { get; set; }

        [LocalizedDescription("DnsDataServerConfig_ResponseAddress6Description", typeof(Properties.Resources)), TypeConverter(typeof(IPAddressConverter))]
        public IPAddress ResponseAddress6 { get; set; }

        [LocalizedDescription("DnsDataServerConfig_ReverseDnsDescription", typeof(Properties.Resources))]
        public string ReverseDns { get; set; }

        [LocalizedDescription("DnsDataServerConfig_TimeToLiveDescription", typeof(Properties.Resources))]
        public uint TimeToLive { get; set; }

        public DnsDataServerConfig()
        {
            ResponseAddress = IPAddress.Loopback;
            ResponseAddress6 = IPAddress.IPv6Loopback;
            ReverseDns = String.Empty;
            TimeToLive = 1000;
        }
    }

    [NodeLibraryClass("DnsDataServer", typeof(Properties.Resources),        
        Category = NodeLibraryClassCategory.Server,
        ConfigType = typeof(DnsDataServerConfig))]
    public class DnsDataServer : BasePersistDataEndpoint<DnsDataServerConfig>
    {
        public override void Run(IDataAdapter adapter, Logger logger)
        {
            DataFrame frame = adapter.Read();

            while (frame != null)
            {
                try
                {
                    DNSPacket packet = DNSPacket.FromArray(frame.ToArray());
                    List<DNSPacket.DNSRRBase> aRecords = new List<DNSPacket.DNSRRBase>();

                    foreach (DNSPacket.DNSQuestion question in packet.Questions)
                    {
                        if ((question.QClass == DNSPacket.DNSClass.IN) || (question.QClass == DNSPacket.DNSClass.AnyClass))
                        {
                            if ((question.QType == DNSPacket.DNSType.A) || (question.QType == DNSPacket.DNSType.AllRecords))
                            {
                                if (Config.ResponseAddress != IPAddress.Any)
                                {
                                    DNSPacket.ADNSRR addr = new DNSPacket.ADNSRR();

                                    addr.Address = new IPAddress(Config.ResponseAddress.GetAddressBytes());

                                    addr.TimeToLive = Config.TimeToLive;
                                    addr.Type = DNSPacket.DNSType.A;
                                    addr.Class = DNSPacket.DNSClass.IN;
                                    addr.Name = question.QName;

                                    aRecords.Add(addr);
                                }
                            }

                            if ((question.QType == DNSPacket.DNSType.AAAA) || (question.QType == DNSPacket.DNSType.AllRecords))
                            {
                                if (Config.ResponseAddress6 != IPAddress.IPv6Any)
                                {
                                    DNSPacket.AAAADNSRR addr = new DNSPacket.AAAADNSRR();

                                    addr.Address = new IPAddress(Config.ResponseAddress6.GetAddressBytes());

                                    addr.TimeToLive = Config.TimeToLive;
                                    addr.Type = DNSPacket.DNSType.AAAA;
                                    addr.Class = DNSPacket.DNSClass.IN;
                                    addr.Name = question.QName;

                                    aRecords.Add(addr);
                                }
                            }

                            if ((question.QType == DNSPacket.DNSType.PTR) || (question.QType == DNSPacket.DNSType.AllRecords))
                            {
                                if (!String.IsNullOrEmpty(Config.ReverseDns) && ((question.QName.EndsWith(".in-addr.arpa.") || question.QName.EndsWith(".ip6.arpa."))))
                                {
                                    DNSPacket.PTRDNSRR addr = new DNSPacket.PTRDNSRR();

                                    addr.Ptr = Config.ReverseDns;
                                    addr.Type = DNSPacket.DNSType.PTR;
                                    addr.Class = DNSPacket.DNSClass.IN;
                                    addr.Name = question.QName;

                                    aRecords.Add(addr);
                                }
                            }
                        }
                    }

                    packet.Query = true;
                    packet.RecursionAvailable = true;
                    if (aRecords.Count > 0)
                    {
                        packet.Answers = aRecords.ToArray();
                        packet.AuthoritiveAnswer = true;
                        packet.ResponseCode = DNSPacket.DNSRCode.NoError;
                    }
                    else
                    {
                        packet.ResponseCode = DNSPacket.DNSRCode.Refused;
                    }

                    adapter.Write(new DataFrame(packet.ToArray()));
                }
                catch (ArgumentException ex)
                {
                    logger.LogException(ex);
                }

                frame = adapter.Read();
            }
        }

        protected override void  ValidateConfig(DnsDataServerConfig config)
        {
            if (config.ResponseAddress.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
            {
                throw new ArgumentException("ResponseAddress must be an IPv4 address");
            }

            if (config.ResponseAddress6.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
            {
                throw new ArgumentException("ResponseAddress6 must be an IPv6 address");
            }
        }

        public override string Description
        {
            get { return "DNS Server"; }
        }
    }
}
