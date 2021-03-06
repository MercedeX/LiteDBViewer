﻿namespace LiteDBViewer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.txt_query = new System.Windows.Forms.TextBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.FileInformation = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.CategoryView = new System.Windows.Forms.TreeView();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.lblRows = new System.Windows.Forms.Label();
            this.lvData = new System.Windows.Forms.ListView();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.messageBar = new System.Windows.Forms.ToolStripStatusLabel();
            this.databaseCMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CloseDatabaseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.statusBar.SuspendLayout();
            this.databaseCMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // txt_query
            // 
            this.txt_query.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_query.Font = new System.Drawing.Font("Courier New", 9F);
            this.txt_query.Location = new System.Drawing.Point(3, 3);
            this.txt_query.Name = "txt_query";
            this.txt_query.Size = new System.Drawing.Size(914, 21);
            this.txt_query.TabIndex = 1;
            this.txt_query.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_KeyDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "database-5-16.png");
            this.imageList1.Images.SetKeyName(1, "tables.png");
            this.imageList1.Images.SetKeyName(2, "table.png");
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.menuStrip1.Size = new System.Drawing.Size(920, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileOpen,
            this.FileInformation,
            this.FileExport,
            this.toolStripMenuItem1,
            this.FileExit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // FileOpen
            // 
            this.FileOpen.Name = "FileOpen";
            this.FileOpen.Size = new System.Drawing.Size(128, 22);
            this.FileOpen.Text = "&Open ...";
            this.FileOpen.Click += new System.EventHandler(this.Open_Click);
            // 
            // FileInformation
            // 
            this.FileInformation.Name = "FileInformation";
            this.FileInformation.Size = new System.Drawing.Size(128, 22);
            this.FileInformation.Text = "&Informaton";
            this.FileInformation.Click += new System.EventHandler(this.Info_Click);
            // 
            // FileExport
            // 
            this.FileExport.Name = "FileExport";
            this.FileExport.Size = new System.Drawing.Size(128, 22);
            this.FileExport.Text = "&Export";
            this.FileExport.Click += new System.EventHandler(this.Export_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(125, 6);
            // 
            // FileExit
            // 
            this.FileExit.Name = "FileExit";
            this.FileExit.Size = new System.Drawing.Size(128, 22);
            this.FileExit.Text = "E&xit";
            this.FileExit.Click += new System.EventHandler(this.FileExit_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.splitContainer1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.txt_query, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.statusBar, 0, 2);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 5.5666F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 94.4334F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(920, 503);
            this.tableLayoutPanel2.TabIndex = 12;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 29);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.CategoryView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel3);
            this.splitContainer1.Size = new System.Drawing.Size(914, 450);
            this.splitContainer1.SplitterDistance = 190;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 13;
            // 
            // CategoryView
            // 
            this.CategoryView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CategoryView.ImageIndex = 0;
            this.CategoryView.ImageList = this.imageList1;
            this.CategoryView.Location = new System.Drawing.Point(0, 0);
            this.CategoryView.Name = "CategoryView";
            this.CategoryView.SelectedImageIndex = 0;
            this.CategoryView.ShowRootLines = false;
            this.CategoryView.Size = new System.Drawing.Size(190, 450);
            this.CategoryView.TabIndex = 12;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.lblRows, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.lvData, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 3.982301F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 96.0177F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(718, 450);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Location = new System.Drawing.Point(3, 0);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(40, 13);
            this.lblRows.TabIndex = 8;
            this.lblRows.Text = "Result:";
            // 
            // lvData
            // 
            this.lvData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvData.FullRowSelect = true;
            this.lvData.Location = new System.Drawing.Point(3, 20);
            this.lvData.Name = "lvData";
            this.lvData.Size = new System.Drawing.Size(712, 427);
            this.lvData.TabIndex = 9;
            this.lvData.UseCompatibleStateImageBehavior = false;
            this.lvData.View = System.Windows.Forms.View.Details;
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messageBar});
            this.statusBar.Location = new System.Drawing.Point(0, 482);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(920, 21);
            this.statusBar.TabIndex = 11;
            this.statusBar.Text = "Waiting for user input ...";
            // 
            // messageBar
            // 
            this.messageBar.Name = "messageBar";
            this.messageBar.Size = new System.Drawing.Size(905, 16);
            this.messageBar.Spring = true;
            this.messageBar.Text = "No Text";
            this.messageBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // databaseCMenu
            // 
            this.databaseCMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CloseDatabaseMenu});
            this.databaseCMenu.Name = "databaseCMenu";
            this.databaseCMenu.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.databaseCMenu.Size = new System.Drawing.Size(101, 26);
            // 
            // CloseDatabaseMenu
            // 
            this.CloseDatabaseMenu.Name = "CloseDatabaseMenu";
            this.CloseDatabaseMenu.Size = new System.Drawing.Size(100, 22);
            this.CloseDatabaseMenu.Text = "Close";
            this.CloseDatabaseMenu.Click += new System.EventHandler(this.CloseDatabaseMenu_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 527);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(700, 375);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LiteDB Viewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.databaseCMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txt_query;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem FileExport;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem FileExit;
        private System.Windows.Forms.ToolStripMenuItem FileInformation;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripMenuItem FileOpen;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView CategoryView;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label lblRows;
        internal System.Windows.Forms.ToolStripStatusLabel messageBar;
        private System.Windows.Forms.ContextMenuStrip databaseCMenu;
        private System.Windows.Forms.ToolStripMenuItem CloseDatabaseMenu;
        private System.Windows.Forms.ListView lvData;
    }
}

