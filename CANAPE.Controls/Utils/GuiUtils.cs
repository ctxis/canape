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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;
using CANAPE.Controls;
using CANAPE.Documents;
using CANAPE.Documents.Data;
using CANAPE.Documents.Net;
using CANAPE.Nodes;
using CANAPE.Security;

namespace CANAPE
{
    public static class GuiUtils
    {
        public static void FromEnum(this ComboBox comboBox, Enum currProtocol) 
        {
            Type enumType = currProtocol.GetType();
            object setValue = currProtocol;

            comboBox.Items.Clear();
            foreach (object sslValue in Enum.GetValues(enumType))
            {
                comboBox.Items.Add(sslValue);
            }

            if (!Enum.IsDefined(enumType, currProtocol))
            {
                setValue = Activator.CreateInstance(enumType);
            }

            comboBox.SelectedItem = setValue;
        }

        public static void CreatePacketLogCopyItems(ToolStripItemCollection items, Func<IEnumerable<LogPacket>> createPackets)
        {
            ToolStripMenuItem packetLog = new ToolStripMenuItem(Properties.Resources.GuiUtils_CopyToPacketLog);
            ToolStripMenuItem testDoc = new ToolStripMenuItem(Properties.Resources.GuiUtils_CopyToTestDocument);
            ToolStripMenuItem diffDoc = new ToolStripMenuItem(Properties.Resources.GuiUtils_CopyToDiffDocument);

            ToolStripMenuItem item = new ToolStripMenuItem(Properties.Resources.GuiUtils_NewDocument);
            item.Click += (s, e) => newLog_Click(s, createPackets);
            packetLog.DropDownItems.Add(item);

            item = new ToolStripMenuItem(Properties.Resources.GuiUtils_NewDocument);            
            diffDoc.DropDownItems.Add(item);

            ToolStripMenuItem subItem = new ToolStripMenuItem(Properties.Resources.GuiUtils_LeftDiffDocument);
            subItem.Click += (s, e) => newDiffLog_Click(s, createPackets, true);
            item.DropDownItems.Add(subItem);

            subItem = new ToolStripMenuItem(Properties.Resources.GuiUtils_RightDiffDocument);
            subItem.Click += (s, e) => newDiffLog_Click(s, createPackets, false);
            item.DropDownItems.Add(subItem);

            foreach (IDocumentObject doc in CANAPEProject.CurrentProject.Documents)
            {
                if ((doc is TestDocument) || (doc is PacketLogDocument))
                {
                    item = new ToolStripMenuItem(doc.Name);
                    item.Click += (s, e) => addToExisting_Click(s, createPackets);
                    item.Tag = doc;

                    if (doc is PacketLogDocument)
                    {
                        packetLog.DropDownItems.Add(item);
                    }
                    else
                    {
                        testDoc.DropDownItems.Add(item);
                    }                    
                }
                else if (doc is PacketLogDiffDocument)
                {
                    item = new ToolStripMenuItem(doc.Name);
                    diffDoc.DropDownItems.Add(item);

                    subItem = new ToolStripMenuItem(Properties.Resources.GuiUtils_LeftDiffDocument);
                    subItem.Click += (s, e) => addToExistingDiffLog_Click(s, createPackets, true);
                    subItem.Tag = doc;
                    item.DropDownItems.Add(subItem);

                    subItem = new ToolStripMenuItem(Properties.Resources.GuiUtils_RightDiffDocument);
                    subItem.Click += (s, e) => addToExistingDiffLog_Click(s, createPackets, false);
                    subItem.Tag = doc;
                    item.DropDownItems.Add(subItem);
                }
            }

            items.Add(packetLog);
            items.Add(diffDoc);
            if (testDoc.DropDownItems.Count > 0)
            {
                items.Add(testDoc);
            }            
        }

        public static void CreateScriptMenuItems(ToolStripItemCollection items, Action<ScriptDocument> clickHandler)
        {
            items.Clear();

            foreach(ScriptDocument doc in CANAPEProject.CurrentProject.GetDocumentsByType(typeof(ScriptDocument)))
            {
                ToolStripItem item = items.Add(doc.Name);
                item.Click += (o, e) => clickHandler(doc);
            }
        }

        static void AddPacketsToDocument(IDocumentObject doc, LogPacket[] packets)
        {           
            PacketLogDocument packetLog = doc as PacketLogDocument;
            TestDocument testDoc = doc as TestDocument;

            if (packetLog != null)
            {
                foreach (LogPacket packet in packets)
                {
                    packetLog.AddPacket((LogPacket)packet.Clone());
                }
            }
            else if (testDoc != null)
            {
                IEnumerable<LogPacket> newPackets = packets.Select(p => (LogPacket)p.Clone());
                testDoc.AddRangeInputPacket(newPackets);
            }
        }

