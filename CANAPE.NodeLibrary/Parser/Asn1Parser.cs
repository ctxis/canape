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
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;
using Org.BouncyCastle.Asn1;

namespace CANAPE.NodeLibrary.Parser
{
    [Serializable]
    public class Asn1ParserConfig
    {
        [LocalizedDescription("Asn1ParserConfig_NoVerifyDescription", typeof(Properties.Resources))]
        public bool NoVerify { get; set; }

        [LocalizedDescription("Asn1ParserConfig_IgnoreSequencesDescription", typeof(Properties.Resources))]
        public bool IgnoreSequences { get; set; }

        [LocalizedDescription("Asn1ParserConfig_IgnoreSetsDescription", typeof(Properties.Resources))]
        public bool IgnoreSets { get; set; }

        [LocalizedDescription("Asn1ParserConfig_IgnoreTaggedObjectsDescription", typeof(Properties.Resources))]
        public bool IgnoreTaggedObjects { get; set; }

        [LocalizedDescription("Asn1ParserConfig_NoDecodeDescription", typeof(Properties.Resources))]
        public bool NoDecode { get; set; }
    }

    [NodeLibraryClass("Asn1Parser", typeof(Properties.Resources),
        Category = NodeLibraryClassCategory.Parser,         
        ConfigType=typeof(Asn1ParserConfig))]
    public class Asn1Parser : BasePersistDynamicNode<Asn1ParserConfig>, IDataStreamParser
    {
        private class DummyAsn1Object : DerUnknownTag
        {
            public DummyAsn1Object(byte[] data) 
                : base(0, data)
            {                
            }

            protected override bool Asn1Equals(Asn1Object asn1Object)
            {
                DummyAsn1Object o = asn1Object as DummyAsn1Object;

                if (o != null)
                {
                    return o.GetData() == GetData();
                }
                else
                {
                    return false;
                }
            }

            protected override int Asn1GetHashCode()
            {
                return GetData().GetHashCode();
            }
        }

        private class CustomDerOutputStream : DerOutputStream
        {
            public CustomDerOutputStream(Stream stm) : base(stm)
            {
            }

            public override void WriteObject(Asn1Object obj)
            {
                DummyAsn1Object o = obj as DummyAsn1Object;

                if (o != null)
                {
                    byte[] data = o.GetData();
                    Write(data, 0, data.Length);
                }
                else
                {
                    base.WriteObject(obj);
                }
            }
        }

        private static byte[] GetDerEncoded(Asn1Object obj)
        {
            MemoryStream stm = new MemoryStream();
            CustomDerOutputStream outstm = new CustomDerOutputStream(stm);
            outstm.WriteObject(obj);
            outstm.Close();

            return stm.ToArray();
        }

        [Serializable]
        class Asn1SequenceKey : DataKey
        {
            bool _noVerify;
        
            public override byte[] ToArray()
            {
                List<Asn1Encodable> objs = new List<Asn1Encodable>();

                if (_noVerify)
                {
                    foreach (DataNode node in SubNodes)
                    {
                        objs.Add(new DummyAsn1Object(node.ToArray()));
                    }
                }
                else
                {
                    foreach (DataNode node in SubNodes)
                    {                       
                        objs.Add(Asn1Object.FromByteArray(node.ToArray()));                       
                    }
                }

                return GetDerEncoded(new DerSequence(objs.ToArray()));
            }

            public Asn1SequenceKey(string name, bool noVerify)
                : base(name)
            {
                _noVerify = noVerify;
            }
        }

        [Serializable]
        class Asn1SetKey : DataKey
        {
            bool _noVerify;

            public override byte[] ToArray()
            {
                List<Asn1Encodable> objs = new List<Asn1Encodable>();

                if (_noVerify)
                {
                    foreach (DataNode node in SubNodes)
                    {
                        objs.Add(new DummyAsn1Object(node.ToArray()));
                    }
                }
                else
                {
                    foreach (DataNode node in SubNodes)
                    {                        
                        objs.Add(Asn1Object.FromByteArray(node.ToArray()));                        
                    }
                }

                return GetDerEncoded(new DerSet(objs.ToArray()));
            }

