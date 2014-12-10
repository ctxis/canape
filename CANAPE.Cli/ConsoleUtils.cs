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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using CANAPE.DataFrames;
using CANAPE.Net;
using CANAPE.Nodes;
using IronPython.Runtime;

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
        /// <param name="file">A python file object to use for output</param>
        /// <returns>The new logger</returns>
        public static Logger GetLogger(PythonFile file)
        {
            Logger ret = new Logger();

            ret.LogEntryAdded += (sender, e) =>
                ret_LogEntryAdded(file, sender, e);

            return ret;
        }

        static void ret_LogEntryAdded(PythonFile file, object sender, Logger.LogEntryAddedEventArgs e)
        {                      
            string text = e.LogEntry.Text;
            if (e.LogEntry.ExceptionObject != null)
            {
                text = e.LogEntry.ExceptionObject.ToString();
            }

            file.write(String.Format("[{0}] {1} {2}: {3}\n", e.LogEntry.EntryType, e.LogEntry.Timestamp, e.LogEntry.SourceName, text));
        }

        /// <summary>
        /// Get a logger which logs to the console verbose logs
        /// </summary>
        /// <param name="file">A python file object to use for output</param>
        /// <returns>The new logger</returns>
        public static Logger GetVerboseLogger(PythonFile file)
        {
            Logger l = GetLogger(file);

            l.LogLevel = Logger.LogEntryType.All;

            return l;
        }        

        private static void WritePacketsHex(Stream stm, LogPacket[] ps)
        {
            using (TextWriter writer = new StreamWriter(stm, Encoding.UTF8))
            {
                foreach (LogPacket p in ps)
                {
                    writer.WriteLine(GetHeader(p));
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
                    writer.WriteLine(GetHeader(p));
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

        private static string GetHeader(LogPacket p)
        {
            return String.Format("Time {0} - Tag '{1}' - Network '{2}'",
                    p.Timestamp.ToString(), p.Tag, p.Network);
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
                writer.WriteLine(GetHeader(p));
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
                writer.WriteLine(GetHeader(p));
                writer.Write(p.Frame.ToDataString());

                return writer.ToString();
            }
        }

        /// <summary>
        /// Convert a packet to a hex string format
        /// </summary>
        /// <param name="p">The packet to convert</param>
        /// <returns>The converted string</returns>
        public static string ConvertBinaryPacketToString(DataFrame p)
        {
            using (TextWriter writer = new StringWriter())
            {                                
                writer.WriteLine(GeneralUtils.BuildHexDump(16, p.ToArray()));
                writer.WriteLine();

                return writer.ToString();
            }
        }

        /// <summary>
        /// Convert a packet to a text string format
        /// </summary>
        /// <param name="p">The packet to convert</param>
        /// <returns>The converted string</returns>
        public static string ConvertTextPacketToString(DataFrame p)
        {
            using (TextWriter writer = new StringWriter())
            {                
                writer.WriteLine(p.ToDataString());

                return writer.ToString();
            }
        }

        private static void WriteNodeToTreeString(TextWriter writer, string link, DataNode node)
        {
            DataKey key = node as DataKey;            

            if (key != null)
            {       
                string towrite = "+ " + key.Name;
                writer.WriteLine("{0}{1}", link, towrite);                           

                foreach (DataNode n in key.SubNodes)
                {
                    WriteNodeToTreeString(writer, new String(' ', link.Length) + "|" + new String('-', towrite.Length - 1), n);
                }
            }
            else
            {
                if (node.Value is byte[])
                {
                    string hexdump = GeneralUtils.BuildHexDump(16, node.Value);
                    writer.WriteLine("{0}> {1} = ", link, node.Name);
                    writer.WriteLine(hexdump);
                }
                else
                {
                    writer.WriteLine("{0}> {1} = {2}", link, node.Name, node.Value);
                }
            }
        }

        private static string ConvertPacketToTreeString(DataNode p)
        {
            using (TextWriter writer = new StringWriter())
            {
                WriteNodeToTreeString(writer, "", p);
                return writer.ToString();
            }
        }        

        public static string ConvertPacketToTreeString(DataFrame p)
        {
            return ConvertPacketToTreeString(p.Root);
        }

        public static string ConvertPacketToTreeString(LogPacket p)
        {
            return ConvertPacketToTreeString(new LogPacket[] { p });
        }

        public static string ConvertPacketToTreeString(IEnumerable ps)
        {
            using (TextWriter writer = new StringWriter())
            {
                int count = 0;

                foreach (object o in ps)
                {
                    LogPacket p = o as LogPacket;
                    DataFrame f = o as DataFrame;

                    if (p != null)
                    {
                        writer.WriteLine(GetHeader(p));
                        WriteNodeToTreeString(writer, "", p.Frame.Root);
                    }
                    else if(f != null)
                    {
                        writer.WriteLine("Packet {0}", count);
                        WriteNodeToTreeString(writer, "", f.Root);
                    }
                    count++;
                }

                return writer.ToString();
            }
        }

        public static string ConvertPacketToString(DataFrame p)
        {
            if (p.IsBasic)
            {
                return ConvertBinaryPacketToString(p);
            }
            else if (p.IsDataString)
            {
                return ConvertTextPacketToString(p);
            }
            else
            {
                return ConvertPacketToTreeString(p);
            }
        }

        public static string ConvertPacketToString(LogPacket p)
        {
            using (TextWriter writer = new StringWriter())
            {
                writer.WriteLine(GetHeader(p));
                writer.WriteLine(ConvertPacketToString(p.Frame));
                return writer.ToString();
            }
        }

        public static string ConvertPacketToString(IEnumerable ps)
        {
            using (TextWriter writer = new StringWriter())
            {
                int count = 0;

                foreach (object o in ps)
                {
                    LogPacket p = o as LogPacket;
                    DataFrame f = o as DataFrame;

                    if (p != null)
                    {
                        writer.Write(ConvertPacketToString(p));
                    }
                    else if(f != null)
                    {
                        writer.WriteLine("Packet {0}", count);
                        writer.Write(ConvertPacketToString(f));
                    }
                    count++;
                }

                return writer.ToString();
            }
        }



        private static void WriteBinaryPacketAsHtml(XmlWriter writer, DataFrame frame, ColorValue c)
        {
            writer.WriteStartElement("pre");
            writer.WriteAttributeString("style", String.Format("background-color:#{0:X02}{1:X02}{2:X02}", c.R, c.G, c.B));
            writer.WriteString(GeneralUtils.BuildHexDump(16, frame.ToArray()));
            writer.WriteEndElement();
        }

        private static void WriteTextPacketAsHtml(XmlWriter writer, DataFrame frame, ColorValue c)
        {
            writer.WriteStartElement("pre");
            writer.WriteAttributeString("style", String.Format("background-color:#{0:X02}{1:X02}{2:X02}", c.R, c.G, c.B));
            writer.WriteString(frame.ToDataString());
            writer.WriteEndElement();
        }

        private static void WriteTreeNodeAsHtml(XmlWriter writer, DataNode node)
        {
            writer.WriteStartElement("li");

            DataKey key = node as DataKey;            

            if (key != null)
            {       
                string towrite = String.Format("{0} +", key.Name);
                writer.WriteElementString("span", towrite);

                foreach (DataNode n in key.SubNodes)
                {                    
                    writer.WriteStartElement("ul");
                    WriteTreeNodeAsHtml(writer, n);
                    writer.WriteEndElement();
                }
            }
            else
            {
                if (node.Value is byte[])
                {
                    string hexdump = GeneralUtils.BuildHexDump(16, node.Value);

                    writer.WriteElementString("span", node.Name + " = ");
                    writer.WriteElementString("pre", hexdump);
                }
                else
                {
                    writer.WriteElementString("span", String.Format("{0} = {1}", node.Name, node.Value));                    
                }
            }

            writer.WriteEndElement();
        }

        private static void WriteTreePacketAsHtml(XmlWriter writer, DataFrame frame, ColorValue c)
        {
            writer.WriteStartElement("ul");
            writer.WriteAttributeString("style", String.Format("background-color:#{0:X02}{1:X02}{2:X02}", c.R, c.G, c.B));
            WriteTreeNodeAsHtml(writer, frame.Root);
            writer.WriteEndElement();
        }

        private static void ConvertPacketsToHtml(TextWriter textWriter, IEnumerable ps, bool forcebin)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = true;
            
            using (XmlWriter writer = XmlWriter.Create(textWriter,settings))
            {
                int count = 0;

                writer.WriteStartElement("html");
                writer.WriteStartElement("head");
                writer.WriteElementString("title", "Packet Log");             
                writer.WriteEndElement();                
                writer.WriteStartElement("body");

                foreach (object o in ps)
                {
                    LogPacket p = o as LogPacket;
                    DataFrame f = o as DataFrame;
                    ColorValue c = ColorValue.White;
                    if (p != null)
                    {
                        writer.WriteElementString("h2", String.Format("Time {0} - Tag '{1}' - Network '{2}'",
                            p.Timestamp.ToString(), p.Tag, p.Network));
                        f = p.Frame;
                        c = p.Color;
                    }
                    else
                    {
                        writer.WriteElementString("h2", String.Format("Packet {0}", count));
                    }
                    count++;

                    if (f.IsBasic || forcebin)
                    {
                        WriteBinaryPacketAsHtml(writer, f, c);
                    }
                    else if (f.IsDataString)
                    {
                        WriteTextPacketAsHtml(writer, f, c);
                    }
                    else
                    {
                        WriteTreePacketAsHtml(writer, f, c);
                    }
                }

                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        public static string ConvertPacketsToHtml(IEnumerable ps)
        {   
            using (TextWriter textWriter = new StringWriter())
            {
                ConvertPacketsToHtml(textWriter, ps, false);

                return textWriter.ToString();
            }
        }

        public static string ConvertPacketsToHtml(DataFrame f)
        {
            return ConvertPacketsToHtml(new DataFrame[] { f });
        }

        public static string ConvertPacketsToHtml(LogPacket p)
        {
            return ConvertPacketsToHtml(new LogPacket[] { p });
        }

        public static byte[] ConvertListToByteArray(IEnumerable list)
        {
            MemoryStream stm = new MemoryStream();

            foreach (object o in list)
            {
                byte b = Convert.ToByte(o);

                stm.WriteByte(b);
            }

            return stm.ToArray();
        }

        public static List<LogPacket> EnumerableToPackets(IEnumerable ps)
        {
            List<LogPacket> packets = new List<LogPacket>();

            foreach(object o in ps)
            {
                LogPacket p = o as LogPacket;
                if(p == null)
                {
                    DataFrame f = o as DataFrame;
                    if(f == null)
                    {
                        continue;
                    }
                    p = new LogPacket("Unknown", Guid.Empty, "Unknown", f, ColorValue.White);
                }
                packets.Add(p);
            }

            return packets;
        }

        public static void SavePackets(string filename, IEnumerable ps)
        {
            List<LogPacket> packets = new List<LogPacket>();

            foreach(object o in ps)
            {
                LogPacket p = o as LogPacket;
                if(p == null)
                {
                    DataFrame f = o as DataFrame;
                    if(f == null)
                    {
                        continue;
                    }
                    p = new LogPacket("Unknown", Guid.Empty, "Unknown", f, ColorValue.White);
                }
                packets.Add(p);
            }

            using (FileStream stm = File.Open(filename, FileMode.Create))
            {
                GeneralUtils.SerializeLogPackets(packets.ToArray(), stm);
            }
        }

        public static IEnumerable<LogPacket> LoadPackets(string filename)
        {            
            using (FileStream stm = File.OpenRead(filename))
            {
                return GeneralUtils.DeserializeLogPackets(stm);
            }
        }
    }
}
