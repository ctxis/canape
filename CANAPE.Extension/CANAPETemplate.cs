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
using System.IO;
using System.Text;
using System.Xml;

namespace CANAPE.Extension
{
    /// <summary>
    /// A class to represent a template file
    /// </summary>
    public class CANAPETemplate
    {
        // Could do with a mechanism to localise the templates
        private string _filename;
        private byte[] _content;
        
        /// <summary>
        /// Get the name of this template
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Get the description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// The type name of the template (e.g. for scripts it will be the engine)
        /// </summary>
        public string TypeName { get; private set; }

        /// <summary>
        /// Id of the template, used to match up localized representations
        /// </summary>
        internal string Id { get; private set; }

        /// <summary>
        /// Get the template as text
        /// </summary>
        /// <returns>The text of the file</returns>
        public string GetText()
        {            
            string ret = String.Empty;

            if(_content != null)
            {
                ret = Encoding.UTF8.GetString(_content);
            }
            else
            {
                if (_filename != null)
                {
                    try
                    {
                        ret = File.ReadAllText(_filename);
                    }
                    catch (IOException)
                    {
                        ret = String.Empty;
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Get the template as bytes
        /// </summary>
        /// <returns>The bytes of the file</returns>
        public byte[] GetBytes()
        {
            byte[] ret = new byte[0];

            if (_content != null)
            {
                ret = _content;
            }
            else
            {
                if (_filename != null)
                {
                    try
                    {
                        ret = File.ReadAllBytes(_filename);
                    }
                    catch (IOException)
                    {
                        ret = new byte[0];
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Open the template as a stream
        /// </summary>
        /// <returns>The stream</returns>
        public Stream GetStream()
        {
            if (_content != null)
            {
                return new MemoryStream(_content);
            }
            else
            {
                return File.OpenRead(_filename);
            }
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the template</param>
        /// <param name="description">The description of the template</param>
        /// <param name="typeName">The typename of the template</param>
        /// <param name="filename">The filename for the content (if applicable)</param>
        /// <param name="id">Id of the template</param>
        /// <param name="content">The contents of the template (if applicable)</param>
        private CANAPETemplate(string name, string description, string typeName, string filename, string id, byte[] content)
        {
            Name = name;
            Description = description;
            TypeName = typeName;

            if (String.IsNullOrWhiteSpace(id))
            {
                id = Guid.NewGuid().ToString();
            }

            Id = id;
            _filename = filename;
            _content = content;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the template</param>
        /// <param name="description">The description of the template</param>
        /// <param name="typeName">The typename of the template</param>
        /// <param name="filename">The filename for the content</param>        
        public CANAPETemplate(string name, string description, string typeName, string filename) 
            : this(name, description, typeName, filename, null, null)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the template</param>
        /// <param name="description">The description of the template</param>
        /// <param name="typeName">The typename of the template</param>
        /// <param name="content">The contents of the template</param>
        public CANAPETemplate(string name, string description, string typeName, byte[] content)
            : this(name, description, typeName, null, null, content)
        {
        }

        internal static CANAPETemplate ReadFromXml(XmlElement elem, string baseDir, string typeName)
        {
            string name = elem.GetAttribute("name");
            string description = elem.GetAttribute("description");
            string filename = elem.GetAttribute("filename");
            string id = elem.GetAttribute("id");

            CANAPETemplate template = null;

            if (!String.IsNullOrWhiteSpace(name) && !String.IsNullOrWhiteSpace(filename))
            {
                string fullPath = Path.Combine(baseDir, filename);

                if (File.Exists(fullPath))
                {
                    template = new CANAPETemplate(name, description == null ? String.Empty : description, 
                        typeName, fullPath, id, null);
                }
            }

            return template;
        }
    }
}
