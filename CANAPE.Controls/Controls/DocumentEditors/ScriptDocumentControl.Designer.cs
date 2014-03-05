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
    partial class ScriptDocumentControl
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScriptDocumentControl));
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSaveToFile = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonParse = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.copyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.pasteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonUndo = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonRedo = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripTextBoxSearch = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripButtonNextSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonPreviousSearch = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonOpenTest = new System.Windows.Forms.ToolStripButton();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.textEditorControl = new ICSharpCode.TextEditor.TextEditorControl();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.selectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gotoLineToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editorConfigurationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listViewErrors = new System.Windows.Forms.ListView();
            this.columnSeverity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnLine = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip
            // 
            this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonOpen,
            this.toolStripButtonSaveToFile,
            this.toolStripButtonSave,
            this.toolStripSeparator,
            this.toolStripButtonParse,
            this.toolStripSeparator1,
            this.cutToolStripButton,
            this.copyToolStripButton,
            this.pasteToolStripButton,
            this.toolStripButtonUndo,
            this.toolStripButtonRedo,
            this.toolStripSeparator3,
            this.toolStripTextBoxSearch,
            this.toolStripButtonNextSearch,
            this.toolStripButtonPreviousSearch,
            this.toolStripSeparator4,
            this.toolStripButtonOptions,
            this.toolStripButtonOpenTest});
            resources.ApplyResources(this.toolStrip, "toolStrip");
            this.toolStrip.Name = "toolStrip";
            // 
            // toolStripButtonOpen
            // 
            this.toolStripButtonOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonOpen, "toolStripButtonOpen");
            this.toolStripButtonOpen.Name = "toolStripButtonOpen";
            this.toolStripButtonOpen.Click += new System.EventHandler(this.toolStripButtonOpen_Click);
            // 
            // toolStripButtonSaveToFile
            // 
            this.toolStripButtonSaveToFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonSaveToFile, "toolStripButtonSaveToFile");
            this.toolStripButtonSaveToFile.Name = "toolStripButtonSaveToFile";
            this.toolStripButtonSaveToFile.Click += new System.EventHandler(this.toolStripButtonSaveToFile_Click);
            // 
            // toolStripButtonSave
            // 
            this.toolStripButtonSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonSave, "toolStripButtonSave");
            this.toolStripButtonSave.Name = "toolStripButtonSave";
            this.toolStripButtonSave.Click += new System.EventHandler(this.toolStripButtonSave_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            resources.ApplyResources(this.toolStripSeparator, "toolStripSeparator");
            // 
            // toolStripButtonParse
            // 
            this.toolStripButtonParse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonParse, "toolStripButtonParse");
            this.toolStripButtonParse.Name = "toolStripButtonParse";
            this.toolStripButtonParse.Click += new System.EventHandler(this.toolStripButtonParse_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.cutToolStripButton, "cutToolStripButton");
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Click += new System.EventHandler(this.cutToolStripButton_Click);
            // 
            // copyToolStripButton
            // 
            this.copyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.copyToolStripButton, "copyToolStripButton");
            this.copyToolStripButton.Name = "copyToolStripButton";
            this.copyToolStripButton.Click += new System.EventHandler(this.copyToolStripButton_Click);
            // 
            // pasteToolStripButton
            // 
            this.pasteToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.pasteToolStripButton, "pasteToolStripButton");
            this.pasteToolStripButton.Name = "pasteToolStripButton";
            this.pasteToolStripButton.Click += new System.EventHandler(this.pasteToolStripButton_Click);
            // 
            // toolStripButtonUndo
            // 
            this.toolStripButtonUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonUndo, "toolStripButtonUndo");
            this.toolStripButtonUndo.Name = "toolStripButtonUndo";
            this.toolStripButtonUndo.Click += new System.EventHandler(this.toolStripButtonUndo_Click);
            // 
            // toolStripButtonRedo
            // 
            this.toolStripButtonRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonRedo, "toolStripButtonRedo");
            this.toolStripButtonRedo.Name = "toolStripButtonRedo";
            this.toolStripButtonRedo.Click += new System.EventHandler(this.toolStripButtonRedo_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // toolStripTextBoxSearch
            // 
            this.toolStripTextBoxSearch.Name = "toolStripTextBoxSearch";
            resources.ApplyResources(this.toolStripTextBoxSearch, "toolStripTextBoxSearch");
            this.toolStripTextBoxSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBoxSearch_KeyPress);
            this.toolStripTextBoxSearch.TextChanged += new System.EventHandler(this.toolStripTextBoxSearch_TextChanged);
            // 
            // toolStripButtonNextSearch
            // 
            this.toolStripButtonNextSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonNextSearch, "toolStripButtonNextSearch");
            this.toolStripButtonNextSearch.Name = "toolStripButtonNextSearch";
            this.toolStripButtonNextSearch.Click += new System.EventHandler(this.toolStripButtonNextSearch_Click);
            // 
            // toolStripButtonPreviousSearch
            // 
            this.toolStripButtonPreviousSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonPreviousSearch, "toolStripButtonPreviousSearch");
            this.toolStripButtonPreviousSearch.Name = "toolStripButtonPreviousSearch";
            this.toolStripButtonPreviousSearch.Click += new System.EventHandler(this.toolStripButtonPreviousSearch_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // toolStripButtonOptions
            // 
            this.toolStripButtonOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonOptions, "toolStripButtonOptions");
            this.toolStripButtonOptions.Name = "toolStripButtonOptions";
            this.toolStripButtonOptions.Click += new System.EventHandler(this.toolStripButtonOptions_Click);
            // 
            // toolStripButtonOpenTest
            // 
            this.toolStripButtonOpenTest.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            resources.ApplyResources(this.toolStripButtonOpenTest, "toolStripButtonOpenTest");
            this.toolStripButtonOpenTest.Name = "toolStripButtonOpenTest";
            this.toolStripButtonOpenTest.Click += new System.EventHandler(this.toolStripButtonOpenTest_Click);
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.textEditorControl);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.listViewErrors);
            // 
            // textEditorControl
            // 
            this.textEditorControl.ContextMenuStrip = this.contextMenuStrip;
            resources.ApplyResources(this.textEditorControl, "textEditorControl");
            this.textEditorControl.IsReadOnly = false;
            this.textEditorControl.Name = "textEditorControl";
            this.textEditorControl.TextChanged += new System.EventHandler(this.textEditorControl_TextChanged);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.toolStripSeparator2,
            this.selectAllToolStripMenuItem,
            this.gotoLineToolStripMenuItem,
            this.toolStripSeparator5,
            this.editorConfigurationToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            resources.ApplyResources(this.contextMenuStrip, "contextMenuStrip");
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            resources.ApplyResources(this.deleteToolStripMenuItem, "deleteToolStripMenuItem");
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // selectAllToolStripMenuItem
            // 
            this.selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            resources.ApplyResources(this.selectAllToolStripMenuItem, "selectAllToolStripMenuItem");
            this.selectAllToolStripMenuItem.Click += new System.EventHandler(this.selectAllToolStripMenuItem_Click);
            // 
            // gotoLineToolStripMenuItem
            // 
            this.gotoLineToolStripMenuItem.Name = "gotoLineToolStripMenuItem";
            resources.ApplyResources(this.gotoLineToolStripMenuItem, "gotoLineToolStripMenuItem");
            this.gotoLineToolStripMenuItem.Click += new System.EventHandler(this.gotoLineToolStripMenuItem_Click);
            // 
            // editorConfigurationToolStripMenuItem
            // 
            this.editorConfigurationToolStripMenuItem.Name = "editorConfigurationToolStripMenuItem";
            resources.ApplyResources(this.editorConfigurationToolStripMenuItem, "editorConfigurationToolStripMenuItem");
            this.editorConfigurationToolStripMenuItem.Click += new System.EventHandler(this.editorConfigurationToolStripMenuItem_Click);
            // 
            // listViewErrors
            // 
            this.listViewErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnSeverity,
            this.columnMessage,
            this.columnLine,
            this.columnColumn});
            resources.ApplyResources(this.listViewErrors, "listViewErrors");
            this.listViewErrors.FullRowSelect = true;
            this.listViewErrors.GridLines = true;
            this.listViewErrors.Name = "listViewErrors";
            this.listViewErrors.UseCompatibleStateImageBehavior = false;
            this.listViewErrors.View = System.Windows.Forms.View.Details;
            this.listViewErrors.DoubleClick += new System.EventHandler(this.listViewErrors_DoubleClick);
            // 
            // columnSeverity
            // 
            resources.ApplyResources(this.columnSeverity, "columnSeverity");
            // 
            // columnMessage
            // 
            resources.ApplyResources(this.columnMessage, "columnMessage");
            // 
            // columnLine
            // 
            resources.ApplyResources(this.columnLine, "columnLine");
            // 
            // columnColumn
            // 
            resources.ApplyResources(this.columnColumn, "columnColumn");
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.imageList.Images.SetKeyName(0, "VSObject_Class.bmp");
            this.imageList.Images.SetKeyName(1, "VSObject_Class_Friend.bmp");
            this.imageList.Images.SetKeyName(2, "VSObject_Class_Private.bmp");
            this.imageList.Images.SetKeyName(3, "VSObject_Class_Protected.bmp");
            this.imageList.Images.SetKeyName(4, "VSObject_Event.bmp");
            this.imageList.Images.SetKeyName(5, "VSObject_Event_Friend.bmp");
            this.imageList.Images.SetKeyName(6, "VSObject_Event_Private.bmp");
            this.imageList.Images.SetKeyName(7, "VSObject_Event_Protected.bmp");
            this.imageList.Images.SetKeyName(8, "VSObject_Field.bmp");
            this.imageList.Images.SetKeyName(9, "VSObject_Field_Friend.bmp");
            this.imageList.Images.SetKeyName(10, "VSObject_Field_Private.bmp");
            this.imageList.Images.SetKeyName(11, "VSObject_Field_Protected.bmp");
            this.imageList.Images.SetKeyName(12, "VSObject_Interface.bmp");
            this.imageList.Images.SetKeyName(13, "VSObject_Interface_Friend.bmp");
            this.imageList.Images.SetKeyName(14, "VSObject_Interface_Private.bmp");
            this.imageList.Images.SetKeyName(15, "VSObject_Interface_Protected.bmp");
            this.imageList.Images.SetKeyName(16, "VSObject_Method.bmp");
            this.imageList.Images.SetKeyName(17, "VSObject_Method_Friend.bmp");
            this.imageList.Images.SetKeyName(18, "VSObject_Method_Private.bmp");
            this.imageList.Images.SetKeyName(19, "VSObject_Method_Protected.bmp");
            this.imageList.Images.SetKeyName(20, "VSObject_Properties.bmp");
            this.imageList.Images.SetKeyName(21, "VSObject_Properties_Friend.bmp");
            this.imageList.Images.SetKeyName(22, "VSObject_Properties_Private.bmp");
            this.imageList.Images.SetKeyName(23, "VSObject_Properties_Protected.bmp");
            this.imageList.Images.SetKeyName(24, "VSObject_Structure.bmp");
            this.imageList.Images.SetKeyName(25, "VSObject_Structure_Friend.bmp");
            this.imageList.Images.SetKeyName(26, "VSObject_Structure_Private.bmp");
            this.imageList.Images.SetKeyName(27, "VSObject_Structure_Protected.bmp");
            this.imageList.Images.SetKeyName(28, "VSObject_Namespace.bmp");
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            resources.ApplyResources(this.toolStripSeparator5, "toolStripSeparator5");
            // 
            // ScriptDocumentControl
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.splitContainer);
            this.Name = "ScriptDocumentControl";            
            this.toolStrip.ResumeLayout(false);
            this.toolStrip.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpen;
        private System.Windows.Forms.ToolStripButton toolStripButtonSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton toolStripButtonParse;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.ListView listViewErrors;
        private System.Windows.Forms.ColumnHeader columnSeverity;
        private System.Windows.Forms.ColumnHeader columnMessage;
        private System.Windows.Forms.ColumnHeader columnLine;
        private System.Windows.Forms.ColumnHeader columnColumn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripButton copyToolStripButton;
        private System.Windows.Forms.ToolStripButton pasteToolStripButton;
        private System.Windows.Forms.ToolStripButton toolStripButtonSaveToFile;
        private System.Windows.Forms.ToolStripButton toolStripButtonOptions;
        private ICSharpCode.TextEditor.TextEditorControl textEditorControl;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem selectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBoxSearch;
        private System.Windows.Forms.ToolStripButton toolStripButtonNextSearch;
        private System.Windows.Forms.ToolStripButton toolStripButtonPreviousSearch;
        private System.Windows.Forms.ToolStripMenuItem gotoLineToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButtonUndo;
        private System.Windows.Forms.ToolStripButton toolStripButtonRedo;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        internal System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripButton toolStripButtonOpenTest;
        private System.Windows.Forms.ToolStripMenuItem editorConfigurationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
    }
}