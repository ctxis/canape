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
using System.Xml.XPath;
using CANAPE.Utils;

namespace CANAPE.DataFrames
{
    /// <summary>
    /// Implementation of XPathNavigator to provide XPath supoprt to the DataFrame
    /// </summary>
    internal class XPathDataNavigator : XPathNavigator
    {
        private DataNode _currNode;
        private bool _caseSensitive;

        void UpdateCurrentNode(DataNode n)
        {
            _currNode = n;            
        }

        public override object UnderlyingObject
        {
            get
            {
                return _currNode;
            }
        }

        public override string BaseURI
        {
            get { return String.Empty; }
        }

        public override XPathNavigator Clone()
        {            
            return (XPathNavigator)MemberwiseClone(); ;
        }

        public override bool IsEmptyElement
        {
            get 
            {
                DataKey key = _currNode as DataKey;

                if (key != null)
                {
                    return key.SubNodes.Count == 0;
                }
                else
                {
                    return false;
                }
            }
        }

        public override bool IsSamePosition(XPathNavigator other)
        {            
            if (other is XPathDataNavigator)
            {
                XPathDataNavigator x = other as XPathDataNavigator;

                if (x._currNode == _currNode)
                {
                    return true;
                }
            }

            return false;
        }

        public override string LocalName
        {
            get 
            {
                if (_caseSensitive)
                {
                    return _currNode.PathName;
                }
                else
                {
                    return _currNode.PathName.ToLower();
                }
            }
        }

        public override bool MoveTo(XPathNavigator other)
        {           
            if (other is XPathDataNavigator)
            {
                XPathDataNavigator nav = other as XPathDataNavigator;

                _currNode = nav._currNode;

                return true;
            }

            return false;            
        }

        public override bool MoveToFirstAttribute()
        {            
            return false;
        }

        public override bool MoveToFirstChild()
        {
            DataKey key = _currNode as DataKey;

            if ((key != null) && (key.SubNodes.Count > 0))
            {
                UpdateCurrentNode(key.SubNodes[0]);
                return true;
            }

            return false;
        }

        public override bool MoveToFirstNamespace(XPathNamespaceScope namespaceScope)
        {
            return false;
        }

        public override bool MoveToId(string id)
        {            
            return false;
        }

        public override bool MoveToNext()
        {            
            if (_currNode.Parent != null)
            {
                DataKey key = _currNode.Parent;
                int index = key.SubNodes.IndexOf(_currNode);

                if ((index >= 0) && ((index + 1) < key.SubNodes.Count))
                {
                    UpdateCurrentNode(key.SubNodes[index + 1]);
                    return true;
                }                
            }

            return false;
        }

        public override bool MoveToNextAttribute()
        {            
            return false;
        }

        public override bool MoveToNextNamespace(XPathNamespaceScope namespaceScope)
        {         
            return false;
        }

        public override bool MoveToParent()
        {         
            if (_currNode.Parent != null)
            {
                UpdateCurrentNode(_currNode.Parent);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override bool MoveToPrevious()
        {
            if (_currNode.Parent != null)
            {
                DataKey key = _currNode.Parent;                
                int index = key.SubNodes.IndexOf(_currNode);

                if (index > 0)
                {
                    UpdateCurrentNode(key.SubNodes[index - 1]);
                    return true;
                }                
            }

            return false;
        }

        public override string Name
        {
            get 
            {
                if (_caseSensitive)
                {
                    return _currNode.PathName;
                }
                else
                {
                    return _currNode.PathName.ToLower();
                }
            }
        }

        public override System.Xml.XmlNameTable NameTable
        {
            get
            {               
                throw new NotImplementedException();
            }
        }

        public override bool CanEdit
        {
            get
            {
                return false;
            }
        }

        public override string NamespaceURI
        {
            get { return String.Empty; }
        }

        public override XPathNodeType NodeType
        {
            get {return XPathNodeType.Element; }
        }

        public override string Prefix
        {
            get { return String.Empty; }
        }

        public override string Value
        {
            get
            {
                string ret;

                if (_currNode is ByteArrayDataValue)
                {
                    byte[] data = _currNode.ToArray();
                    ret = new BinaryEncoding().GetString(data, 0, data.Length);
                }
                else if(_currNode is DataValue)
                {
                    ret = ((DataValue)_currNode).Value.ToString();
                }
                else
                {
                    return _currNode.ToString();
                }

                if (_caseSensitive)
                {
                    return ret;
                }
                else
                {
                    return ret.ToLower();
                }
            }
        }

        public XPathDataNavigator(DataNode currNode, bool caseSensitive)
        {
            _caseSensitive = caseSensitive;
            UpdateCurrentNode(currNode);
        }
    }
}
