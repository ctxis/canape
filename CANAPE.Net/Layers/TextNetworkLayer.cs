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

using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using System;
using System.IO;
using System.Text;

namespace CANAPE.Net.Layers
{
    /// <summary>
    /// Network layer to convert data to encoded text
    /// </summary>
    public class TextNetworkLayer : BaseNetworkLayer<TextNetworkLayerConfig, dynamic>
    {
        private TextNetworkLayerConfig _config;

        private class SimpleTextDataAdapter : BaseDataAdapter
        {
            private Encoding _encoding;
            private IDataAdapter _adapter;

            public SimpleTextDataAdapter(IDataAdapter adapter, Encoding encoding)
            {
                _adapter = adapter;
                _encoding = encoding;
            }

            public override DataFrames.DataFrame Read()
            {
                DataFrame ret = _adapter.Read();
                if (ret != null)
                {
                    string s = _encoding.GetString(ret.ToArray());
                    ret = new DataFrame(s, _encoding);
                    return ret;
                }
                else
                {
                    return null;
                }
            }

            public override void Write(DataFrame data)
            {
                _adapter.Write(data);
            }

            protected override void OnDispose(bool disposing)
            {
                _adapter.Close();
            }

            public override bool CanTimeout
            {
                get
                {
                    return _adapter.CanTimeout;
                }
            }

            public override int ReadTimeout
            {
                get
                {
                    return _adapter.ReadTimeout;
                }
                set
                {
                    _adapter.ReadTimeout = value;
                }
            }

            public override void Reconnect()
            {
                _adapter.Reconnect();
            }
        }
        
        private class ComplexTextDataAdapter : BaseDataAdapter
        {
            private Encoding _encoding;
            private IDataAdapter _adapter;
            private MemoryStream _readBuffer;
            private Decoder _decoder;

            public ComplexTextDataAdapter(IDataAdapter adapter, Encoding encoding)
            {
                _adapter = adapter;
                _encoding = encoding;
                _decoder = encoding.GetDecoder();
                _readBuffer = new MemoryStream();
            }

            public override DataFrames.DataFrame Read()
            {
                DataFrame ret = null;

                while (ret != null)
                {
                    DataFrame curr = _adapter.Read();
                    if (curr != null)
                    {
                        byte[] data = ret.ToArray();

                        int charcount = _decoder.GetCharCount(data, 0, data.Length, false);
                        if (charcount > 0)
                        {
                            char[] buf = new char[charcount];

                            _decoder.GetChars(data, 0, data.Length, buf, 0);

                            ret = new DataFrame(new String(buf), _encoding);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                return ret;
            }

            public override void Write(DataFrame data)
            {
                _adapter.Write(data);
            }

            protected override void OnDispose(bool disposing)
            {
                _adapter.Close();
            }

            public override bool CanTimeout
            {
                get
                {
                    return _adapter.CanTimeout;
                }
            }

            public override int ReadTimeout
            {
                get
                {
                    return _adapter.ReadTimeout;
                }
                set
                {
                    _adapter.ReadTimeout = value;
                }
            }

            public override void Reconnect()
            {                
                _adapter.Reconnect();
            }
        }

        protected override void OnConnect(ref IDataAdapter client, ref IDataAdapter server, NetworkLayerBinding binding)
        {
            Encoding encoding = _config.GetEncoding();

            if ((binding & NetworkLayerBinding.Client) == NetworkLayerBinding.Client)
            {
                if (encoding.IsSingleByte)
                {
                    client = new SimpleTextDataAdapter(client, encoding);
                }
                else
                {
                    client = new ComplexTextDataAdapter(client, encoding);
                }
            }

            if ((binding & NetworkLayerBinding.Server) == NetworkLayerBinding.Server) 
            {
                if (encoding.IsSingleByte)
                {
                    server = new SimpleTextDataAdapter(server, encoding);
                }
                else
                {
                    server = new ComplexTextDataAdapter(server, encoding);
                }
            }
        }
    }
}
