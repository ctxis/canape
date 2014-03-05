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
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Utils;

namespace CANAPE.Extension
{   
    public abstract class BaseHexConverter : IHexConverter
    {
        protected IWin32Window _parent;

        protected BaseHexConverter(IWin32Window parent)
        {
            _parent = parent;
        }

        protected abstract byte[] OnConvert(long startPos, byte[] ba);

        public byte[] Convert(long startPos, byte[] ba)
        {
 	        try
            {
                return OnConvert(startPos, ba);
            }
            catch (Exception ex)
            {
                MessageBox.Show(_parent, ex.Message,
                        CANAPE.Properties.Resources.MessageBox_ErrorString, 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return ba;
        }
    }

    [HexConverterAttribute("Decode", ShortcutKeys=Keys.Control | Keys.Shift | Keys.B), Category("Base64")]
    public class BinaryFromBase64Converter : BaseHexConverter
    {
        protected override byte[]  OnConvert(long startPos, byte[] ba)
        {
            return System.Convert.FromBase64String(Encoding.ASCII.GetString(ba));
        }
             
        public BinaryFromBase64Converter(IWin32Window parent) : base(parent)
        {
        }
    }

    [HexConverterAttribute("Encode"), Category("Base64")]
    public class BinaryToBase64Converter : BaseHexConverter
    {
        public BinaryToBase64Converter(IWin32Window parent)
            : base(parent)
        {            
        }

        protected override byte[]  OnConvert(long startPos, byte[] ba)
        {
            return BinaryEncoding.Instance.GetBytes(System.Convert.ToBase64String(ba));
        }
    }

    [HexConverterAttribute("Decode"), Category("Hex String")]
    public class BinaryFromHexConverter : BaseHexConverter
    {
        public BinaryFromHexConverter(IWin32Window parent) 
            : base(parent)
        {
        }

        protected override byte[] OnConvert(long startPos, byte[] ba)
        {
            return StringConverters.HexStringToBytes(BinaryEncoding.Instance.GetString(ba));
        }
    }

    [HexConverter("Encode"), Category("Hex String")]
    public class BinaryToHexConverter : BaseHexConverter
    {
        public BinaryToHexConverter(IWin32Window parent)
            : base(parent)
        {
        }

        protected override byte[] OnConvert(long startPos, byte[] ba)
        {
            return BinaryEncoding.Instance.GetBytes(StringConverters.BytesToHexString(ba));
        }
    }

    [HexConverter("Decompress"), LocalizedCategory("GZip")]
    public class GzipToBinaryConverter : BaseHexConverter
    {
        public GzipToBinaryConverter(IWin32Window parent) : base(parent)
        {
        }

        protected override byte[] OnConvert(long startPos, byte[] ba)
        {
            GZipStream gzip = new GZipStream(new MemoryStream(ba), CompressionMode.Decompress);
            MemoryStream outStm = new MemoryStream();

            gzip.CopyTo(outStm);

            return outStm.ToArray();
        }
    }

    [HexConverter("Compress"), LocalizedCategory("GZip")]
    public class BinaryToGzipConverter : BaseHexConverter
    {
        public BinaryToGzipConverter(IWin32Window parent)
            : base(parent)
        {

        }

        protected override byte[] OnConvert(long startPos, byte[] ba)
        {
            GZipStream gzip = new GZipStream(new MemoryStream(ba), CompressionMode.Compress);
            MemoryStream outStm = new MemoryStream();

            gzip.CopyTo(outStm);

            return outStm.ToArray();
        }
    }
}
