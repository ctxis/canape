using CANAPE.Documents.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CANAPE.Net.NamedPipes
{
    public class NamedPipeProxyServerDocument : FixedProxyDocument
    {
        public NamedPipeProxyServerDocument()
        {
            this.Client = new NamedPipeProxyClientFactory();
        }
    }
}
