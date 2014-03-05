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
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Utils;
using System.Collections.Generic;

namespace CANAPE.Extension
{
    internal class StringConverters
    {
        public static byte[] HexStringToBytes(string s)
        {
            List<byte> ret = new List<byte>();

            if ((s.Length % 2) != 0)
            {
                throw new ArgumentException(CANAPE.Properties.Resources.StringConverters_InvalidHexStringLength);
            }

            for (int i = 0; i < s.Length; i += 2)
            {
                byte val = byte.Parse(s.Substring(i, 2), NumberStyles.HexNumber, null);

                ret.Add(val);
            }

            return ret.ToArray();
        }

        public static string BytesToHexString(byte[] ba)
        {
            StringBuilder builder = new StringBuilder();

            foreach (byte b in ba)
            {
                builder.AppendFormat("{0:X02}", b);
            }

            return builder.ToString();
        }
    }

    public abstract class BaseStringConverter : IStringConverter
    {
        protected IWin32Window _parent;

        protected BaseStringConverter(IWin32Window parent)
        {
            _parent = parent;
        }

        protected abstract string OnConvert(long startPos, string s);

        public string Convert(long startPos, string s)
        {
            try
            {
                return OnConvert(startPos, s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(_parent, ex.Message,
                        CANAPE.Properties.Resources.MessageBox_ErrorString,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return s;
        }
    }

    public abstract class BaseStringAndHexConverter : IStringConverter, IHexConverter
    {
        protected IWin32Window _parent;

        protected BaseStringAndHexConverter(IWin32Window parent)
        {
            _parent = parent;
        }

        protected abstract string OnConvert(long startPos, string s);
        protected abstract byte[] OnConvert(long startPos, byte[] ba);

        public string Convert(long startPos, string s)
        {
            try
            {
                return OnConvert(startPos, s);
            }
            catch (Exception ex)
            {
                MessageBox.Show(_parent, ex.Message,
                        CANAPE.Properties.Resources.MessageBox_ErrorString,
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return s;
        }
    
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

    [StringConverter("Encode"), Category("Base64")]
    public class ToBase64Converter : BaseStringConverter
    {
        public ToBase64Converter(IWin32Window parent) :
            base(parent)
        {
        }

        protected override string OnConvert(long startPos, string s)
        {
            byte[] data = BinaryEncoding.Instance.GetBytes(s);

            return System.Convert.ToBase64String(data);
        }
    }

    [StringConverter("Decode"), Category("Base64")]
    public class FromBase64Converter : BaseStringConverter
    {
        public FromBase64Converter(IWin32Window parent)
            : base(parent)
        { }

        protected override string OnConvert(long startPos, string s)
        {
            if ((s.Length % 4) != 0)
            {
                int len = s.Length % 4;

                s += new String('=', 4 - len);
            }

            byte[] data = System.Convert.FromBase64String(s);

            return BinaryEncoding.Instance.GetString(data);
        }
    }

    [StringConverter("To Upper Case"), Category("String Conversion")]
    public class ToUpperConverter : BaseStringConverter
    {
        public ToUpperConverter(IWin32Window parent)
            : base(parent)
        { }

        protected override string OnConvert(long startPos, string s)
        {
            return s.ToUpper(CultureInfo.CurrentCulture);
        }
    }

    [StringConverter("To Lower Case"), Category("String Conversion")]
    public class ToLowerConverter : BaseStringConverter
    {
        public ToLowerConverter(IWin32Window parent)
            : base(parent)
        { }

        protected override string OnConvert(long startPos, string s)
        {
            return s.ToLower(CultureInfo.CurrentCulture);
        }
    }

    [StringConverter("Swap Case"), Category("String Conversion")]
    public class SwapCaseConverter : BaseStringConverter
    {
        public SwapCaseConverter(IWin32Window parent)
            : base(parent)
        { }

        protected override string OnConvert(long startPos, string s)
        {
            char[] buf = s.ToCharArray();

            for (int i = 0; i < buf.Length; ++i)
            {
                if (Char.IsUpper(buf[i]))
                {
                    buf[i] = Char.ToLower(buf[i], CultureInfo.CurrentCulture);
                }
                else if (Char.IsLower(buf[i]))
                {
                    buf[i] = Char.ToUpper(buf[i], CultureInfo.CurrentCulture);
                }
            }

            return new string(buf);
        }
    }

    [StringConverter("Encode"), Category("Hex String")]
    public class ToHexConverter : BaseStringConverter
    {
        public ToHexConverter(IWin32Window parent) 
            : base(parent)
        {

        }

        protected override string OnConvert(long startPos, string s)
        {
            return StringConverters.BytesToHexString(BinaryEncoding.Instance.GetBytes(s));
        }
    }

    [StringConverter("Decode"), Category("Hex String")]
    public class FromHexConverter : BaseStringConverter
    {
        public FromHexConverter(IWin32Window parent)
            : base(parent)
        {

        }

        protected override string OnConvert(long startPos, string s)
        {
            return BinaryEncoding.Instance.GetString(StringConverters.HexStringToBytes(s));
        }
    }

    [StringConverter("Decode"), HexConverter("Decode"), Category("URL String")]
    public class FromUrlConverter : BaseStringAndHexConverter
    {
        public FromUrlConverter(IWin32Window parent)
            : base(parent)
        {

        }

        protected override string OnConvert(long startPos, string s)
        {
            return Uri.UnescapeDataString(s);
        }

        protected override byte[] OnConvert(long startPos, byte[] ba)
        {
            return BinaryEncoding.Instance.GetBytes(OnConvert(startPos, BinaryEncoding.Instance.GetString(ba)));
        }
    }

    [StringConverter("Encode"), HexConverter("Encode"), Category("URL String")]
    public class ToUrlConverter : BaseStringAndHexConverter
    {
        public ToUrlConverter(IWin32Window parent)
            : base(parent)
        {

        }

        protected override string OnConvert(long startPos, string s)
        {
            return Uri.EscapeDataString(s);
        }

        protected override byte[] OnConvert(long startPos, byte[] ba)
        {
            return BinaryEncoding.Instance.GetBytes(OnConvert(startPos, BinaryEncoding.Instance.GetString(ba)));
        }
    }

    [StringConverter("Full Encode"), HexConverter("Full Encode"), Category("URL String")]
    public class ToFullUrlConverter : BaseStringAndHexConverter
    {
        public ToFullUrlConverter(IWin32Window parent)
            : base(parent)
        {

        }

        protected override string OnConvert(long startPos, string s)
        {
            StringBuilder builder = new StringBuilder();

            foreach (char c in s)
            {
                builder.AppendFormat("%{0}", Uri.HexEscape(c));
            }

            return builder.ToString();
        }

        protected override byte[] OnConvert(long startPos, byte[] ba)
        {
            return BinaryEncoding.Instance.GetBytes(OnConvert(startPos, BinaryEncoding.Instance.GetString(ba)));
        }
    }
}
