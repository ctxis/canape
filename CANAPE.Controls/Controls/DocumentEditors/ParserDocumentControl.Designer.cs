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

namespace CANAPE.Controls.DocumentEditors
{
    partial class ParserDocumentControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ParserDocumentControl));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeViewTypes = new System.Windows.Forms.TreeView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addEnumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.valueEnumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flagsEnumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addParserFromSequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.renameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateParserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createScriptDocumentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serializersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.scriptParserTypeEditorControl = new CANAPE.Controls.DocumentEditors.ParserEditors.ScriptParserTypeEditorControl();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.enumParserTypeEditorControl = new CANAPE.Controls.DocumentEditors.ParserEditors.EnumParserTypeEditorControl();
            this.sequenceEditorControl = new CANAPE.Controls.DocumentEditors.ParserEditors.SequenceParserTypeEditorControl();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonParse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOpenTest = new System.Windows.Forms.ToolStripButton();
            this.duplicateTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.toolStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeViewTypes);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.scriptParserTypeEditorControl);
            this.splitContainer.Panel2.Controls.Add(this.propertyGrid);
            this.splitContainer.Panel2.Controls.Add(this.enumParserTypeEditorControl);
            this.splitContainer.Panel2.Controls.Add(this.sequenceEditorControl);
            // 
            // treeViewTypes
            // 
            this.treeViewTypes.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.treeViewTypes, "treeViewTypes");
            this.treeViewTypes.HideSelection = false;
            this.treeViewTypes.ImageList = this.imageList;
            this.treeViewTypes.LabelEdit = true;
            this.treeViewTypes.Name = "treeViewTypes";
            this.treeViewTypes.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewTypes_BeforeLabelEdit);
            this.treeViewTypes.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.treeViewTypes_AfterLabelEdit);
            this.treeViewTypes.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewTypes_BeforeCollapse);
            this.treeViewTypes.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewTypes_BeforeExpand);
            this.treeViewTypes.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewTypes_AfterSelect);
            this.treeViewTypes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewTypes_MouseDown);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addEnumToolStripMenuItem,
            this.addSequenceToolStripMenuItem,
            this.addParserFromSequenceToolStripMenuItem,
            this.addScriptToolStripMenuItem,
            this.duplicateTypeToolStripMenuItem,
            this.toolStripSeparator1,
            this.renameToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportScriptToolStripMenuItem,
            this.copyScriptToolStripMenuItem,
            this.validateParserToolStripMenuItem,
            this.createScriptDocumentToolStripMenuItem,
            this.serializersToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // addEnumToolStripMenuItem
            // 
            this.addEnumToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.valueEnumToolStripMenuItem,
            this.flagsEnumToolStripMenuItem});
            this.addEnumToolStripMenuItem.Name = "addEnumToolStripMenuItem";
            resources.ApplyResources(this.addEnumToolStripMenuItem, "addEnumToolStripMenuItem");
            // 
            // valueEnumToolStripMenuItem
            // 
            this.valueEnumToolStripMenuItem.Name = "valueEnumToolStripMenuItem";
            resources.ApplyResources(this.valueEnumToolStripMenuItem, "valueEnumToolStripMenuItem");
            this.valueEnumToolStripMenuItem.Click += new System.EventHandler(this.valueEnumToolStripMenuItem_Click);
            // 
            // flagsEnumToolStripMenuItem
            // 
            this.flagsEnumToolStripMenuItem.Name = "flagsEnumToolStripMenuItem";
            resources.ApplyResources(this.flagsEnumToolStripMenuItem, "flagsEnumToolStripMenuItem");
            this.flagsEnumToolStripMenuItem.Click += new System.EventHandler(this.flagsEnumToolStripMenuItem_Click);
            // 
            // addSequenceToolStripMenuItem
            // 
            this.addSequenceToolStripMenuItem.Name = "addSequenceToolStripMenuItem";
            resources.ApplyResources(this.addSequenceToolStripMenuItem, "addSequenceToolStripMenuItem");
            this.addSequenceToolStripMenuItem.Click += new System.EventHandler(this.addSequenceToolStripMenuItem_Click);
            // 
            // addParserFromSequenceToolStripMenuItem
            // 
            this.addParserFromSequenceToolStripMenuItem.Name = "addParserFromSequenceToolStripMenuItem";
            resources.ApplyResources(this.addParserFromSequenceToolStripMenuItem, "addParserFromSequenceToolStripMenuItem");
            // 
            // addScriptToolStripMenuItem
            // 
            this.addScriptToolStripMenuItem.Name = "addScriptToolStripMenuItem";
            resources.ApplyResources(this.addScriptToolStripMenuItem, "addScriptToolStripMenuItem");
            this.addScriptToolStripMenuItem.Click += new System.EventHandler(this.addScriptToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // renameToolStripMenuItem
            // 
            this.renameToolStripMenuItem.Name = "renameToolStripMenuItem";
            resources.ApplyResources(this.renameToolStripMenuItem, "renameToolStripMenuItem");
            this.renameToolStripMenuItem.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            resources.ApplyResources(this.removeToolStripMenuItem, "removeToolStripMenuItem");
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // exportScriptToolStripMenuItem
            // 
            this.exportScriptToolStripMenuItem.Name = "exportScriptToolStripMenuItem";
            resources.ApplyResources(this.exportScriptToolStripMenuItem, "exportScriptToolStripMenuItem");
            this.exportScriptToolStripMenuItem.Click += new System.EventHandler(this.exportScriptToolStripMenuItem_Click);
            // 
            // copyScriptToolStripMenuItem
            // 
            this.copyScriptToolStripMenuItem.Name = "copyScriptToolStripMenuItem";
            resources.ApplyResources(this.copyScriptToolStripMenuItem, "copyScriptToolStripMenuItem");
            this.copyScriptToolStripMenuItem.Click += new System.EventHandler(this.copyScriptToolStripMenuItem_Click);
            // 
            // validateParserToolStripMenuItem
            // 
            this.validateParserToolStripMenuItem.Name = "validateParserToolStripMenuItem";
            resources.ApplyResources(this.validateParserToolStripMenuItem, "validateParserToolStripMenuItem");
            this.validateParserToolStripMenuItem.Click += new System.EventHandler(this.validateParserToolStripMenuItem_Click);
            // 
            // createScriptDocumentToolStripMenuItem
            // 
            this.createScriptDocumentToolStripMenuItem.Name = "createScriptDocumentToolStripMenuItem";
            resources.ApplyResources(this.createScriptDocumentToolStripMenuItem, "createScriptDocumentToolStripMenuItem");
            this.createScriptDocumentToolStripMenuItem.Click += new System.EventHandler(this.createScriptDocumentToolStripMenuItem_Click);
            // 
            // serializersToolStripMenuItem
            // 
            this.serializersToolStripMenuItem.Name = "serializersToolStripMenuItem";
            resources.ApplyResources(this.serializersToolStripMenuItem, "serializersToolStripMenuItem");
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList.Images.SetKeyName(0, "VSObject_Class.bmp");
            this.imageList.Images.SetKeyName(1, "VSObject_Class_Private.bmp");
            this.imageList.Images.SetKeyName(2, "VSObject_Field.bmp");
            this.imageList.Images.SetKeyName(3, "VSObject_Field_Private.bmp");
            this.imageList.Images.SetKeyName(4, "VSFolder_closed.bmp");
            this.imageList.Images.SetKeyName(5, "VSFolder_open.bmp");
            this.imageList.Images.SetKeyName(6, "VSProject_genericproject.ico");
            // 
            // scriptParserTypeEditorControl
            // 
            resources.ApplyResources(this.scriptParserTypeEditorControl, "scriptParserTypeEditorControl");
            this.scriptParserTypeEditorControl.Name = "scriptParserTypeEditorControl";
            // 
            // propertyGrid
            // 
            resources.ApplyResources(this.propertyGrid, "propertyGrid");
            this.propertyGrid.Name = "propertyGrid";
            // 
            // enumParserTypeEditorControl
            // 
            resources.ApplyResources(this.enumParserTypeEditorControl, "enumParserTypeEditorControl");
            this.enumParserTypeEditorControl.Name = "enumParserTypeEditorControl";
            // 
            // sequenceEditorControl
            // 
            resources.ApplyResources(this.sequenceEditorControl, "sequenceEditorControl");
            this.sequenceEditorControl.Name = "sequenceEditorControl";
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonParse,
            this.toolStripSeparator3,
            this.toolStripButtonOpenTest});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            // 
            // toolStripButtonParse
            // 
            this.toolStripButtonParse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonParse, "toolStripButtonParse");
            this.toolStripButtonParse.Name = "toolStripButtonParse";
            this.toolStripButtonParse.Click += new System.EventHandler(this.validateParserToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripButtonOpenTest
            // 
            this.toolStripButtonOpenTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonOpenTest, "toolStripButtonOpenTest");
            this.toolStripButtonOpenTest.Name = "toolStripButtonOpenTest";
            this.toolStripButtonOpenTest.Click += new System.EventHandler(this.toolStripButtonOpenTest_Click);
            // 
            // duplicateTypeToolStripMenuItem
            // 
            this.duplicateTypeToolStripMenuItem.Name = "duplicateTypeToolStripMenuItem";
            resources.ApplyResources(this.duplicateTypeToolStripMenuItem, "duplicateTypeToolStripMenuItem");
            this.duplicateTypeToolStripMenuItem.Click += new System.EventHandler(this.duplicateTypeToolStripMenuItem_Click);
            // 
            // ParserDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.splitContainer);
            this.Name = "ParserDocumentControl";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addEnumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addSequenceToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.TreeView treeViewTypes;
        private ParserEditors.EnumParserTypeEditorControl enumParserTypeEditorControl;
        private ParserEditors.SequenceParserTypeEditorControl sequenceEditorControl;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripMenuItem exportScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addParserFromSequenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStripMenuItem valueEnumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flagsEnumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateParserToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyScriptToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createScriptDocumentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serializersToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonParse;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenTest;
        private System.Windows.Forms.ToolStripMenuItem addScriptToolStripMenuItem;
        private ParserEditors.ScriptParserTypeEditorControl scriptParserTypeEditorControl;
        private System.Windows.Forms.ToolStripMenuItem duplicateTypeToolStripMenuItem;        
    }
}
