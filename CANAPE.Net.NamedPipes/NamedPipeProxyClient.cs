using CANAPE.DataAdapters;
using CANAPE.Net.Clients;
using CANAPE.Net.Tokens;
using CANAPE.Nodes;
using CANAPE.Security;
using CANAPE.Utils;
using System;
using System.IO.Pipes;

namespace CANAPE.Net.NamedPipes
{
    class NamedPipeProxyClient : ProxyClient
    {        
        string _pipeName;

        public NamedPipeProxyClient(string pipeName)
        {
            _pipeName = pipeName;
        }

        public override IDataAdapter Connect(ProxyToken token, Logger logger, MetaDictionary meta, MetaDictionary globalMeta, PropertyBag properties, CredentialsManagerService credmgr)
        {
            return new StreamDataAdapter(new NamedPipeClientStream(".", _pipeName, PipeDirection.InOut));
        }

        public override CANAPE.DataAdapters.IDataAdapter Bind(ProxyToken token, Logger logger, MetaDictionary meta, MetaDictionary globalMeta, PropertyBag properties, CredentialsManagerService credmgr)
        {
            throw new NotImplementedException();
        }
    }
}