            public Asn1SetKey(string name, bool noVerify)
                : base(name)
            {
                _noVerify = noVerify;
            }

            #region IAsn1VerifyObject Members

            public bool Verify
            {
                get { return true; }
            }

            #endregion
        }

        [Serializable]
        class Asn1TaggedObjectKey : DataKey
        {
            bool _noVerify;

            public Asn1TaggedObjectKey(string name, int tagNo, bool noVerify)
                : base(name)
            {
                AddValue("TagNo", tagNo);
                _noVerify = noVerify;
            }

            public override byte[]  ToArray()
            {
                DerTaggedObject tag = new DerTaggedObject(GetValue("TagNo").Value,
                    _noVerify ? new DummyAsn1Object(this["Object"].ToArray()) :
                    Asn1Object.FromByteArray(this["Object"].ToArray()));

                return GetDerEncoded(tag);
            }

            #region IAsn1VerifyObject Members

            public bool Verify
            {
                get { return true; }
            }

            #endregion
        }

        [Serializable]
        class Asn1ObjectIdentifierValue : StringDataValue
        {
            public override byte[] ToArray()
            {
                DerObjectIdentifier objid = new DerObjectIdentifier(Value);

                return objid.GetDerEncoded();
            }

            public Asn1ObjectIdentifierValue(string name, string value)
                : base(name, value)
            {
            }

            #region IAsn1VerifyObject Members

            public bool Verify
            {
                get { return true; }
            }

            #endregion
        }

        [Serializable]
        class Asn1StringValue<T> : StringDataValue where T : DerStringBase
        {
            public override byte[] ToArray()
            {
                DerStringBase str = (DerStringBase)Activator.CreateInstance(typeof(T), Value);

                return str.GetDerEncoded();
            }

            public Asn1StringValue(string name, string value)
                : base(name, value)
            {
            }
        }

        [Serializable]
        class Asn1BitStringKey : DataKey
        {
            public override byte[] ToArray()
            {
                DerBitString bits = new DerBitString(this["Bits"].ToArray(), GetValue("PadBits").Value);

                return bits.GetDerEncoded();
            }

            public Asn1BitStringKey(string name, int padBits, byte[] bits)
                : base(name)
            {
                AddValue("PadBits", padBits);
                AddValue("Bits", bits);
            }
        }

        [Serializable]
        class Asn1OctetStringObject : DataKey
        {
            public override byte[] ToArray()
            {
                MemoryStream stm = new MemoryStream();
                DataWriter writer = new DataWriter(stm);

                foreach (DataNode node in this.SubNodes)
                {
                    node.ToWriter(writer);
                }

                DerOctetString oct = new DerOctetString(stm.ToArray());

                return oct.GetDerEncoded();
            }

            public Asn1OctetStringObject(string name)
                : base(name)
            {
            }
        }

        [Serializable]
        class Asn1IntegerValue : ByteArrayDataValue
        {
            public override byte[] ToArray()
            {
                DerInteger i = new DerInteger((byte[])Value);

                return i.GetDerEncoded();
            }

            public Asn1IntegerValue(string name, byte[] data)
                : base(name, data)
            {
            }
        }

        [Serializable]
        class Asn1OctetStringValue : ByteArrayDataValue
        {
            public override byte[] ToArray()
            {
                DerOctetString oct = new DerOctetString((byte[])Value);

                return oct.GetDerEncoded();
            }

            public Asn1OctetStringValue(string name, byte[] value)
                : base(name, value)
            {
            }
        }

        [Serializable]
        class Asn1BooleanValue : GenericDataValue<bool>
        {
            public override byte[] ToArray()
            {
                DerBoolean boo = Value ? DerBoolean.True : DerBoolean.False;

                return boo.GetDerEncoded();
            }

            public Asn1BooleanValue(string name, bool value)
                : base(name, value)
            {
            }
        }

        [Serializable]
        class Asn1NullValue : DataValue
        {
            public override byte[] ToArray()
            {
                return DerNull.Instance.GetDerEncoded();
            }