        static void addToExisting_Click(object sender, Func<IEnumerable<LogPacket>> createPackets)
        {
            LogPacket[] packets = createPackets().ToArray();

            if (packets.Length > 0)
            {
                ToolStripItem item = sender as ToolStripItem;

                if (item != null)
                {
                    IDocumentObject doc = item.Tag as IDocumentObject;

                    if (doc != null)
                    {
                        AddPacketsToDocument(doc, packets);
                    }
                }
            }
        }        

        static void newLog_Click(object sender, Func<IEnumerable<LogPacket>> createPackets)
        {
            LogPacket[] packets = createPackets().ToArray();

            if (packets.Length > 0)
            {
                PacketLogDocument doc = CANAPEProject.CurrentProject.CreateDocument<PacketLogDocument>();

                AddPacketsToDocument(doc, packets);

                DocumentControl.Show(doc);
            }
        }

        static void AddToExistingDifflog(LogPacketCollection coll, LogPacket[] packets)
        {
            lock (coll)
            {
                foreach (LogPacket packet in packets)
                {
                    coll.Add(packet.ClonePacket());
                }
            }
        }

        static void addToExistingDiffLog_Click(object sender, Func<IEnumerable<LogPacket>> createPackets, bool left)
        {
            LogPacket[] packets = createPackets().ToArray();

            if (packets.Length > 0)
            {
                ToolStripItem item = sender as ToolStripItem;

                if (item != null)
                {
                    PacketLogDiffDocument doc = item.Tag as PacketLogDiffDocument;

                    if (doc != null)
                    {
                        LogPacketCollection coll = left ? doc.Left : doc.Right;

                        AddToExistingDifflog(coll, packets);
                    }
                }
            }
        }

        static void newDiffLog_Click(object sender, Func<IEnumerable<LogPacket>> createPackets, bool left)
        {
            LogPacket[] packets = createPackets().ToArray();

            if (packets.Length > 0)
            {
                PacketLogDiffDocument doc = CANAPEProject.CurrentProject.CreateDocument<PacketLogDiffDocument>();

                AddToExistingDifflog(left ? doc.Left : doc.Right, packets);

                DocumentControl.Show(doc);
            }
        }

        public static void OpenDocument(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(path);
            startInfo.UseShellExecute = true;
            startInfo.Verb = "open";

            try
            {
                Process.Start(startInfo);
            }
            catch (Win32Exception)
            {
            }
            catch (FileNotFoundException)
            {
            }
        }

        internal struct CREDUI_INFO
        {
            public int cbSize;
            public IntPtr hwndParent;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszMessageText;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszCaptionText;
            public IntPtr hbmBanner;
        }

        [DllImport("credui.dll", CharSet = CharSet.Unicode)]
        private static extern CredUIReturnCodes CredUIPromptForCredentials(ref CREDUI_INFO creditUR,
                string targetName,
                IntPtr reserved1,
                int iError,
                StringBuilder userName,
                int maxUserName,
                StringBuilder password,
                int maxPassword,
                [MarshalAs(UnmanagedType.Bool)] ref bool pfSave,
                CREDUI_FLAGS flags);

        [Flags]
        enum CREDUI_FLAGS
        {
            INCORRECT_PASSWORD = 0x1,
            DO_NOT_PERSIST = 0x2,
            REQUEST_ADMINISTRATOR = 0x4,
            EXCLUDE_CERTIFICATES = 0x8,
            REQUIRE_CERTIFICATE = 0x10,
            SHOW_SAVE_CHECK_BOX = 0x40,
            ALWAYS_SHOW_UI = 0x80,
            REQUIRE_SMARTCARD = 0x100,
            PASSWORD_ONLY_OK = 0x200,
            VALIDATE_USERNAME = 0x400,
            COMPLETE_USERNAME = 0x800,
            PERSIST = 0x1000,
            SERVER_CREDENTIAL = 0x4000,
            EXPECT_CONFIRMATION = 0x20000,
            GENERIC_CREDENTIALS = 0x40000,
            USERNAME_TARGET_CREDENTIALS = 0x80000,
            KEEP_USERNAME = 0x100000,
        }

