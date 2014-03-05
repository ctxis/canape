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
using CANAPE.DataAdapters;
using CANAPE.DataFrames;
using CANAPE.Scripting;
using CANAPE.Utils;

namespace CANAPE.NodeLibrary.Endpoint
{
    [Serializable]
    public class EchoDataEndpointConfig
    {
        [LocalizedDescription("EchoDataEndpointConfig_ConvertToBasicDescription", typeof(Properties.Resources)), Category("Control")]
        public bool ConvertToBasic { get; set; }
    }

    [NodeLibraryClass("EchoDataEndpoint", typeof(Properties.Resources),
        Category = NodeLibraryClassCategory.Endpoint,
        ConfigType = typeof(EchoDataEndpointConfig))]
    public class EchoDataEndpoint : BasePersistDataEndpoint<EchoDataEndpointConfig>
    {       
        public override void Run(IDataAdapter adapter, Utils.Logger logger)
        {
            DataFrame frame = adapter.Read();

            while(frame != null)
            {                
                if (Config.ConvertToBasic)
                {
                    frame.ConvertToBasic();
                }

                adapter.Write(frame);

                frame = adapter.Read();
            }            
        }

        public override string Description { get { return "Echo Endpoint"; } }
    }
}
