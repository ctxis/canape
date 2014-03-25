#    CANAPE Network Testing Tool
#    Copyright (C) 2014 Context Information Security
#
#    This program is free software: you can redistribute it and/or modify
#    it under the terms of the GNU General Public License as published by
#    the Free Software Foundation, either version 3 of the License, or
#    (at your option) any later version.
#
#    This program is distributed in the hope that it will be useful,
#    but WITHOUT ANY WARRANTY; without even the implied warranty of
#    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
#    GNU General Public License for more details.
#
#    You should have received a copy of the GNU General Public License
#    along with this program.  If not, see <http:#www.gnu.org/licenses/>.

from CANAPE.Documents.Net import *
import CANAPE.NodeLibrary.Server
import CANAPE.Documents.Net.Factories
import System.Net

class DnsServerDocument(NetServerDocument):
    def __init__(self):        
        self.ServerFactory = CANAPE.Documents.Net.Factories.LibraryDataEndpointFactory(CANAPE.NodeLibrary.Server.DnsDataServer, 
                                                                                       CANAPE.NodeLibrary.Server.DnsDataServerConfig, "DNS Server")
        self.LocalPort = 53
        self.UdpEnable = True

    @property
    def ResponseAddress(self):
        return str(self.Config.ReponseAddress)

    @ResponseAddress.setter
    def ResponseAddress(self, addr):
        self.ServerFactory.Config.ResponseAddress = System.Net.IPAddress.Parse(addr)

    @property
    def ResponseAddress6(self):
        return str(self.ServerFactory.Config.ReponseAddress6)

    @ResponseAddress6.setter
    def ResponseAddress6(self, addr):
        self.ServerFactory.Config.ResponseAddress6 = System.Net.IPAddress.Parse(addr)

    @property
    def ReverseDns(self):
        return self.ServerFactory.Config.ReverseDns

    @ReverseDns.setter
    def ReverseDns(self, rdns):
        self.ServerFactory.Config.ReverseDns = rdns

    @property
    def TimeToLive(self):
        return self.ServerFactory.Config.ReverseDns

    @TimeToLive.setter
    def TimeToLive(self, ttl):
        self.ServerFactory.Config.TimeToLive = ttl

