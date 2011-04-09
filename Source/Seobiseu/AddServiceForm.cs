using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.ServiceProcess;

namespace Seobiseu {
    public partial class AddServiceForm : Form {

        public AddServiceForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyData) {

                case Keys.Escape: {
                        this.Close();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    } break;

                case Keys.Insert: {
                        if (lsvServices.SelectedItems.Count > 0) {
                            lsvServices.SelectedItems[0].Checked = true;
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    } break;

                case Keys.Delete: {
                        if (lsvServices.SelectedItems.Count > 0) {
                            lsvServices.SelectedItems[0].Checked = false;
                        }
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    } break;

            }
        }

        private void AddServiceForm_Load(object sender, EventArgs e) {
            foreach (var service in ServiceController.GetServices()) {
                var item = new ServiceItem(service);
                var lvi = new ListViewItem() { Tag = item };
                lvi.Text = item.DisplayName;
                lvi.ImageIndex = (int)item.Status;
                lsvServices.Items.Add(lvi);
            }

        }

        private void Form_Resize(object sender, EventArgs e) {
            lsvServices.Columns[0].Width = lsvServices.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }


        private void lsvServices_ItemChecked(object sender, ItemCheckedEventArgs e) {
            btnOK.Enabled = (lsvServices.CheckedItems.Count > 0);
        }


        private void btnOK_Click(object sender, EventArgs e) {
            var serviceNames = new List<string>();
            foreach (var serviceName in Settings.ServiceNames) {
                serviceNames.Add(serviceName);
            }

            foreach (ListViewItem item in lsvServices.CheckedItems) {
                var service = (ServiceItem)item.Tag;
                if (serviceNames.Contains(service.ServiceName) == false) {
                    serviceNames.Add(service.ServiceName);
                }
            }

            Settings.ServiceNames = serviceNames.ToArray();
        }

    }
}
