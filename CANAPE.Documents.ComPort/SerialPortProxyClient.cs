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
using CANAPE.Net;
using CANAPE.Net.Clients;
using CANAPE.Net.Tokens;
using CANAPE.Nodes;
using CANAPE.Security;
using CANAPE.Utils;

namespace CANAPE.Documents.ComPort
{
    class SerialPortProxyClient : ProxyClient
    {
        private SerialPortConfiguration _config;

        public SerialPortProxyClient(SerialPortConfiguration config)
        {
            _config = config;
        }

        public override IDataAdapter Connect(ProxyToken token, Logger logger, MetaDictionary meta, MetaDictionary globalMeta, PropertyBag properties, CredentialsManagerService credmgr)
        {
            return new SerialPortDataAdapter(_config.Create());
        }

        // Bind and connect are the same thing
        public override IDataAdapter Bind(ProxyToken token, Logger logger, MetaDictionary meta, MetaDictionary globalMeta, PropertyBag properties, CredentialsManagerService credmgr)
        {
            return new SerialPortDataAdapter(_config.Create());
        }
    }
}