            public Asn1NullValue(string name)
                : base(name)
            {
            }

            public override dynamic Value
            {
                get
                {
                    return String.Empty;
                }
                set
                {
                    // Do nothing
                }
            }

            public override bool FixedLength
            {
                get { return true; }
            }

            public override void FromArray(byte[] data)
            {
                throw new NotImplementedException();
            }
        }

        [Serializable]
        class Asn1DateTimeValue : DateTimeDataValue
        {
            protected override byte[] OnToArray()
            {
                throw new NotImplementedException();
            }

            protected override void OnFromArray(byte[] data)
            {
                throw new NotImplementedException();
            }

            public override byte[] ToArray()
            {
                DerUtcTime t = new DerUtcTime(_time);

                return t.GetDerEncoded();
            }

            public Asn1DateTimeValue(string name, DateTime time) 
                : base(name, false, time)
            {

            }
        }

        [Serializable]
        class Asn1GeneralizedTimeValue : DateTimeDataValue
        {
            protected override byte[] OnToArray()
            {
                throw new NotImplementedException();
            }

            protected override void OnFromArray(byte[] data)
            {
                throw new NotImplementedException();
            }

            public override byte[] ToArray()
            {
                DerGeneralizedTime t = new DerGeneralizedTime(_time);

                return t.GetDerEncoded();
            }

            public Asn1GeneralizedTimeValue(string name, DateTime time)
                : base(name, false, time)
            {

            }
        }

        [Serializable]
        class Asn1ApplicationSpecificValue : DataKey
        {
            public override byte[] ToArray()
            {
                DerApplicationSpecific bits = new DerApplicationSpecific(GetValue("TagNo").Value, 
                    this["Octets"].ToArray());

                return bits.GetDerEncoded();
            }

            public Asn1ApplicationSpecificValue(string name, int tagNo, byte[] data)
                : base(name)
            {
                AddValue("TagNo", tagNo);
                AddValue("Octets", data);
            }
        }

        #region IDataStreamParser Members

