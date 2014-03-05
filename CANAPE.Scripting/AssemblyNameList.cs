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
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace CANAPE.Scripting
{
    /// <summary>
    /// This is a list class which is used to better serialize assembly names, default causes issues
    /// as AssemblyName gets deserialized very late for some reason.
    /// </summary>
    [Serializable]
    sealed class AssemblyNameList : IList<AssemblyName>, ISerializable
    {
        [NonSerialized]
        private List<AssemblyName> _list;

        public AssemblyNameList(IEnumerable<AssemblyName> names)
        {
            _list = new List<AssemblyName>(names);
        }

        public AssemblyNameList()
        {
            _list = new List<AssemblyName>();
        }

        public int IndexOf(AssemblyName item)
        {
            return _list.IndexOf(item);
        }

        public void Insert(int index, AssemblyName item)
        {
            _list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }

        public AssemblyName this[int index]
        {
            get
            {
                return _list[index];
            }
            set
            {
                _list[index] = value;
            }
        }

        public void Add(AssemblyName item)
        {
            _list.Add(item);
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(AssemblyName item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(AssemblyName[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(AssemblyName item)
        {
            return _list.Remove(item);
        }

        public IEnumerator<AssemblyName> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _list.GetEnumerator();        
        }

        private AssemblyNameList(SerializationInfo info, StreamingContext context)
        {
            string[] names = info.GetValue("names", typeof(string[])) as string[];

            _list = new List<AssemblyName>();
            if(names != null)
            {
                foreach(string name in names)
                {
                    _list.Add(new AssemblyName(name));
                }
            }
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            string[] names = new string[_list.Count];
            for(int i = 0; i < _list.Count; ++i)
            {
                names[i] = _list[i].FullName;
            }
            info.AddValue("names", names);
        }
    }
}
