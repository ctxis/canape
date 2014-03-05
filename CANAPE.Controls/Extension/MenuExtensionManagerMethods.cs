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
using System.Text;
using System.Windows.Forms;

namespace CANAPE.Extension
{
    /// <summary>
    /// Extension methods for menu managers
    /// </summary>
    public static class MenuExtensionManagerMethods
    {
        private static void PopulateDictionary(Dictionary<string, ToolStripMenuItem> categories, string currentName, ToolStripMenuItem root)
        {
            foreach (ToolStripMenuItem item in root.DropDownItems)
            {
                StringBuilder builder = new StringBuilder(currentName);
                builder.Append(item.Name.ToLowerInvariant());
                categories[builder.ToString()] = item;
                builder.Append("/");
                PopulateDictionary(categories, builder.ToString(), item);
            }
        }        

        /// <summary>
        /// A method to add the menu values to a tool strip menu
        /// </summary>
        /// <param name="manager">The manager</param>
        /// <param name="createItem">A method to create top level items</param>
        /// <param name="extMenu">The extension menu strip</param>
        /// <param name="clickHandler">The click handler to associate with the items</param>
        private static void AddToMenu<T, U>(GenericExtensionManager<T, U> manager, Func<string, ToolStripMenuItem> createItem, 
            ToolStripMenuItem extMenu, EventHandler clickHandler) where T : MenuExtensionAttribute
        {
            Dictionary<string, ToolStripMenuItem> categories = new Dictionary<string, ToolStripMenuItem>();

            PopulateDictionary(categories, "", extMenu);

            foreach (var ext in manager.GetExtensions())
            {
                ToolStripMenuItem subItem = extMenu;
                ToolStripMenuItem item = new ToolStripMenuItem(ext.ExtensionAttribute.Name);
                item.Click += clickHandler;
                item.Tag = ext.ExtensionType;

                if (ext.ExtensionAttribute.ShortcutKeys != Keys.None)
                {
                    item.ShortcutKeys = ext.ExtensionAttribute.ShortcutKeys;
                    item.ShowShortcutKeys = true;
                }

                if (!String.IsNullOrWhiteSpace(ext.Category))
                {
                    string catName = ext.Category.ToLowerInvariant();

                    if (!categories.ContainsKey(catName))
                    {
                        string[] origNames = ext.Category.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                        string[] lowerNames = catName.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                        if (categories.ContainsKey(lowerNames[0]))
                        {
                            subItem = categories[lowerNames[0]];
                        }
                        else
                        {
                            subItem = createItem(origNames[0]);
                            categories[lowerNames[0]] = subItem;
                        }

                        StringBuilder currName = new StringBuilder(lowerNames[0]);                        

                        for (int i = 1; i < origNames.Length; ++i)
                        {
                            currName.AppendFormat("/{0}", lowerNames[i]);
                            string newName = currName.ToString();

                            if (categories.ContainsKey(newName))
                            {
                                subItem = categories[newName];
                            }
                            else
                            {
                                subItem = (ToolStripMenuItem)subItem.DropDownItems.Add(origNames[i]);
                                categories[newName] = subItem;
                            }
                        }

                        categories[catName] = subItem;                        
                    }
                    else
                    {
                        subItem = categories[catName];
                    }
                }

                subItem.DropDownItems.Add(item);
            }
        }

        /// <summary>
        /// A method to add the menu values to a tool strip menu
        /// </summary>
        /// <param name="manager">The manager</param>
        /// <param name="rootMenu">The root menu strip</param>
        /// <param name="clickHandler">The click handler to associate with the items</param>
        public static void AddToMenu<T, U>(this GenericExtensionManager<T, U> manager, ToolStripMenuItem rootMenu, EventHandler clickHandler) where T : MenuExtensionAttribute
        {
            AddToMenu(manager, s => (ToolStripMenuItem)rootMenu.DropDownItems.Add(s), rootMenu, clickHandler);            
        }

        /// <summary>
        /// A method to add the menu values to a tool strip menu
        /// </summary>
        /// <param name="manager">The manager</param>
        /// <param name="rootMenu">The root menu strip</param>
        /// <param name="clickHandler">The click handler to associate with the items</param>
        public static void AddToMenu<T, U>(this GenericExtensionManager<T, U> manager, MenuStrip rootMenu, ToolStripMenuItem extensionMenuItem, EventHandler clickHandler) where T : MenuExtensionAttribute
        {
            AddToMenu(manager, s => (ToolStripMenuItem)rootMenu.Items.Add(s), extensionMenuItem, clickHandler);
        }
    }
}
