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
using System.Drawing;
using CANAPE.Controls;
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.NodeLibrary;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.Net.ConsoleServer
{
    [Serializable]
    public class ConsoleServerEndpointConfig
    {
        public ConsoleServerEndpointConfig()
        {
            ForeColor = Color.Black;
            BackColor = Color.White;
        }

        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Font ConsoleFont { get; set; }
    }

    [NodeLibraryClass("ConsoleServer",        
        Category = NodeLibraryClassCategory.Server,
        ConfigType = typeof(ConsoleServerEndpointConfig))]
    public class ConsoleServerEndpoint : BasePersistDataEndpoint<ConsoleServerEndpointConfig>
    {
        public override void Run(IDataAdapter adapter, Logger logger)
        {
            BinaryEncoding encoding = new BinaryEncoding(true);
            ConsoleControl ctl = new ConsoleControl(adapter, Config.ForeColor, Config.BackColor, Config.ConsoleFont);
           
            CANAPEServiceProvider.GlobalInstance.GetService<IDocumentControl>().HostControl("Console", ctl);

            foreach (DataFrame frame in adapter.ReadFrames())
            {
                ctl.AddText(encoding.GetString(frame.ToArray()));
            }

            ctl.AddText("Connection Closed");
        }

        public override string Description
        {
            get { return "Console Server"; }
        }
    }
}
