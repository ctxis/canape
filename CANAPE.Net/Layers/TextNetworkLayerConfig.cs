using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CANAPE.Utils;

namespace CANAPE.Net.Layers
{
    /// <summary>
    /// Configuration for network layer
    /// </summary>
    [Serializable]
    public class TextNetworkLayerConfig
    {
        private bool _enabled;
        private BinaryStringEncoding _textEncoding;

        public BinaryStringEncoding TextEncoding
        {
            get { return _textEncoding; }
            set
            {
                if (_textEncoding != value)
                {
                    _textEncoding = value;
                }
            }
        }

        public Encoding GetEncoding()
        {
            return GeneralUtils.GetEncodingFromType(_textEncoding);
        }
    }
}
