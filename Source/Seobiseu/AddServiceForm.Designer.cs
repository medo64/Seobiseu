namespace Seobiseu {
    partial class AddServiceForm {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddServiceForm));
            this.lsvServices = new System.Windows.Forms.ListView();
            this.lsvAllServices_colDisplayName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imlServiceStatus = new System.Windows.Forms.ImageList(this.components);
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lsvServices
            // 
            this.lsvServices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsvServices.CheckBoxes = true;
            this.lsvServices.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvAllServices_colDisplayName});
            this.lsvServices.FullRowSelect = true;
            this.lsvServices.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvServices.LabelWrap = false;
            this.lsvServices.Location = new System.Drawing.Point(0, 0);
            this.lsvServices.MultiSelect = false;
            this.lsvServices.Name = "lsvServices";
            this.lsvServices.Size = new System.Drawing.Size(494, 309);
            this.lsvServices.SmallImageList = this.imlServiceStatus;
            this.lsvServices.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lsvServices.TabIndex = 0;
            this.lsvServices.UseCompatibleStateImageBehavior = false;
            this.lsvServices.View = System.Windows.Forms.View.Details;
            this.lsvServices.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.lsvServices_ItemChecked);
            // 
            // lsvAllServices_colDisplayName
            // 
            this.lsvAllServices_colDisplayName.Text = "Name";
            this.lsvAllServices_colDisplayName.Width = 180;
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
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(296, 327);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 28);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(392, 327);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 15, 3, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 28);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // AddServiceForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(494, 367);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lsvServices);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddServiceForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add service";
            this.Load += new System.EventHandler(this.AddServiceForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_KeyDown);
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lsvServices;
        private System.Windows.Forms.ColumnHeader lsvAllServices_colDisplayName;
        private System.Windows.Forms.ImageList imlServiceStatus;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}