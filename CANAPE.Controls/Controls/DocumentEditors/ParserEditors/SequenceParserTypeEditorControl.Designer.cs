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

namespace CANAPE.Controls.DocumentEditors.ParserEditors
{
    partial class SequenceParserTypeEditorControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ColumnHeader columnHeaderName;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SequenceParserTypeEditorControl));
            System.Windows.Forms.ColumnHeader columnHeaderType;
            System.Windows.Forms.ColumnHeader columnHeaderSize;
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addPrimitiveTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signedByteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signed16BitIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signed24BitIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signed32BitIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.signed64BitIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsignedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsigned16BitIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsigned24BitIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsigned32BitIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unsigned64BitIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitVariableIntToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.floatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.doubleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixedCharLengthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToEndToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lengthReferenceToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.terminatedStringToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addFixedBytesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSequenceChoiceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToArrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixedLengthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readToEndToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lengthReferenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToBooleanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToCalculatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToSizedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToEnumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.convertToBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonEntryUp = new System.Windows.Forms.Button();
            this.buttonEntryDown = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.listViewEntries = new CANAPE.Controls.ListViewExtension();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.addByteArrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixedLengthToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.readToEndToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.lengthReferenceToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.terminatedToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            columnHeaderSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeaderName
            // 
            resources.ApplyResources(columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderType
            // 
            resources.ApplyResources(columnHeaderType, "columnHeaderType");
            // 
            // columnHeaderSize
            // 
            resources.ApplyResources(columnHeaderSize, "columnHeaderSize");
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addPrimitiveTypeToolStripMenuItem,
            this.addStringToolStripMenuItem,
            this.addFixedBytesToolStripMenuItem,
            this.addByteArrayToolStripMenuItem,
            this.addSequenceToolStripMenuItem,
            this.addSequenceChoiceToolStripMenuItem,
            this.convertToArrayToolStripMenuItem,
            this.convertToBooleanToolStripMenuItem,
            this.convertToCalculatedToolStripMenuItem,
            this.convertToSizedToolStripMenuItem,
            this.convertToEnumToolStripMenuItem,
            this.convertToBaseToolStripMenuItem,
            this.toolStripSeparator1,
            this.deleteToolStripMenuItem,
            this.renameToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // addPrimitiveTypeToolStripMenuItem
            // 
            this.addPrimitiveTypeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signedToolStripMenuItem,
            this.unsignedToolStripMenuItem,
            this.bitVariableIntToolStripMenuItem,
            this.floatToolStripMenuItem,
            this.doubleToolStripMenuItem,
            this.bitFieldToolStripMenuItem});
            this.addPrimitiveTypeToolStripMenuItem.Name = "addPrimitiveTypeToolStripMenuItem";
            resources.ApplyResources(this.addPrimitiveTypeToolStripMenuItem, "addPrimitiveTypeToolStripMenuItem");
            // 
            // signedToolStripMenuItem
            // 
            this.signedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signedByteToolStripMenuItem,
            this.signed16BitIntToolStripMenuItem,
            this.signed24BitIntToolStripMenuItem,
            this.signed32BitIntToolStripMenuItem,
            this.signed64BitIntToolStripMenuItem});
            this.signedToolStripMenuItem.Name = "signedToolStripMenuItem";
            resources.ApplyResources(this.signedToolStripMenuItem, "signedToolStripMenuItem");
            // 
            // signedByteToolStripMenuItem
            // 
            this.signedByteToolStripMenuItem.Name = "signedByteToolStripMenuItem";
            resources.ApplyResources(this.signedByteToolStripMenuItem, "signedByteToolStripMenuItem");
            this.signedByteToolStripMenuItem.Tag = "System.SByte";
            this.signedByteToolStripMenuItem.Click += new System.EventHandler(this.addIntegerType_Click);
            // 
            // signed16BitIntToolStripMenuItem
            // 
            this.signed16BitIntToolStripMenuItem.Name = "signed16BitIntToolStripMenuItem";
            resources.ApplyResources(this.signed16BitIntToolStripMenuItem, "signed16BitIntToolStripMenuItem");
            this.signed16BitIntToolStripMenuItem.Tag = "System.Int16";
            this.signed16BitIntToolStripMenuItem.Click += new System.EventHandler(this.addIntegerType_Click);
            // 
            // signed24BitIntToolStripMenuItem
            // 
            this.signed24BitIntToolStripMenuItem.Name = "signed24BitIntToolStripMenuItem";
            resources.ApplyResources(this.signed24BitIntToolStripMenuItem, "signed24BitIntToolStripMenuItem");
            this.signed24BitIntToolStripMenuItem.Tag = "CANAPE.DataFrames.Int24";
            this.signed24BitIntToolStripMenuItem.Click += new System.EventHandler(this.signed24BitIntToolStripMenuItem_Click);
            // 
            // signed32BitIntToolStripMenuItem
            // 
            this.signed32BitIntToolStripMenuItem.Name = "signed32BitIntToolStripMenuItem";
            resources.ApplyResources(this.signed32BitIntToolStripMenuItem, "signed32BitIntToolStripMenuItem");
            this.signed32BitIntToolStripMenuItem.Tag = "System.Int32";
            this.signed32BitIntToolStripMenuItem.Click += new System.EventHandler(this.addIntegerType_Click);
            // 
            // signed64BitIntToolStripMenuItem
            // 
            this.signed64BitIntToolStripMenuItem.Name = "signed64BitIntToolStripMenuItem";
            resources.ApplyResources(this.signed64BitIntToolStripMenuItem, "signed64BitIntToolStripMenuItem");
            this.signed64BitIntToolStripMenuItem.Tag = "System.Int64";
            this.signed64BitIntToolStripMenuItem.Click += new System.EventHandler(this.addIntegerType_Click);
            // 
            // unsignedToolStripMenuItem
            // 
            this.unsignedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.byteToolStripMenuItem,
            this.unsigned16BitIntToolStripMenuItem,
            this.unsigned24BitIntToolStripMenuItem,
            this.unsigned32BitIntToolStripMenuItem,
            this.unsigned64BitIntToolStripMenuItem});
            this.unsignedToolStripMenuItem.Name = "unsignedToolStripMenuItem";
            resources.ApplyResources(this.unsignedToolStripMenuItem, "unsignedToolStripMenuItem");
            // 
            // byteToolStripMenuItem
            // 
            this.byteToolStripMenuItem.Name = "byteToolStripMenuItem";
            resources.ApplyResources(this.byteToolStripMenuItem, "byteToolStripMenuItem");
            this.byteToolStripMenuItem.Tag = "System.Byte";
            this.byteToolStripMenuItem.Click += new System.EventHandler(this.addIntegerType_Click);
            // 
            // unsigned16BitIntToolStripMenuItem
            // 
            this.unsigned16BitIntToolStripMenuItem.Name = "unsigned16BitIntToolStripMenuItem";
            resources.ApplyResources(this.unsigned16BitIntToolStripMenuItem, "unsigned16BitIntToolStripMenuItem");
            this.unsigned16BitIntToolStripMenuItem.Tag = "System.UInt16";
            this.unsigned16BitIntToolStripMenuItem.Click += new System.EventHandler(this.addIntegerType_Click);
            // 
            // unsigned24BitIntToolStripMenuItem
            // 
            this.unsigned24BitIntToolStripMenuItem.Name = "unsigned24BitIntToolStripMenuItem";
            resources.ApplyResources(this.unsigned24BitIntToolStripMenuItem, "unsigned24BitIntToolStripMenuItem");
            this.unsigned24BitIntToolStripMenuItem.Tag = "CANAPE.DataFrames.UInt24";
            this.unsigned24BitIntToolStripMenuItem.Click += new System.EventHandler(this.unsigned24BitIntToolStripMenuItem_Click);
            // 
            // unsigned32BitIntToolStripMenuItem
            // 
            this.unsigned32BitIntToolStripMenuItem.Name = "unsigned32BitIntToolStripMenuItem";
            resources.ApplyResources(this.unsigned32BitIntToolStripMenuItem, "unsigned32BitIntToolStripMenuItem");
            this.unsigned32BitIntToolStripMenuItem.Tag = "System.UInt32";
            this.unsigned32BitIntToolStripMenuItem.Click += new System.EventHandler(this.addIntegerType_Click);
            // 
            // unsigned64BitIntToolStripMenuItem
            // 
            this.unsigned64BitIntToolStripMenuItem.Name = "unsigned64BitIntToolStripMenuItem";
            resources.ApplyResources(this.unsigned64BitIntToolStripMenuItem, "unsigned64BitIntToolStripMenuItem");
            this.unsigned64BitIntToolStripMenuItem.Tag = "System.UInt64";
            this.unsigned64BitIntToolStripMenuItem.Click += new System.EventHandler(this.addIntegerType_Click);
            // 
            // bitVariableIntToolStripMenuItem
            // 
            this.bitVariableIntToolStripMenuItem.Name = "bitVariableIntToolStripMenuItem";
            resources.ApplyResources(this.bitVariableIntToolStripMenuItem, "bitVariableIntToolStripMenuItem");
            this.bitVariableIntToolStripMenuItem.Click += new System.EventHandler(this.bitVariableIntToolStripMenuItem_Click);
            // 
            // floatToolStripMenuItem
            // 
            this.floatToolStripMenuItem.Name = "floatToolStripMenuItem";
            resources.ApplyResources(this.floatToolStripMenuItem, "floatToolStripMenuItem");
            this.floatToolStripMenuItem.Tag = "System.Single";
            this.floatToolStripMenuItem.Click += new System.EventHandler(this.addFloatType_Click);
            // 
            // doubleToolStripMenuItem
            // 
            this.doubleToolStripMenuItem.Name = "doubleToolStripMenuItem";
            resources.ApplyResources(this.doubleToolStripMenuItem, "doubleToolStripMenuItem");
            this.doubleToolStripMenuItem.Tag = "System.Double";
            this.doubleToolStripMenuItem.Click += new System.EventHandler(this.addFloatType_Click);
            // 
            // bitFieldToolStripMenuItem
            // 
            this.bitFieldToolStripMenuItem.Name = "bitFieldToolStripMenuItem";
            resources.ApplyResources(this.bitFieldToolStripMenuItem, "bitFieldToolStripMenuItem");
            this.bitFieldToolStripMenuItem.Click += new System.EventHandler(this.bitFieldToolStripMenuItem_Click);
            // 
            // addStringToolStripMenuItem
            // 
            this.addStringToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixedCharLengthToolStripMenuItem,
            this.readToEndToolStripMenuItem,
            this.lengthReferenceToolStripMenuItem1,
            this.terminatedStringToolStripMenuItem});
            this.addStringToolStripMenuItem.Name = "addStringToolStripMenuItem";
            resources.ApplyResources(this.addStringToolStripMenuItem, "addStringToolStripMenuItem");
            // 
            // fixedCharLengthToolStripMenuItem
            // 
            this.fixedCharLengthToolStripMenuItem.Name = "fixedCharLengthToolStripMenuItem";
            resources.ApplyResources(this.fixedCharLengthToolStripMenuItem, "fixedCharLengthToolStripMenuItem");
            this.fixedCharLengthToolStripMenuItem.Click += new System.EventHandler(this.fixedCharLengthToolStripMenuItem_Click);
            // 
            // readToEndToolStripMenuItem
            // 
            this.readToEndToolStripMenuItem.Name = "readToEndToolStripMenuItem";
            resources.ApplyResources(this.readToEndToolStripMenuItem, "readToEndToolStripMenuItem");
            this.readToEndToolStripMenuItem.Click += new System.EventHandler(this.readToEndToolStripMenuItem_Click);
            // 
            // lengthReferenceToolStripMenuItem1
            // 
            this.lengthReferenceToolStripMenuItem1.Name = "lengthReferenceToolStripMenuItem1";
            resources.ApplyResources(this.lengthReferenceToolStripMenuItem1, "lengthReferenceToolStripMenuItem1");
            this.lengthReferenceToolStripMenuItem1.Click += new System.EventHandler(this.lengthReferenceToolStripMenuItem1_Click);
            // 
            // terminatedStringToolStripMenuItem
            // 
            this.terminatedStringToolStripMenuItem.Name = "terminatedStringToolStripMenuItem";
            resources.ApplyResources(this.terminatedStringToolStripMenuItem, "terminatedStringToolStripMenuItem");
            this.terminatedStringToolStripMenuItem.Click += new System.EventHandler(this.terminatedStringToolStripMenuItem_Click);
            // 
            // addFixedBytesToolStripMenuItem
            // 
            this.addFixedBytesToolStripMenuItem.Name = "addFixedBytesToolStripMenuItem";
            resources.ApplyResources(this.addFixedBytesToolStripMenuItem, "addFixedBytesToolStripMenuItem");
            this.addFixedBytesToolStripMenuItem.Click += new System.EventHandler(this.addFixedBytesToolStripMenuItem_Click);
            // 
            // addSequenceToolStripMenuItem
            // 
            this.addSequenceToolStripMenuItem.Name = "addSequenceToolStripMenuItem";
            resources.ApplyResources(this.addSequenceToolStripMenuItem, "addSequenceToolStripMenuItem");
            // 
            // addSequenceChoiceToolStripMenuItem
            // 
            this.addSequenceChoiceToolStripMenuItem.Name = "addSequenceChoiceToolStripMenuItem";
            resources.ApplyResources(this.addSequenceChoiceToolStripMenuItem, "addSequenceChoiceToolStripMenuItem");
            this.addSequenceChoiceToolStripMenuItem.Click += new System.EventHandler(this.addSequenceChoiceToolStripMenuItem_Click);
            // 
            // convertToArrayToolStripMenuItem
            // 
            this.convertToArrayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixedLengthToolStripMenuItem,
            this.readToEndToolStripMenuItem1,
            this.lengthReferenceToolStripMenuItem,
            this.terminatedToolStripMenuItem});
            this.convertToArrayToolStripMenuItem.Name = "convertToArrayToolStripMenuItem";
            resources.ApplyResources(this.convertToArrayToolStripMenuItem, "convertToArrayToolStripMenuItem");
            // 
            // fixedLengthToolStripMenuItem
            // 
            this.fixedLengthToolStripMenuItem.Name = "fixedLengthToolStripMenuItem";
            resources.ApplyResources(this.fixedLengthToolStripMenuItem, "fixedLengthToolStripMenuItem");
            this.fixedLengthToolStripMenuItem.Click += new System.EventHandler(this.fixedLengthToolStripMenuItem_Click);
            // 
            // readToEndToolStripMenuItem1
            // 
            this.readToEndToolStripMenuItem1.Name = "readToEndToolStripMenuItem1";
            resources.ApplyResources(this.readToEndToolStripMenuItem1, "readToEndToolStripMenuItem1");
            this.readToEndToolStripMenuItem1.Click += new System.EventHandler(this.readToEndArrayToolStripMenuItem_Click);
            // 
            // lengthReferenceToolStripMenuItem
            // 
            this.lengthReferenceToolStripMenuItem.Name = "lengthReferenceToolStripMenuItem";
            resources.ApplyResources(this.lengthReferenceToolStripMenuItem, "lengthReferenceToolStripMenuItem");
            this.lengthReferenceToolStripMenuItem.Click += new System.EventHandler(this.lengthReferenceToolStripMenuItem_Click);
            // 
            // terminatedToolStripMenuItem
            // 
            this.terminatedToolStripMenuItem.Name = "terminatedToolStripMenuItem";
            resources.ApplyResources(this.terminatedToolStripMenuItem, "terminatedToolStripMenuItem");
            this.terminatedToolStripMenuItem.Click += new System.EventHandler(this.terminatedToolStripMenuItem_Click);
            // 
            // convertToBooleanToolStripMenuItem
            // 
            this.convertToBooleanToolStripMenuItem.Name = "convertToBooleanToolStripMenuItem";
            resources.ApplyResources(this.convertToBooleanToolStripMenuItem, "convertToBooleanToolStripMenuItem");
            this.convertToBooleanToolStripMenuItem.Click += new System.EventHandler(this.convertToBooleanToolStripMenuItem_Click);
            // 
            // convertToCalculatedToolStripMenuItem
            // 
            this.convertToCalculatedToolStripMenuItem.Name = "convertToCalculatedToolStripMenuItem";
            resources.ApplyResources(this.convertToCalculatedToolStripMenuItem, "convertToCalculatedToolStripMenuItem");
            this.convertToCalculatedToolStripMenuItem.Click += new System.EventHandler(this.convertToCalculatedToolStripMenuItem_Click);
            // 
            // convertToSizedToolStripMenuItem
            // 
            this.convertToSizedToolStripMenuItem.Name = "convertToSizedToolStripMenuItem";
            resources.ApplyResources(this.convertToSizedToolStripMenuItem, "convertToSizedToolStripMenuItem");
            this.convertToSizedToolStripMenuItem.Click += new System.EventHandler(this.convertToSizedToolStripMenuItem_Click);
            // 
            // convertToEnumToolStripMenuItem
            // 
            this.convertToEnumToolStripMenuItem.Name = "convertToEnumToolStripMenuItem";
            resources.ApplyResources(this.convertToEnumToolStripMenuItem, "convertToEnumToolStripMenuItem");
            // 
            // convertToBaseToolStripMenuItem
            // 
            this.convertToBaseToolStripMenuItem.Name = "convertToBaseToolStripMenuItem";
            resources.ApplyResources(this.convertToBaseToolStripMenuItem, "convertToBaseToolStripMenuItem");
            this.convertToBaseToolStripMenuItem.Click += new System.EventHandler(this.convertToBaseToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            resources.ApplyResources(this.renameToolStripMenuItem, "renameToolStripMenuItem");
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            resources.ApplyResources(this.addToolStripMenuItem, "addToolStripMenuItem");
            // 
            // buttonEntryUp
            // 
            resources.ApplyResources(this.buttonEntryUp, "buttonEntryUp");
            this.buttonEntryUp.Name = "buttonEntryUp";
            this.toolTip.SetToolTip(this.buttonEntryUp, resources.GetString("buttonEntryUp.ToolTip"));
            this.buttonEntryUp.UseVisualStyleBackColor = true;
            this.buttonEntryUp.Click += new System.EventHandler(this.buttonEntryUp_Click);
            // 
            // buttonEntryDown
            // 
            resources.ApplyResources(this.buttonEntryDown, "buttonEntryDown");
            this.buttonEntryDown.Name = "buttonEntryDown";
            this.toolTip.SetToolTip(this.buttonEntryDown, resources.GetString("buttonEntryDown.ToolTip"));
            this.buttonEntryDown.UseVisualStyleBackColor = true;
            this.buttonEntryDown.Click += new System.EventHandler(this.buttonEntryDown_Click);
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.listViewEntries);
            this.splitContainer.Panel1.Controls.Add(this.buttonEntryUp);
            this.splitContainer.Panel1.Controls.Add(this.buttonEntryDown);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.propertyGrid);
            // 
            // listViewEntries
            // 
            resources.ApplyResources(this.listViewEntries, "listViewEntries");
            this.listViewEntries.AutoScrollList = false;
            this.listViewEntries.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeaderName,
            columnHeaderType,
            columnHeaderSize});
            this.listViewEntries.ContextMenuStrip = this.contextMenuStrip;
            this.listViewEntries.FullRowSelect = true;
            this.listViewEntries.GridLines = true;
            this.listViewEntries.HideSelection = false;
            this.listViewEntries.LabelEdit = true;
            this.listViewEntries.MultiSelect = false;
            this.listViewEntries.Name = "listViewEntries";
            this.toolTip.SetToolTip(this.listViewEntries, resources.GetString("listViewEntries.ToolTip"));
            this.listViewEntries.UseCompatibleStateImageBehavior = false;
            this.listViewEntries.View = System.Windows.Forms.View.Details;
            this.listViewEntries.AfterLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewEntries_AfterLabelEdit);
            this.listViewEntries.BeforeLabelEdit += new System.Windows.Forms.LabelEditEventHandler(this.listViewEntries_BeforeLabelEdit);
            this.listViewEntries.SelectedIndexChanged += new System.EventHandler(this.listViewEntries_SelectedIndexChanged);
            // 
            // propertyGrid
            // 
            resources.ApplyResources(this.propertyGrid, "propertyGrid");
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // addByteArrayToolStripMenuItem
            // 
            this.addByteArrayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixedLengthToolStripMenuItem1,
            this.readToEndToolStripMenuItem2,
            this.lengthReferenceToolStripMenuItem2,
            this.terminatedToolStripMenuItem1});
            this.addByteArrayToolStripMenuItem.Name = "addByteArrayToolStripMenuItem";
            resources.ApplyResources(this.addByteArrayToolStripMenuItem, "addByteArrayToolStripMenuItem");
            // 
            // fixedLengthToolStripMenuItem1
            // 
            this.fixedLengthToolStripMenuItem1.Name = "fixedLengthToolStripMenuItem1";
            resources.ApplyResources(this.fixedLengthToolStripMenuItem1, "fixedLengthToolStripMenuItem1");
            this.fixedLengthToolStripMenuItem1.Click += new System.EventHandler(this.fixedLengthToolStripMenuItem1_Click);
            // 
            // readToEndToolStripMenuItem2
            // 
            this.readToEndToolStripMenuItem2.Name = "readToEndToolStripMenuItem2";
            resources.ApplyResources(this.readToEndToolStripMenuItem2, "readToEndToolStripMenuItem2");
            this.readToEndToolStripMenuItem2.Click += new System.EventHandler(this.readToEndToolStripMenuItem2_Click);
            // 
            // lengthReferenceToolStripMenuItem2
            // 
            this.lengthReferenceToolStripMenuItem2.Name = "lengthReferenceToolStripMenuItem2";
            resources.ApplyResources(this.lengthReferenceToolStripMenuItem2, "lengthReferenceToolStripMenuItem2");
            this.lengthReferenceToolStripMenuItem2.Click += new System.EventHandler(this.lengthReferenceToolStripMenuItem2_Click);
            // 
            // terminatedToolStripMenuItem1
            // 
            this.terminatedToolStripMenuItem1.Name = "terminatedToolStripMenuItem1";
            resources.ApplyResources(this.terminatedToolStripMenuItem1, "terminatedToolStripMenuItem1");
            this.terminatedToolStripMenuItem1.Click += new System.EventHandler(this.terminatedToolStripMenuItem1_Click);
            // 
            // SequenceParserTypeEditorControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Name = "SequenceParserTypeEditorControl";
            this.Load += new System.EventHandler(this.SequenceEditorControl_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addPrimitiveTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signedByteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signed16BitIntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsigned16BitIntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signed32BitIntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsigned32BitIntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signed64BitIntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsigned64BitIntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bitVariableIntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem floatToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem doubleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsigned24BitIntToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signed24BitIntToolStripMenuItem;
        private ListViewExtension listViewEntries;
        private System.Windows.Forms.Button buttonEntryUp;
        private System.Windows.Forms.Button buttonEntryDown;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStripMenuItem bitFieldToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixedCharLengthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToEndToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem signedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unsignedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSequenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToArrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixedLengthToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readToEndToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem lengthReferenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lengthReferenceToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem terminatedStringToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToBaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToBooleanToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToEnumToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripMenuItem convertToCalculatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminatedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSequenceChoiceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem convertToSizedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addFixedBytesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addByteArrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixedLengthToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem readToEndToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem lengthReferenceToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem terminatedToolStripMenuItem1;
    }
}