        [Flags]
        enum CREDUI_FLAGS_NEW
        {
            CREDUIWIN_GENERIC = 0x1,
            CREDUIWIN_CHECKBOX = 0x2,
            CREDUIWIN_AUTHPACKAGE_ONLY = 0x10,
            CREDUIWIN_IN_CRED_ONLY = 0x20,
            CREDUIWIN_ENUMERATE_ADMINS = 0x100,
            CREDUIWIN_ENUMERATE_CURRENT_USER = 0x200,
            CREDUIWIN_SECURE_PROMPT = 0x1000,
            CREDUIWIN_PREPROMPTING = 0x2000,
            CREDUIWIN_PACK_32_WOW = 0x10000000
        }

        public enum CredUIReturnCodes
        {
            NO_ERROR = 0,
            ERROR_CANCELLED = 1223,
            ERROR_NO_SUCH_LOGON_SESSION = 1312,
            ERROR_NOT_FOUND = 1168,
            ERROR_INVALID_ACCOUNT_NAME = 1315,
            ERROR_INSUFFICIENT_BUFFER = 122,
            ERROR_INVALID_PARAMETER = 87,
            ERROR_INVALID_FLAGS = 1004,
        }

        [DllImport("credui.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredPackAuthenticationBuffer(
            uint   dwFlags,
            string pszUserName,
            string pszPassword,
            [Out] byte[] pPackedCredentials,
            out uint pcbPackedCredentials
        );

        [DllImport("credui.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredUnPackAuthenticationBuffer(
              uint dwFlags,
              IntPtr pAuthBuffer,
              uint cbAuthBuffer,
              [Out] StringBuilder pszUserName,
              ref uint pcchMaxUserName,
              [Out] StringBuilder pszDomainName,
              ref uint pcchMaxDomainname,
              [Out] StringBuilder pszPassword,
              ref uint pcchMaxPassword
            );

        [DllImport("credui.dll", SetLastError=true, CharSet=CharSet.Unicode)]
        private static extern CredUIReturnCodes CredUIPromptForWindowsCredentials(
                ref CREDUI_INFO creditUR,
                uint dwAuthError,
                ref uint pulAuthPackage,
                IntPtr pvInAuthBuffer,
                uint ulInAuthBufferSize,
                [Out] out IntPtr ppvOutAuthBuffer,
                out uint pulOutAuthBufferSize,
                [MarshalAs(UnmanagedType.Bool)] ref bool pfSave,
                CREDUI_FLAGS_NEW dwFlags
            );

        internal static void Test()
        {
            CREDUI_INFO credUI = new CREDUI_INFO();
            credUI.cbSize = Marshal.SizeOf(credUI);
            credUI.pszCaptionText = "Hello";
            credUI.pszMessageText = "There";
            CREDUI_FLAGS_NEW flags = CREDUI_FLAGS_NEW.CREDUIWIN_AUTHPACKAGE_ONLY;
            bool save = false;
            uint authPackage = 0;
            IntPtr p;
            uint psize = 0;

            if (CredUIPromptForWindowsCredentials(ref credUI, 0, ref authPackage, IntPtr.Zero, 0, out p, out psize, ref save, flags) == CredUIReturnCodes.NO_ERROR)
            {

            }
        }

        /// <summary>
        /// Prompts for password.
        /// </summary>
        /// <param name="user">The user, can specify an existing value</param>
        /// <param name="password">The password.</param>
        /// <returns>The authentication credentials result</returns>
        public static ResolveCredentialsResult PromptForPassword(SecurityPrincipal principal)
        {
            // Setup the flags and variables
            StringBuilder userPassword = new StringBuilder(100);
            StringBuilder userID = new StringBuilder(100);            
            CREDUI_INFO credUI = new CREDUI_INFO();
            credUI.cbSize = Marshal.SizeOf(credUI);          
            CREDUI_FLAGS flags = CREDUI_FLAGS.ALWAYS_SHOW_UI | CREDUI_FLAGS.GENERIC_CREDENTIALS;            
            bool save = false;
            
            // Prompt the user
            CredUIReturnCodes returnCode = CredUIPromptForCredentials(ref credUI, String.Format("{0}@{1}", principal.Name, principal.Realm), 
                IntPtr.Zero, 0, userID, 100, userPassword, 100, ref save, flags);

            if(returnCode == CredUIReturnCodes.NO_ERROR)
            {
                AuthenticationCredentials creds = new AuthenticationCredentials();
                string domain = String.Empty;
                string username = userID.ToString();
                string password = userPassword.ToString();

                if(username.Contains("\\"))
                {
                    string[] s = username.Split('\\');

                    username = s[0];
                    domain = s[1];
                }

                creds.Username = username;
                creds.Domain = domain;
                creds.Password = password;           

                return new ResolveCredentialsResult(creds, save);
            }
            else
            {
                return null;
            }
        }
   
    }
}
