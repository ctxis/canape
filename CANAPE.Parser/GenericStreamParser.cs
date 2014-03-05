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
using System.Threading;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using CANAPE.Utils;

namespace CANAPE.Parser
{
    /// <summary>
    /// Generic stream parser for a structure
    /// </summary>
    /// <typeparam name="T">The type of sequence</typeparam>
    public abstract class GenericStreamParser<T> : IDataStreamParser where T :  IStreamTypeParser, new()
    {
        private string _formatString;
        private static Type[] _derivedTypes;        

        static GenericStreamParser()
        {
            Type[] types = typeof(T).Assembly.GetTypes();
            List<Type> outTypes = new List<Type>();

            foreach (Type t in types)
            {
                if (t.IsClass && !t.IsAbstract)
                {
                    if (t.GetCustomAttributes(typeof(GuidAttribute), false).Length > 0)
                    {
                        outTypes.Add(t);
                    }
                }
            }

            _derivedTypes = outTypes.ToArray();
        }

        protected GenericStreamParser() 
            : this(ObjectConverter.GetFormatString(typeof(T)))
        {
        }        

        protected GenericStreamParser(string formatString)
        {
            _formatString = formatString;
        }

        public void FromReader(DataReader reader, DataFrames.DataKey root, Utils.Logger logger)
        {
            IStreamTypeParser parser = new T();
            StateDictionary state = new StateDictionary();

            try
            {
                // Reset byte count
                reader.ByteCount = 0;
                parser.FromStream(reader, state, logger);

                ObjectConverter.ToNode(root, parser, _derivedTypes);
            }
            catch(ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                if (!(ex is EndOfStreamException) || (reader.ByteCount != 0))
                {
                    logger.LogError(CANAPE.Parser.Properties.Resources.GenericStreamParser_ReadException, ex.Message, typeof(T).Name);
                }
                throw;
            }
        }

        public void ToWriter(DataFrames.DataWriter writer, DataFrames.DataKey root, Utils.Logger logger)
        {             
            StateDictionary state = new StateDictionary();

            try
            {
                IStreamTypeParser parser = ObjectConverter.FromNode<T>(root, _derivedTypes);

                parser.ToStream(writer, state, logger);
            }
            catch (ThreadAbortException)
            {
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(CANAPE.Parser.Properties.Resources.GenericStreamParser_WriteException, ex.Message, typeof(T).Name);
                throw;
            }
        }

        public string ToDisplayString(DataKey root, Utils.Logger logger)
        {
            // Need to implement some mechanism of handling this

            if (String.IsNullOrWhiteSpace(_formatString))
            {
                return typeof(T).ToString();
            }
            else
            {
                // Do format
                return GeneralUtils.FormatKeyString(_formatString, root);
            }
        }
    }
}
