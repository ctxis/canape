using CANAPE.Documents.Net.Factories;
using CANAPE.Utils;
using System;

namespace CANAPE.Net.NamedPipes
{
    [Serializable]
    class NamedPipeProxyClientFactory : IProxyClientFactory
    {
        public event EventHandler ConfigChanged;

        public Clients.ProxyClient Create(CANAPE.Utils.Logger logger)
        {
            return new NamedPipeProxyClient("atsvc");
        }

        public object Clone()
        {
            return GeneralUtils.CloneObject(this);
        }
    }
}
