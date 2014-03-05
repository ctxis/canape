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
using System.Windows.Forms;
using CANAPE.Controls;

namespace CANAPE.Forms
{
    public partial class ObjectCollectionForm : Form
    {
        List<object> _objs;

        private class TypeEntry
        {
            string _name;
            Func<object> _createObject;

            public TypeEntry(string name, Func<object> createObject)
            {
                _name = name;
                _createObject = createObject;
            }

            public object CreateObject()
            {
                return _createObject();
            }

            public override string ToString()
            {
                return _name;
            }
        }

        public IEnumerable<object> Objects
        {
            get { return _objs.AsEnumerable(); }
        }

        private static IEnumerable<TypeEntry> ConvertTypeDictionary(Dictionary<string, Type> dict)
        {
            List<TypeEntry> ret = new List<TypeEntry>();

            foreach (KeyValuePair<string, Type> pair in dict)
            {
                ret.Add(new TypeEntry(pair.Key, () => Activator.CreateInstance(pair.Value)));
            }

            return ret;
        }

        public ObjectCollectionForm(IEnumerable<object> objs, Dictionary<string, Type> typeMap) 
            : this(objs, typeMap.Select(p => new TypeEntry(p.Key, () => Activator.CreateInstance(p.Value))))
        {            
        }

        public ObjectCollectionForm(IEnumerable<object> objs, Dictionary<string, Func<object>> funcMap) 
            : this(objs, funcMap.Select(p => new TypeEntry(p.Key, p.Value)))
        {
        }

        private ObjectCollectionForm(IEnumerable<object> objs, IEnumerable<TypeEntry> types)
        {
            InitializeComponent();

            foreach (TypeEntry t in types)
            {
                comboBoxTypes.Items.Add(t);
            }

            if (comboBoxTypes.Items.Count > 0)
            {
                comboBoxTypes.SelectedItem = comboBoxTypes.Items[0];
            }

            foreach (object o in objs)
            {
                listBoxItems.Items.Add(o);
            }

            _objs = new List<object>(objs);

            if (listBoxItems.Items.Count > 0)
            {
                listBoxItems.SelectedIndex = 0;
                propertyGrid.SelectedObject = _objs[0];
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (comboBoxTypes.SelectedItem != null)
            {
                TypeEntry ent = (TypeEntry)comboBoxTypes.SelectedItem;
                object factory = ent.CreateObject();
                int idx = listBoxItems.Items.Add(factory);
                listBoxItems.SelectedIndex = idx;
                propertyGrid.SelectedObject = factory;
                _objs.Add(factory);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (listBoxItems.SelectedItem != null)
            {
                object item = listBoxItems.SelectedItem;

                _objs.Remove(listBoxItems.SelectedItem);
                listBoxItems.Items.Remove(item);

                if (propertyGrid.SelectedObject == item)
                {
                    propertyGrid.SelectedObject = null;
                }
            }
        }

        private void buttonFilterUp_Click(object sender, EventArgs e)
        {
            int idx = listBoxItems.SelectedIndex;

            if (idx > 0)
            {
                object o = listBoxItems.SelectedItem;
                listBoxItems.Items.RemoveAt(idx);
                listBoxItems.Items.Insert(idx - 1, o);

                _objs.RemoveAt(idx);
                _objs.Insert(idx - 1, o);
                listBoxItems.SelectedIndex = idx - 1;
            }
        }

        private void buttonFilterDown_Click(object sender, EventArgs e)
        {
            int idx = listBoxItems.SelectedIndex;

            if ((idx >= 0) && (idx < (listBoxItems.Items.Count - 1)))
            {
                object o = listBoxItems.SelectedItem;
                listBoxItems.Items.RemoveAt(idx);
                listBoxItems.Items.Insert(idx + 1, o);
                _objs.RemoveAt(idx);
                _objs.Insert(idx + 1, o);
                listBoxItems.SelectedIndex = idx + 1;
            }
        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            int idx = listBoxItems.Items.IndexOf(propertyGrid.SelectedObject);

            if (idx >= 0)
            {
                object o = propertyGrid.SelectedObject;
                listBoxItems.Items.RemoveAt(idx);
                listBoxItems.Items.Insert(idx, o);
            }
        }

        private void listBoxItems_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBoxItems.SelectedItem != null)
            {
                propertyGrid.SelectedObject = listBoxItems.SelectedItem;
            }
        }

    }
}
