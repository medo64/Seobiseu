using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace Seobiseu {
    internal partial class MainForm : Form {

        public MainForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            mnu.Renderer = Helpers.ToolStripBorderlessSystemRendererInstance;

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }


        private void Form_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyData) {
                case Keys.Alt | Keys.Menu: {
                        mnu.Select();
                        mnuStart.Select();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    } break;

                case Keys.F5: {
                        mnuStart_Click(null, null);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    } break;

                case Keys.Shift | Keys.F5: {
                        mnuStop_Click(null, null);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    } break;

                case Keys.Insert: {
                        mnuAdd_Click(null, null);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    } break;

                case Keys.Delete: {
                        mnuRemove_Click(null, null);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    } break;

            }
        }

        private void Form_Load(object sender, EventArgs e) {
            bwServicesUpdate.RunWorkerAsync();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            bwServicesUpdate.CancelAsync();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            if (Settings.UseNotificationArea == false) {
                App.MainForm = null;
                Application.Exit();
            }
        }

        private void Form_Resize(object sender, EventArgs e) {
            lsvServices.Columns[0].Width = lsvServices.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }


        private void mnuStart_Click(object sender, EventArgs e) {
            if (lsvServices.SelectedItems.Count > 0) {
                var serviceName = ((ServiceItem)lsvServices.SelectedItems[0].Tag).ServiceName;
                Transfer.Send(new Transfer("Start", serviceName));
            }
        }

        private void mnuStop_Click(object sender, EventArgs e) {
            if (lsvServices.SelectedItems.Count > 0) {
                var serviceName = ((ServiceItem)lsvServices.SelectedItems[0].Tag).ServiceName;
                Transfer.Send(new Transfer("Stop", serviceName));
            }
        }

        private void mnuAdd_Click(object sender, EventArgs e) {
            using (var frm = new AddServiceForm()) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    Tray.UpdateContextMenu();
                }
            }
        }

        private void mnuRemove_Click(object sender, EventArgs e) {
            if (lsvServices.SelectedItems.Count > 0) {
                var selectedServiceName = ((ServiceItem)lsvServices.SelectedItems[0].Tag).ServiceName;

                var serviceNames = new List<string>();
                foreach (var serviceName in Settings.ServiceNames) {
                    if (serviceName != selectedServiceName) {
                        serviceNames.Add(serviceName);
                    }
                }
                Settings.ServiceNames = serviceNames.ToArray();
                Tray.UpdateContextMenu();
            }
        }

        private void mnuOptions_Click(object sender, EventArgs e) {
            using (var frm = new OptionsForm()) {
                frm.ShowDialog(this);
            }
        }

        private void mnuReportABug_Click(object sender, EventArgs e) {
            Medo.Diagnostics.ErrorReport.ShowDialog(this, null, new Uri("http://jmedved.com/errorreport/"));
        }

        private void mnuAbout_Click(object sender, EventArgs e) {
            Medo.Windows.Forms.AboutBox.ShowDialog(this, new Uri("http://www.jmedved.com/seobiseu/"));
        }


        #region Context menu

        private void mnxServices_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            mnxServicesStart.Enabled = mnuStart.Enabled;
            mnxServicesStop.Enabled = mnuStop.Enabled;
            mnxServicesAdd.Enabled = mnuAdd.Enabled;
            mnxServicesRemove.Enabled = mnuRemove.Enabled;
        }

        private void mnxServicesStart_Click(object sender, EventArgs e) {
            mnuStart_Click(null, null);
        }

        private void mnxServicesStop_Click(object sender, EventArgs e) {
            mnuStop_Click(null, null);
        }

        private void mnxServicesAdd_Click(object sender, EventArgs e) {
            mnuAdd_Click(null, null);
        }

        private void mnxServicesRemove_Click(object sender, EventArgs e) {
            mnuRemove_Click(null, null);
        }

        #endregion


        private void bwServicesUpdate_DoWork(object sender, DoWorkEventArgs e) {
            var currItems = new List<ServiceItem>();
            while (!bwServicesUpdate.CancellationPending) {
                var services = new Dictionary<string, ServiceController>();
                foreach (var service in ServiceController.GetServices()) {
                    services.Add(service.ServiceName, service);
                }

                var newItems = new List<ServiceItem>();
                var newItemsToAdd = new List<ServiceItem>();
                var newItemsToChange = new List<ServiceItem>();
                var newItemsToRemove = new List<ServiceItem>();
                foreach (var serviceName in Settings.ServiceNames) {
                    ServiceItem item;
                    if (services.ContainsKey(serviceName)) {
                        item = new ServiceItem(services[serviceName]);
                    } else {
                        item = new ServiceItem(serviceName);
                    }

                    newItems.Add(item);
                    if (currItems.Contains(item)) {
                        newItemsToChange.Add(item);
                    } else {
                        newItemsToAdd.Add(item);
                    }
                }
                foreach (var item in currItems) {
                    if (newItems.Contains(item) == false) {
                        newItemsToRemove.Add(item);
                    }
                }
                if ((newItemsToAdd.Count > 0) || (newItemsToChange.Count > 0) || (newItemsToRemove.Count > 0)) {
                    bwServicesUpdate.ReportProgress(-1, new object[] { newItemsToAdd.AsReadOnly(), newItemsToChange.AsReadOnly(), newItemsToRemove.AsReadOnly() });
                }

                currItems = newItems;
                Thread.Sleep(500);
            }
        }

        private void bwServicesUpdate_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            var state = (object[])e.UserState;
            var toAdd = (IList<ServiceItem>)state[0];
            var toChange = (IList<ServiceItem>)state[1];
            var toRemove = (IList<ServiceItem>)state[2];

            lsvServices.BeginUpdate();

            for (int i = lsvServices.Items.Count - 1; i >= 0; i--) {
                var item = (ServiceItem)lsvServices.Items[i].Tag;
                if (toChange.Contains(item)) {
                    SetListViewItemContent(lsvServices.Items[i], toChange[toChange.IndexOf(item)]);
                } else if (toRemove.Contains(item)) {
                    lsvServices.Items.RemoveAt(i);
                }
            }

            foreach (var item in toAdd) {
                ListViewItem lvi = new ListViewItem();
                SetListViewItemContent(lvi, item);
                lsvServices.Items.Add(lvi);
            }

            lsvServices.Sort();

            lsvServices.EndUpdate();

            UpdateToolbar();
        }

        private static void SetListViewItemContent(ListViewItem item, ServiceItem zone) {
            item.SubItems.Clear();
            item.Tag = zone;
            item.Text = zone.DisplayName;
            item.ImageIndex = (int)zone.Status;
        }

        private void UpdateToolbar() {
            if (lsvServices.SelectedItems.Count > 0) {
                var service = (ServiceItem)lsvServices.SelectedItems[0].Tag;
                mnuStart.Enabled = (service.Status == ServiceControllerStatus.Stopped) || (service.Status == ServiceControllerStatus.Paused);
                mnuStop.Enabled = (service.Status == ServiceControllerStatus.Running);
                mnuRemove.Enabled = true;
            } else {
                mnuStart.Enabled = false;
                mnuStop.Enabled = false;
                mnuRemove.Enabled = false;
            }
        }

        private void lsvServices_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateToolbar();
        }

    }
}
