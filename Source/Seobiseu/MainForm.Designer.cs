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
            this.lsvServices_colDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mnxServices = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnxServicesStart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxServicesRestart = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxServicesStop = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnxServicesAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnxServicesRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.imlServiceStatus = new System.Windows.Forms.ImageList(this.components);
            this.mnu = new System.Windows.Forms.ToolStrip();
            this.mnuStart = new System.Windows.Forms.ToolStripButton();
            this.mnuStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAdd = new System.Windows.Forms.ToolStripButton();
            this.mnuRemove = new System.Windows.Forms.ToolStripButton();
            this.mnuApp = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuAppOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuApp0 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuAppFeedback = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAppAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.bwServicesUpdate = new System.ComponentModel.BackgroundWorker();
            this.sta = new System.Windows.Forms.StatusStrip();
            this.staStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.staServiceName = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnxServices.SuspendLayout();
            this.mnu.SuspendLayout();
            this.sta.SuspendLayout();
            this.SuspendLayout();
            // 
            // lsvServices
            // 
            this.lsvServices.AllowDrop = true;
            this.lsvServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsvServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvServices_colDisplayName});
            this.lsvServices.ContextMenuStrip = this.mnxServices;
            this.lsvServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvServices.FullRowSelect = true;
            this.lsvServices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvServices.LabelWrap = false;
            this.lsvServices.Location = new System.Drawing.Point(0, 27);
            this.lsvServices.MultiSelect = false;
            this.lsvServices.Name = "lsvServices";
            this.lsvServices.Size = new System.Drawing.Size(482, 203);
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
            // lsvServices_colDisplayName
            // 
            this.lsvServices_colDisplayName.Text = "Name";
            this.lsvServices_colDisplayName.Width = 180;
            // 
            // mnxServices
            // 
            this.mnxServices.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnxServices.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnxServicesStart,
            this.mnxServicesRestart,
            this.mnxServicesStop,
            this.toolStripMenuItem2,
            this.mnxServicesAdd,
            this.mnxServicesRemove});
            this.mnxServices.Name = "mnxServices";
            this.mnxServices.Size = new System.Drawing.Size(180, 140);
            this.mnxServices.Opening += new System.ComponentModel.CancelEventHandler(this.mnxServices_Opening);
            // 
            // mnxServicesStart
            // 
            this.mnxServicesStart.Image = ((System.Drawing.Image)(resources.GetObject("mnxServicesStart.Image")));
            this.mnxServicesStart.Name = "mnxServicesStart";
            this.mnxServicesStart.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnxServicesStart.Size = new System.Drawing.Size(179, 26);
            this.mnxServicesStart.Tag = "mnuStart";
            this.mnxServicesStart.Text = "Start";
            this.mnxServicesStart.Click += new System.EventHandler(this.mnxServicesStart_Click);
            // 
            // mnxServicesRestart
            // 
            this.mnxServicesRestart.Image = ((System.Drawing.Image)(resources.GetObject("mnxServicesRestart.Image")));
            this.mnxServicesRestart.Name = "mnxServicesRestart";
            this.mnxServicesRestart.Size = new System.Drawing.Size(179, 26);
            this.mnxServicesRestart.Tag = "mnuRestart";
            this.mnxServicesRestart.Text = "Restart";
            this.mnxServicesRestart.Click += new System.EventHandler(this.mnxServicesRestart_Click);
            // 
            // mnxServicesStop
            // 
            this.mnxServicesStop.Image = ((System.Drawing.Image)(resources.GetObject("mnxServicesStop.Image")));
            this.mnxServicesStop.Name = "mnxServicesStop";
            this.mnxServicesStop.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F5)));
            this.mnxServicesStop.Size = new System.Drawing.Size(179, 26);
            this.mnxServicesStop.Tag = "mnuStop";
            this.mnxServicesStop.Text = "Stop";
            this.mnxServicesStop.Click += new System.EventHandler(this.mnxServicesStop_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(176, 6);
            // 
            // mnxServicesAdd
            // 
            this.mnxServicesAdd.Image = ((System.Drawing.Image)(resources.GetObject("mnxServicesAdd.Image")));
            this.mnxServicesAdd.Name = "mnxServicesAdd";
            this.mnxServicesAdd.ShortcutKeys = System.Windows.Forms.Keys.Insert;
            this.mnxServicesAdd.Size = new System.Drawing.Size(179, 26);
            this.mnxServicesAdd.Tag = "mnuAdd";
            this.mnxServicesAdd.Text = "Add";
            this.mnxServicesAdd.Click += new System.EventHandler(this.mnxServicesAdd_Click);
            // 
            // mnxServicesRemove
            // 
            this.mnxServicesRemove.Image = ((System.Drawing.Image)(resources.GetObject("mnxServicesRemove.Image")));
            this.mnxServicesRemove.Name = "mnxServicesRemove";
            this.mnxServicesRemove.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.mnxServicesRemove.Size = new System.Drawing.Size(179, 26);
            this.mnxServicesRemove.Tag = "mnuRemove";
            this.mnxServicesRemove.Text = "Remove";
            this.mnxServicesRemove.Click += new System.EventHandler(this.mnxServicesRemove_Click);
            // 
            // imlServiceStatus
            // 
            this.imlServiceStatus.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imlServiceStatus.ImageSize = new System.Drawing.Size(16, 16);
            this.imlServiceStatus.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // mnu
            // 
            this.mnu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.mnu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStart,
            this.mnuStop,
            this.toolStripSeparator2,
            this.mnuAdd,
            this.mnuRemove,
            this.mnuApp});
            this.mnu.Location = new System.Drawing.Point(0, 0);
            this.mnu.Name = "mnu";
            this.mnu.Padding = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.mnu.Size = new System.Drawing.Size(482, 27);
            this.mnu.TabIndex = 1;
            // 
            // mnuStart
            // 
            this.mnuStart.Image = global::Seobiseu.Properties.Resources.mnuStart_16;
            this.mnuStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuStart.Name = "mnuStart";
            this.mnuStart.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mnuStart.Size = new System.Drawing.Size(64, 24);
            this.mnuStart.Text = "Start";
            this.mnuStart.ToolTipText = "Start service (F5)";
            this.mnuStart.Click += new System.EventHandler(this.mnuStart_Click);
            // 
            // mnuStop
            // 
            this.mnuStop.Image = global::Seobiseu.Properties.Resources.mnuStop_16;
            this.mnuStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuStop.Name = "mnuStop";
            this.mnuStop.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mnuStop.Size = new System.Drawing.Size(64, 24);
            this.mnuStop.Text = "Stop";
            this.mnuStop.ToolTipText = "Stop service (Shift+F5)";
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
            this.mnuAdd.Image = global::Seobiseu.Properties.Resources.mnuAdd_16;
            this.mnuAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuAdd.Name = "mnuAdd";
            this.mnuAdd.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mnuAdd.Size = new System.Drawing.Size(61, 24);
            this.mnuAdd.Text = "Add";
            this.mnuAdd.ToolTipText = "Add service (Insert)";
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // mnuRemove
            // 
            this.mnuRemove.Image = global::Seobiseu.Properties.Resources.mnuRemove_16;
            this.mnuRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuRemove.Name = "mnuRemove";
            this.mnuRemove.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.mnuRemove.Size = new System.Drawing.Size(87, 24);
            this.mnuRemove.Text = "Remove";
            this.mnuRemove.ToolTipText = "Remove service (Delete)";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // mnuApp
            // 
            this.mnuApp.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mnuApp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuApp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAppOptions,
            this.mnuApp0,
            this.mnuAppFeedback,
            this.mnuAppAbout});
            this.mnuApp.Image = global::Seobiseu.Properties.Resources.mnuApp_16;
            this.mnuApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuApp.Name = "mnuApp";
            this.mnuApp.Size = new System.Drawing.Size(34, 24);
            this.mnuApp.Text = "Application";
            // 
            // mnuAppOptions
            // 
            this.mnuAppOptions.Name = "mnuAppOptions";
            this.mnuAppOptions.Size = new System.Drawing.Size(182, 26);
            this.mnuAppOptions.Text = "&Options";
            this.mnuAppOptions.Click += new System.EventHandler(this.mnuAppOptions_Click);
            // 
            // mnuApp0
            // 
            this.mnuApp0.Name = "mnuApp0";
            this.mnuApp0.Size = new System.Drawing.Size(179, 6);
            // 
            // mnuAppFeedback
            // 
            this.mnuAppFeedback.Name = "mnuAppFeedback";
            this.mnuAppFeedback.Size = new System.Drawing.Size(182, 26);
            this.mnuAppFeedback.Text = "Send &feedback";
            this.mnuAppFeedback.Click += new System.EventHandler(this.mnuAppFeedback_Click);
            // 
            // mnuAppAbout
            // 
            this.mnuAppAbout.Name = "mnuAppAbout";
            this.mnuAppAbout.Size = new System.Drawing.Size(182, 26);
            this.mnuAppAbout.Text = "&About";
            this.mnuAppAbout.Click += new System.EventHandler(this.mnuAppAbout_Click);
            // 
            // bwServicesUpdate
            // 
            this.bwServicesUpdate.WorkerReportsProgress = true;
            this.bwServicesUpdate.WorkerSupportsCancellation = true;
            this.bwServicesUpdate.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwServicesUpdate_DoWork);
            this.bwServicesUpdate.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwServicesUpdate_ProgressChanged);
            // 
            // sta
            // 
            this.sta.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.staStatus,
            this.staServiceName});
            this.sta.Location = new System.Drawing.Point(0, 230);
            this.sta.Name = "sta";
            this.sta.Size = new System.Drawing.Size(482, 25);
            this.sta.TabIndex = 2;
            this.sta.Text = "statusStrip1";
            // 
            // staStatus
            // 
            this.staStatus.Name = "staStatus";
            this.staStatus.Size = new System.Drawing.Size(454, 20);
            this.staStatus.Spring = true;
            this.staStatus.Text = " ";
            this.staStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // staServiceName
            // 
            this.staServiceName.Name = "staServiceName";
            this.staServiceName.Size = new System.Drawing.Size(13, 20);
            this.staServiceName.Text = " ";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(482, 255);
            this.Controls.Add(this.lsvServices);
            this.Controls.Add(this.sta);
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
            this.sta.ResumeLayout(false);
            this.sta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lsvServices;
        private System.Windows.Forms.ColumnHeader lsvServices_colDisplayName;
        private System.Windows.Forms.ToolStrip mnu;
        private System.Windows.Forms.ToolStripButton mnuStart;
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
        private System.Windows.Forms.StatusStrip sta;
        private System.Windows.Forms.ToolStripStatusLabel staStatus;
        private System.Windows.Forms.ToolStripStatusLabel staServiceName;
        private System.Windows.Forms.ToolStripMenuItem mnxServicesRestart;
        private System.Windows.Forms.ToolStripDropDownButton mnuApp;
        private System.Windows.Forms.ToolStripMenuItem mnuAppOptions;
        private System.Windows.Forms.ToolStripSeparator mnuApp0;
        private System.Windows.Forms.ToolStripMenuItem mnuAppFeedback;
        private System.Windows.Forms.ToolStripMenuItem mnuAppAbout;

    }
}

