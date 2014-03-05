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
using System.Text;
using CANAPE.Nodes;

namespace CANAPE.Utils
{
    /// <summary>
    /// Utilities for the console 
    /// </summary>
    public static class ConsoleUtils
    {
        /// <summary>
        /// Get a logger which logs to the console
        /// </summary>
        /// <returns>The new logger</returns>
        public static Logger GetLogger()
        {
            Logger ret = new Logger();

            ret.LogEntryAdded += ret_LogEntryAdded;

            return ret;
        }

        static void ret_LogEntryAdded(object sender, Logger.LogEntryAddedEventArgs e)
        {
            TextWriter writer = null;

            switch (e.LogEntry.EntryType)
            {
                case Logger.LogEntryType.Info:
                    writer = Console.Out;
                    break;
                default:
                    writer = Console.Error;
                    break;
            }

            string text = e.LogEntry.Text;
            if (e.LogEntry.ExceptionObject != null)
            {
                text = e.LogEntry.ExceptionObject.ToString();
            }

            writer.WriteLine("{0} {1}: {2}", e.LogEntry.Timestamp, e.LogEntry.SourceName, text);
            
        }

        /// <summary>
        /// Get a logger which logs to the console verbose logs
        /// </summary>
        /// <returns>The new logger</returns>
        public static Logger GetVerboseLogger()
        {
            Logger l = GetLogger();

            l.LogLevel = Logger.LogEntryType.All;

            return l;
        }

        private static void WritePacketsHex(Stream stm, LogPacket[] ps)
        {
            using (TextWriter writer = new StreamWriter(stm, Encoding.UTF8))
            {
                foreach (LogPacket p in ps)
                {
                    writer.WriteLine("Time {0} - Tag '{1}' - Network '{2}'",
                        p.Timestamp.ToString(), p.Tag, p.Network);

                    writer.WriteLine(GeneralUtils.BuildHexDump(16, p.Frame.ToArray()));
                    writer.WriteLine();
                }
            }
        }

        private static void WritePacketsText(Stream stm, LogPacket[] ps)
        {
            using (TextWriter writer = new StreamWriter(stm, Encoding.UTF8))
            {
                foreach (LogPacket p in ps)
                {
                    writer.WriteLine("Time {0} - Tag '{1}' - Network '{2}'",
                    p.Timestamp.ToString(), p.Tag, p.Network);
                    writer.WriteLine(GeneralUtils.MakeByteString(p.Frame.ToArray()));
                }
            }
        }

        /// <summary>
        /// Dump a binary packet to stdout
        /// </summary>
        /// <param name="packet">The packet to dump</param>
        public static void DumpBinaryPacket(LogPacket packet)
        {
            Console.Out.WriteLine(ConvertBinaryPacketToString(packet));
        }

        /// <summary>
        /// Dump a text packet to stdout
        /// </summary>
        /// <param name="packet">The packet to dump</param>
        public static void DumpTextPacket(LogPacket packet)
        {
            Console.Out.WriteLine(ConvertTextPacketToString(packet));
        }

        /// <summary>
        /// Dump a list of binary packets to stdout
        /// </summary>
        /// <param name="packets">The packets to dump</param>
        public static void DumpBinaryPackets(IEnumerable<LogPacket> packets)
        {
            foreach (LogPacket p in packets)
            {
                DumpBinaryPacket(p);
            }
        }

        /// <summary>
        /// Dump a list of text packets to stdout
        /// </summary>
        /// <param name="packets">The packets to dump</param>
        public static void DumpTextPackets(IEnumerable<LogPacket> packets)
        {
            foreach (LogPacket p in packets)
            {
                DumpTextPacket(p);
            }
        }

        /// <summary>
        /// Convert a packet to a hex string format
        /// </summary>
        /// <param name="p">The packet to convert</param>
        /// <returns>The converted string</returns>
        public static string ConvertBinaryPacketToString(LogPacket p)
        {
            using (TextWriter writer = new StringWriter())
            {                
                writer.WriteLine("Time {0} - Tag '{1}' - Network '{2}'",
                    p.Timestamp.ToString(), p.Tag, p.Network);

                writer.WriteLine(GeneralUtils.BuildHexDump(16, p.Frame.ToArray()));
                writer.WriteLine();             

                return writer.ToString();
            }
        }

        /// <summary>
        /// Convert a packet to a text string format
        /// </summary>
        /// <param name="p">The packet to convert</param>
        /// <returns>The converted string</returns>
        public static string ConvertTextPacketToString(LogPacket p)
        {
            using (TextWriter writer = new StringWriter())
            {
                writer.WriteLine("Time {0} - Tag '{1}' - Network '{2}'",
                p.Timestamp.ToString(), p.Tag, p.Network);
                writer.WriteLine(p.Frame.ToDataString());

                return writer.ToString();
            }
        }
    }
}
