using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace Seobiseu {
    internal partial class MainForm : Form {

        private ManualResetEvent AllowedRefreshEvent = new ManualResetEvent(true);

        public MainForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            mnu.Font = SystemFonts.MessageBoxFont;
            lsvServices.Font = SystemFonts.MessageBoxFont;
            sta.Font = SystemFonts.MessageBoxFont;

            mnu.Renderer = Helpers.ToolStripBorderlessSystemRendererInstance;
            Helpers.UpdateToolstripImages(imlServiceStatus, mnu, mnxServices);

            Medo.Windows.Forms.State.SetupOnLoadAndClose(this);
        }


        private void Form_KeyDown(object sender, KeyEventArgs e) {
            switch (e.KeyData) {
                case Keys.Alt | Keys.Menu:
                    {
                        mnu.Select();
                        mnuStart.Select();
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;

                case Keys.Escape:
                    {
                        if (Settings.UseNotificationArea) {
                            this.Close();
                            e.Handled = true;
                            e.SuppressKeyPress = true;
                        }
                    }
                    break;

                case Keys.F5:
                    {
                        mnuStart_Click(null, null);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;

                case Keys.Shift | Keys.F5:
                    {
                        mnuStop_Click(null, null);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;

                case Keys.Insert:
                    {
                        mnuAdd_Click(null, null);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;

                case Keys.Delete:
                    {
                        mnuRemove_Click(null, null);
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                    }
                    break;

            }
        }

        private void Form_Load(object sender, EventArgs e) {
            bwServicesUpdate.RunWorkerAsync();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e) {
            App.TrayContext.CancelForm();
            bwServicesUpdate.CancelAsync();
            this.AllowedRefreshEvent.Set();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
#if DEBUG
            Application.Exit();
#else
            if (Settings.UseNotificationArea == false) {
                Application.Exit();
            }
#endif
        }

        private void Form_Resize(object sender, EventArgs e) {
            lsvServices.Columns[0].Width = lsvServices.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
        }


        private void mnuStart_Click(object sender, EventArgs e) {
            if (lsvServices.SelectedItems.Count > 0) {
                var serviceName = ((ServiceItem)lsvServices.SelectedItems[0].Tag).ServiceName;
                try {
                    Transfer.Send(new Transfer("Start", serviceName));
                } catch (Win32Exception ex) {
                    Medo.MessageBox.ShowError(this, "Cannot contact Seobiseu service.\n\n" + ex.Message);
                }
            }
        }

        private void mnuStop_Click(object sender, EventArgs e) {
            if (lsvServices.SelectedItems.Count > 0) {
                var serviceName = ((ServiceItem)lsvServices.SelectedItems[0].Tag).ServiceName;
                try {
                    Transfer.Send(new Transfer("Stop", serviceName));
                } catch (Win32Exception ex) {
                    Medo.MessageBox.ShowError(this, "Cannot contact Seobiseu service.\n\n" + ex.Message);
                }
            }
        }

        private void mnuAdd_Click(object sender, EventArgs e) {
            using (var frm = new AddServiceForm()) {
                if (frm.ShowDialog(this) == DialogResult.OK) {
                    App.TrayContext.UpdateContextMenu();
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
                App.TrayContext.UpdateContextMenu();
            }
        }

        private void mnuAppOptions_Click(object sender, EventArgs e) {
            using (var frm = new OptionsForm()) {
                frm.ShowDialog(this);
            }
        }

        private void mnuAppFeedback_Click(object sender, EventArgs e) {
            Medo.Diagnostics.ErrorReport.ShowDialog(this, null, new Uri("https://medo64.com/feedback/"));
        }

        private void mnuAppAbout_Click(object sender, EventArgs e) {
            Medo.Windows.Forms.AboutBox.ShowDialog(this, new Uri("https://www.medo64.com/seobiseu/"));
        }


        #region Context menu

        private void mnxServices_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            mnxServicesStart.Enabled = mnuStart.Enabled;
            mnxServicesRestart.Enabled = mnuStop.Enabled;
            mnxServicesStop.Enabled = mnuStop.Enabled;
            mnxServicesAdd.Enabled = mnuAdd.Enabled;
            mnxServicesRemove.Enabled = mnuRemove.Enabled;
        }

        private void mnxServicesStart_Click(object sender, EventArgs e) {
            mnuStart_Click(null, null);
        }

        private void mnxServicesRestart_Click(object sender, EventArgs e) {
            if (lsvServices.SelectedItems.Count > 0) {
                var serviceName = ((ServiceItem)lsvServices.SelectedItems[0].Tag).ServiceName;
                var bw = new BackgroundWorker();
                bw.DoWork += new DoWorkEventHandler(delegate (object sender2, DoWorkEventArgs e2) {
                    try {
                        Transfer.Send(new Transfer("Stop", serviceName));
                        using (var service = new ServiceController(serviceName)) {
                            service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 5, 0));
                        }
                        Transfer.Send(new Transfer("Start", serviceName));
                    } catch (Win32Exception ex) {
                        Medo.MessageBox.ShowError(this, "Cannot contact Seobiseu service.\n\n" + ex.Message);
                    }
                });
                bw.RunWorkerAsync();
            }
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
                var newItems = new List<ServiceItem>();
                var newItemsToAdd = new List<ServiceItem>();
                var newItemsToChange = new List<ServiceItem>();
                var newItemsToRemove = new List<ServiceItem>();
                foreach (var serviceName in Settings.ServiceNames) {
                    ServiceItem item = new ServiceItem(serviceName);

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
                    while (this.AllowedRefreshEvent.WaitOne(100) == false) { }
                    if (bwServicesUpdate.CancellationPending) { return; }
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

            lsvServices.EndUpdate();

            UpdateToolbar();
        }

        private static void SetListViewItemContent(ListViewItem item, ServiceItem zone) {
            item.SubItems.Clear();
            item.Tag = zone;
            item.Text = zone.DisplayName;
            item.ImageIndex = zone.StatusIndex;
        }

        private void UpdateToolbar() {
            if (lsvServices.SelectedItems.Count > 0) {
                var service = (ServiceItem)lsvServices.SelectedItems[0].Tag;
                mnuStart.Enabled = service.CanStart;
                mnuStop.Enabled = service.CanStop;
                mnuRemove.Enabled = true;
                staStatus.Text = service.StatusText;
                staServiceName.Text = service.ServiceName;
            } else {
                mnuStart.Enabled = false;
                mnuStop.Enabled = false;
                mnuRemove.Enabled = false;
                staStatus.Text = "";
                staServiceName.Text = "";
            }
        }

        private void lsvServices_SelectedIndexChanged(object sender, EventArgs e) {
            UpdateToolbar();
        }

        private void lsvServices_ItemDrag(object sender, ItemDragEventArgs e) {
            this.AllowedRefreshEvent.Reset(); //to prevent updates
            lsvServices.DoDragDrop(lsvServices.SelectedItems, DragDropEffects.Move);
        }

        private void lsvServices_DragEnter(object sender, DragEventArgs e) {
            System.Diagnostics.Debug.WriteLine("ENTER");
            for (int i = 0; i < e.Data.GetFormats().Length; i++) {
                if (e.Data.GetFormats()[i].Equals("System.Windows.Forms.ListView+SelectedListViewItemCollection")) {
                    e.Effect = DragDropEffects.Move;
                    break;
                }
            }
        }

        private void lsvServices_DragDrop(object sender, DragEventArgs e) {
            System.Diagnostics.Debug.WriteLine("DROP");
            if (lsvServices.SelectedItems.Count == 0) {
                this.AllowedRefreshEvent.Set();
                return;
            }

            Point cp = lsvServices.PointToClient(new Point(e.X, e.Y));
            ListViewItem dragToItem = lsvServices.GetItemAt(cp.X, cp.Y);
            if (dragToItem == null) {
                this.AllowedRefreshEvent.Set();
                return;
            }

            int dragIndex = dragToItem.Index;
            var sel = new ListViewItem[lsvServices.SelectedItems.Count];
            for (int i = 0; i < lsvServices.SelectedItems.Count; i++) {
                sel[i] = lsvServices.SelectedItems[i];
            }

            ListViewItem insertItem = null;
            for (int i = 0; i < sel.Length; i++) {
                var dragItem = sel[i];
                int itemIndex = dragIndex;
                if (itemIndex == dragItem.Index) {
                    this.AllowedRefreshEvent.Set();
                    return;
                }

                if (dragItem.Index < itemIndex) {
                    itemIndex++;
                } else {
                    itemIndex = dragIndex + i;
                }

                insertItem = (ListViewItem)dragItem.Clone();
                lsvServices.Items.Insert(itemIndex, insertItem);
                lsvServices.Items.Remove(dragItem);
            }

            if (insertItem != null) {
                insertItem.Selected = true;
                insertItem.Focused = true;
            }

            var serviceNames = new List<string>();
            foreach (ListViewItem item in lsvServices.Items) {
                var serviceName = ((ServiceItem)item.Tag).ServiceName;
                serviceNames.Add(serviceName);
            }
            Settings.ServiceNames = serviceNames.ToArray();
            App.TrayContext.UpdateContextMenu();

            this.AllowedRefreshEvent.Set(); //to allow updates after drag
        }

        private void lsvServices_MouseUp(object sender, MouseEventArgs e) {
            this.AllowedRefreshEvent.Set(); //to allow updates after drag (in case that there wasn't drag)
        }

    }
}
