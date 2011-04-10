namespace Seobiseu {
    partial class MainForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lsvServices = new System.Windows.Forms.ListView();
            this.lsvAllServices_colDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnxServices = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnxServicesStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxServicesStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxServicesAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxServicesRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.imlServiceStatus = new System.Windows.Forms.ImageList(this.components);
            this.mnu = new System.Windows.Forms.ToolStrip();
            this.mnuStart = new System.Windows.Forms.ToolStripButton();
            this.mnuAbout = new System.Windows.Forms.ToolStripButton();
            this.mnuReportABug = new System.Windows.Forms.ToolStripButton();
            this.mnuOptions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAdd = new System.Windows.Forms.ToolStripButton();
            this.mnuRemove = new System.Windows.Forms.ToolStripButton();
            this.bwServicesUpdate = new System.ComponentModel.BackgroundWorker();
            this.mnxServices.SuspendLayout();
            this.mnu.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvServices
            // 
            this.lsvServices.AllowDrop = true;
            this.lsvServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsvServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvAllServices_colDisplayName});
            this.lsvServices.ContextMenuStrip = this.mnxServices;
            this.lsvServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvServices.FullRowSelect = true;
            this.lsvServices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvServices.LabelWrap = false;
            this.lsvServices.Location = new System.Drawing.Point(0, 27);
            this.lsvServices.MultiSelect = false;
            this.lsvServices.Name = "lsvServices";
            this.lsvServices.Size = new System.Drawing.Size(382, 228);
            this.lsvServices.SmallImageList = this.imlServiceStatus;
            this.lsvServices.TabIndex = 0;
            this.lsvServices.UseCompatibleStateImageBehavior = false;
            this.lsvServices.View = System.Windows.Forms.View.Details;
            this.lsvServices.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.lsvServices_ItemDrag);
            this.lsvServices.SelectedIndexChanged += new System.EventHandler(this.lsvServices_SelectedIndexChanged);
            this.lsvServices.DragDrop += new System.Windows.Forms.DragEventHandler(this.lsvServices_DragDrop);
            this.lsvServices.DragEnter += new System.Windows.Forms.DragEventHandler(this.lsvServices_DragEnter);
            this.lsvServices.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lsvServices_MouseUp);
            // 
            // lsvAllServices_colDisplayName
            // 
            this.lsvAllServices_colDisplayName.Text = "Name";
            this.lsvAllServices_colDisplayName.Width = 180;
            // 
            // mnxServices
            // 
            this.mnxServices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnxServicesStart,
            this.mnxServicesStop,
            this.toolStripMenuItem2,
            this.mnxServicesAdd,
            this.mnxServicesRemove});
            this.mnxServices.Name = "mnxServices";
            this.mnxServices.Size = new System.Drawing.Size(174, 128);
            this.mnxServices.Opening += new System.ComponentModel.CancelEventHandler(this.mnxServices_Opening);
            // 
            // mnxServicesStart
            // 
            this.mnxServicesStart.Image = ((System.Drawing.Image)(resources.GetObject("mnxServicesStart.Image")));
            this.mnxServicesStart.Name = "mnxServicesStart";
            this.mnxServicesStart.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnxServicesStart.Size = new System.Drawing.Size(173, 24);
            this.mnxServicesStart.Text = "Start";
            this.mnxServicesStart.Click += new System.EventHandler(this.mnxServicesStart_Click);
            // 
            // mnxServicesStop
            // 
            this.mnxServicesStop.Image = ((System.Drawing.Image)(resources.GetObject("mnxServicesStop.Image")));
            this.mnxServicesStop.Name = "mnxServicesStop";
            this.mnxServicesStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.mnxServicesStop.Size = new System.Drawing.Size(173, 24);
            this.mnxServicesStop.Text = "Stop";
            this.mnxServicesStop.Click += new System.EventHandler(this.mnxServicesStop_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(170, 6);
            // 
            // mnxServicesAdd
            // 
            this.mnxServicesAdd.Image = ((System.Drawing.Image)(resources.GetObject("mnxServicesAdd.Image")));
            this.mnxServicesAdd.Name = "mnxServicesAdd";
            this.mnxServicesAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.mnxServicesAdd.Size = new System.Drawing.Size(173, 24);
            this.mnxServicesAdd.Text = "Add";
            this.mnxServicesAdd.Click += new System.EventHandler(this.mnxServicesAdd_Click);
            // 
            // mnxServicesRemove
            // 
            this.mnxServicesRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnxServicesRemove.Image")));
            this.mnxServicesRemove.Name = "mnxServicesRemove";
            this.mnxServicesRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnxServicesRemove.Size = new System.Drawing.Size(173, 24);
            this.mnxServicesRemove.Text = "Remove";
            this.mnxServicesRemove.Click += new System.EventHandler(this.mnxServicesRemove_Click);
            // 
            // imlServiceStatus
            // 
            this.imlServiceStatus.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlServiceStatus.ImageStream")));
            this.imlServiceStatus.TransparentColor = System.Drawing.Color.Transparent;
            this.imlServiceStatus.Images.SetKeyName(0, "Service 0x00 Unknown (16x16).png");
            this.imlServiceStatus.Images.SetKeyName(1, "Service 0x01 Stopped (16x16).png");
            this.imlServiceStatus.Images.SetKeyName(2, "Service 0x02 StartPending (16x16).png");
            this.imlServiceStatus.Images.SetKeyName(3, "Service 0x03 StopPending (16x16).png");
            this.imlServiceStatus.Images.SetKeyName(4, "Service 0x04 Running (16x16).png");
            this.imlServiceStatus.Images.SetKeyName(5, "Service 0x05 ContinuePending (16x16).png");
            this.imlServiceStatus.Images.SetKeyName(6, "Service 0x06 PausePending (16x16).png");
            this.imlServiceStatus.Images.SetKeyName(7, "Service 0x07 Paused (16x16).png");
            // 
            // mnu
            // 
            this.mnu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStart,
            this.mnuAbout,
            this.mnuReportABug,
            this.mnuOptions,
            this.toolStripSeparator1,
            this.mnuStop,
            this.toolStripSeparator2,
            this.mnuAdd,
            this.mnuRemove});
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.mnu.Size = new System.Drawing.Size(382, 27);
            this.mnu.TabIndex = 1;
            // 
            // mnuStart
            // 
            this.mnuStart.Image = ((System.Drawing.Image)(resources.GetObject("mnuStart.Image")));
            this.mnuStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuStart.Name = "mnuStart";
            this.mnuStart.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mnuStart.Size = new System.Drawing.Size(60, 24);
            this.mnuStart.Text = "Start";
            this.mnuStart.ToolTipText = "Start <F5>";
            this.mnuStart.Click += new System.EventHandler(this.mnuStart_Click);
            // 
            // mnuAbout
            // 
            this.mnuAbout.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuAbout.Image = ((System.Drawing.Image)(resources.GetObject("mnuAbout.Image")));
            this.mnuAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(23, 24);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuReportABug
            // 
            this.mnuReportABug.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuReportABug.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuReportABug.Image = ((System.Drawing.Image)(resources.GetObject("mnuReportABug.Image")));
            this.mnuReportABug.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuReportABug.Name = "mnuReportABug";
            this.mnuReportABug.Size = new System.Drawing.Size(23, 24);
            this.mnuReportABug.Text = "Report a bug";
            this.mnuReportABug.Click += new System.EventHandler(this.mnuReportABug_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuOptions.Image = ((System.Drawing.Image)(resources.GetObject("mnuOptions.Image")));
            this.mnuOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(23, 24);
            this.mnuOptions.Text = "Options";
            this.mnuOptions.Click += new System.EventHandler(this.mnuOptions_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // mnuStop
            // 
            this.mnuStop.Image = ((System.Drawing.Image)(resources.GetObject("mnuStop.Image")));
            this.mnuStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mnuStop.Size = new System.Drawing.Size(60, 24);
            this.mnuStop.Text = "Stop";
            this.mnuStop.ToolTipText = "Stop <Shift+F5>";
            this.mnuStop.Click += new System.EventHandler(this.mnuStop_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // mnuAdd
            // 
            this.mnuAdd.Image = ((System.Drawing.Image)(resources.GetObject("mnuAdd.Image")));
            this.mnuAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAdd.Name = "mnuAdd";
            this.mnuAdd.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mnuAdd.Size = new System.Drawing.Size(57, 24);
            this.mnuAdd.Text = "Add";
            this.mnuAdd.ToolTipText = "Add <Ins>";
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // mnuRemove
            // 
            this.mnuRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnuRemove.Image")));
            this.mnuRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuRemove.Name = "mnuRemove";
            this.mnuRemove.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mnuRemove.Size = new System.Drawing.Size(83, 24);
            this.mnuRemove.Text = "Remove";
            this.mnuRemove.ToolTipText = "Remove <Del>";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // bwServicesUpdate
            // 
            this.bwServicesUpdate.WorkerReportsProgress = true;
            this.bwServicesUpdate.WorkerSupportsCancellation = true;
            this.bwServicesUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwServicesUpdate_DoWork);
            this.bwServicesUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwServicesUpdate_ProgressChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 255);
            this.Controls.Add(this.lsvServices);
            this.Controls.Add(this.mnu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(320, 200);
            this.Name = "MainForm";
            this.Text = "Seobiseu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.Form_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.mnxServices.ResumeLayout(false);
            this.mnu.ResumeLayout(false);
            this.mnu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lsvServices;
        private System.Windows.Forms.ColumnHeader lsvAllServices_colDisplayName;
        private System.Windows.Forms.ToolStrip mnu;
        private System.Windows.Forms.ToolStripButton mnuStart;
        private System.Windows.Forms.ToolStripButton mnuAbout;
        private System.Windows.Forms.ToolStripButton mnuReportABug;
        private System.Windows.Forms.ToolStripButton mnuOptions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton mnuStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mnuAdd;
        private System.Windows.Forms.ToolStripButton mnuRemove;
        private System.Windows.Forms.ImageList imlServiceStatus;
        private System.Windows.Forms.ContextMenuStrip mnxServices;
        private System.Windows.Forms.ToolStripMenuItem mnxServicesStart;
        private System.Windows.Forms.ToolStripMenuItem mnxServicesStop;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mnxServicesAdd;
        private System.Windows.Forms.ToolStripMenuItem mnxServicesRemove;
        private System.ComponentModel.BackgroundWorker bwServicesUpdate;

    }
}

