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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CANAPE.Extension;

namespace CANAPE.Extension
{
    public class BaseHighlighter 
    {
        protected BaseHighlighter(string enginename, string name, string fileextension, string hightlightxml)
        {
            EngineName = enginename;
            Name = name;
            FileExtension = fileextension;
            HighlightXml = hightlightxml;
        }

        public string EngineName
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string FileExtension
        {
            get;
            private set;
        }

        public string HighlightXml
        {
            get;
            private set;
        }
    }
}