        private void AddAsn1Object(string name, DataKey root, Asn1Object obj, int level, Logger logger)
        {
            Asn1Sequence seq = obj as Asn1Sequence;
            Asn1Set set = obj as Asn1Set;
            Asn1TaggedObject tag = obj as Asn1TaggedObject;
            string currName = name ?? obj.GetType().Name;

            System.Diagnostics.Trace.WriteLine(String.Format("{0} {1}", currName, obj.GetType()));

            if (seq != null)
            {
                if (!Config.IgnoreSequences)
                {
                    DataKey key = new Asn1SequenceKey(currName, Config.NoVerify);

                    foreach (Asn1Object o in seq)
                    {
                        AddAsn1Object(null, key, o, level + 1, logger);
                    }

                    root.AddSubNode(key);
                }
                else
                {
                    root.AddValue(currName, obj.GetDerEncoded());
                }
            }
            else if (set != null)
            {
                if (!Config.IgnoreSets)
                {
                    DataKey key = new Asn1SetKey(currName, Config.NoVerify);

                    foreach (Asn1Object o in set)
                    {
                        AddAsn1Object(null, key, o, level + 1, logger);
                    }

                    root.AddSubNode(key);
                }
                else
                {
                    root.AddValue(currName, obj.GetDerEncoded());
                }
            }
            else if (tag != null)
            {
                if (!Config.IgnoreTaggedObjects)
                {
                    DataKey key = new Asn1TaggedObjectKey(currName, tag.TagNo, Config.NoVerify);

                    root.AddSubNode(key);

                    Asn1Object o = tag.GetObject();
                    DerOctetString oct = o as DerOctetString;

                    AddAsn1Object("Object", key, tag.GetObject(), level + 1, logger);

                    //if (oct != null)
                    //{
                    //    Asn1InputStream input = new Asn1InputStream(oct.GetOctetStream());

                    //    try
                    //    {
                    //        Asn1Object next = input.ReadObject();
                    //        if (next == null)
                    //        {
                    //            AddAsn1Object("Object", key, o, logger);
                    //        }
                    //        else
                    //        {
                    //            Asn1OctetStringObject newRoot = new Asn1OctetStringObject("Object");

                    //            while (next != null)
                    //            {
                    //                AddAsn1Object(next.GetType().Name, newRoot, next, logger);

                    //                next = input.ReadObject();
                    //            }

                    //            key.AddSubNode(newRoot);
                    //        }
                    //    }
                    //    catch (IOException)
                    //    {
                    //        AddAsn1Object("Object", key, o, logger);
                    //    }
                    //}
                    //else
                    //{
                    //    AddAsn1Object("Object", key, tag.GetObject(), logger);
                    //}
                }
                else
                {
                    root.AddValue(currName, obj.GetDerEncoded());
                }
            }
            else
            {
                if (!Config.NoDecode)
                {                    
                    DerStringBase str = obj as DerStringBase;
                    DerObjectIdentifier oid = obj as DerObjectIdentifier;
                    DerInteger i = obj as DerInteger;
                    DerOctetString oct = obj as DerOctetString;
                    DerBitString bits = obj as DerBitString;
                    DerBoolean boo = obj as DerBoolean;
                    DerNull n = obj as DerNull;
                    DerUtcTime time = obj as DerUtcTime;
                    DerGeneralizedTime gt = obj as DerGeneralizedTime;
                    DerApplicationSpecific app = obj as DerApplicationSpecific;

                    if (oct != null)
                    {
                        root.AddValue(new Asn1OctetStringValue(currName, oct.GetOctets()));
                    }
                    else if (bits != null)
                    {
                        root.AddSubNode(new Asn1BitStringKey(currName, bits.PadBits, bits.GetBytes()));
                    }
                    else if (str != null)
                    {
                        Type stringType = typeof(Asn1StringValue<>).MakeGenericType(str.GetType());

                        root.AddValue((DataValue)Activator.CreateInstance(stringType, currName, str.GetString()));
                    }
                    else if (oid != null)
                    {
                        root.AddValue(new Asn1ObjectIdentifierValue(currName, oid.Id));
                    }                    
                    else if (i != null)
                    {                        
                        root.AddValue(new Asn1IntegerValue(currName, i.Value.ToByteArray()));
                    }
                    else if (boo != null)
                    {
                        root.AddValue(new Asn1BooleanValue(currName, boo.IsTrue));
                    } 
                    else if (n != null)
                    {
                        root.AddValue(new Asn1NullValue(currName));
                    }
                    else if (time != null)
                    {
                        root.AddValue(new Asn1DateTimeValue(currName, time.ToDateTime()));
                    }
                    else if (gt != null)
                    {
                        root.AddValue(new Asn1GeneralizedTimeValue(currName, gt.ToDateTime()));
                    }
                    else if (app != null)
                    {
                        root.AddSubNode(new Asn1ApplicationSpecificValue(currName, app.ApplicationTag, app.GetContents()));
                    }
                    else
                    {
                        logger.LogError("Cannot convert type {0} to a class", obj.GetType().Name);
                        root.AddValue(currName, obj.GetDerEncoded());
                    }
                }
                else
                {
                    root.AddValue(currName, obj.GetDerEncoded());
                }
            }
        }

        public void FromReader(DataReader reader, DataFrames.DataKey root, Utils.Logger logger)
        {
            Asn1InputStream input = new Asn1InputStream(reader.GetStream());
            Asn1Object obj = input.ReadObject();

            if (obj == null)
            {
                throw new EndOfStreamException();
            }

            while (obj != null)
            {
                AddAsn1Object(obj.GetType().Name, root, obj, 0, logger);

                obj = input.ReadObject();          
            } 
        }

        public void ToWriter(DataFrames.DataWriter writer, DataFrames.DataKey root, Utils.Logger logger)
        { 
            foreach(DataNode node in root.SubNodes)
            {
                writer.Write(node.ToArray());
            }
        }

        #endregion

        #region IDataParser Members

        public string ToDisplayString(DataFrames.DataKey root, Utils.Logger logger)
        {
            return "ASN1";
        }

        #endregion
    }
}
