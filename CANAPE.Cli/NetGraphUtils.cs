using System;
using System.Collections.Generic;
using System.Text;
using CANAPE.Documents.Net;
using CANAPE.Documents.Net.NodeConfigs;

namespace CANAPE.Cli
{
    /// <summary>
    /// Simple utilities for netgraphs
    /// </summary>
    public static class NetGraphUtils
    {
        private static string FormatId(Guid id)
        {
            return String.Format("_{0}", id.ToString().Replace("-", "_"));
        }

        /// <summary>
        /// Convert a netgraph to a simple dot diagram
        /// </summary>
        /// <param name="netgraph">The netgraph to convert</param>
        /// <returns>The graph as a dot diagram</returns>
        public static string ToDot(NetGraphDocument netgraph)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendFormat("digraph netgraph {{").AppendLine();
            builder.AppendLine("rankdir=LR;");
            foreach (BaseNodeConfig node in netgraph.Nodes)
            {
                List<string> attrs = new List<string>();

                if (!String.IsNullOrWhiteSpace(node.Label))
                {
                    attrs.Add(String.Format("label=\"{0}\"", node.Label));
                }

                if (node is ServerEndpointConfig)
                {
                    attrs.Add("ordering=out");                    
                    attrs.Add("rank=min");
                }
                else if (node is ClientEndpointConfig)
                {
                    attrs.Add("ordering=in");                    
                    attrs.Add("rank=max");
                }
                else if ((node is DecisionNodeConfig) || (node is SwitchNodeConfig))
                {
                    attrs.Add("shape=diamond");
                }
                else
                {
                    attrs.Add("shape=box");
                }

                if (!node.Enabled)
                {
                    attrs.Add("style=dotted");
                }                

                if (attrs.Count > 0)
                {
                    builder.AppendFormat("{0} [{1}];", FormatId(node.Id), String.Join(",", attrs)).AppendLine();
                }
            }

            foreach (LineConfig edge in netgraph.Edges)
            {
                builder.AppendFormat("{0} -> {1}", FormatId(edge.SourceNode.Id), FormatId(edge.DestNode.Id));
                if (!String.IsNullOrWhiteSpace(edge.PathName))
                {
                    builder.AppendFormat(" [label=\"{0}\"]", edge.PathName);
                }
                builder.AppendLine(";");
            }

            builder.AppendLine("}");

            return builder.ToString();
        }

        /// <summary>
        /// Get a default netgraph
        /// </summary>
        /// <returns>The default graph</returns>
        public static NetGraphDocument GetDefault()
        {
            NetGraphDocument doc = new NetGraphDocument();

            ClientEndpointConfig client = doc.AddClient("Client");
            ServerEndpointConfig server = doc.AddServer("Server");

            LogPacketConfig logout = doc.AddLog("LogOut");
            LogPacketConfig login = doc.AddLog("LogIn");

            server.AddEdge(server, logout, client, login, server);

            return doc;
        }
    }
}
