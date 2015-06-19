using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Reflection;
using System.ServiceProcess;
using System.ComponentModel;
using System.Globalization;

namespace Seobiseu {
    internal class TrayContext : ApplicationContext {

        private NotifyIcon Notify;
        public Form Form { get; private set; }

        internal void ShowIcon(bool interactive = false) {
            if (this.Notify != null) {
                this.Notify.Visible = false;
                this.Notify.Dispose();
            }
            this.Notify = new NotifyIcon();
            this.Notify.Icon = GetApplicationIcon();
            this.Notify.Text = "Seobiseu";
            this.Notify.Visible = true;

            this.Notify.DoubleClick += Menu_Show_OnClick;

            this.UpdateContextMenu();
        }

        internal void UpdateContextMenu() {
            if (this.Notify == null) { return; }

            this.Notify.ContextMenuStrip = new ContextMenuStrip();
            this.Notify.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Show", null, Menu_Show_OnClick));
            this.Notify.ContextMenuStrip.Items[0].Font = new Font(this.Notify.ContextMenuStrip.Items[0].Font, FontStyle.Bold);
            this.Notify.ContextMenuStrip.Items.Add("-");

            bool needsSeparator = false;
            foreach (var serviceName in Settings.ServiceNames) {
                using (var service = new ServiceController(serviceName)) {
                    var item = new ServiceItem(serviceName);
                    var parent = new ToolStripMenuItem(item.DisplayName);
                    parent.DropDownItems.Add(new ToolStripMenuItem("Start", Seobiseu.Properties.Resources.mnuStart_16, Menu_ServiceStart_OnClick) { Tag = item.ServiceName });
                    parent.DropDownItems.Add(new ToolStripMenuItem("Restart", Seobiseu.Properties.Resources.mnuRestart_16, Menu_ServiceRestart_OnClick) { Tag = item.ServiceName });
                    parent.DropDownItems.Add(new ToolStripMenuItem("Stop", Seobiseu.Properties.Resources.mnuStop_16, Menu_ServiceStop_OnClick) { Tag = item.ServiceName });
                    parent.Tag = new object[] { item.ServiceName, parent.DropDownItems[0], parent.DropDownItems[1], parent.DropDownItems[2] };
                    parent.DropDownOpening += new EventHandler(Menu_Service_DropDownOpening);
                    this.Notify.ContextMenuStrip.Items.Add(parent);
                    this.Notify.ContextMenuStrip.Opening += new CancelEventHandler(Menu_Opening);
                }
                needsSeparator = true;
            }

            if (needsSeparator) { this.Notify.ContextMenuStrip.Items.Add("-"); }
            this.Notify.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Exit", null, Menu_Exit_OnClick));
        }

        internal void HideIcon() {
            if (this.Notify != null) {
                this.Notify.Visible = false;
            }
        }

        internal void ShowForm() {
            if ((this.Form == null) || (this.Form.IsDisposed)) { this.Form = new MainForm(); }
            if (this.Form.IsHandleCreated == false) {
                this.Form.CreateControl();
                this.Form.Handle.GetType();
            }
            this.Form.Show();
            if (this.Form.WindowState == FormWindowState.Minimized) { this.Form.WindowState = FormWindowState.Normal; }
            this.Form.Activate();
        }

        internal void CancelForm() {
            this.Form = null;
        }


        private void Menu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            foreach (ToolStripItem item in this.Notify.ContextMenuStrip.Items) {
                var state = item.Tag as object[];
                if (state != null) {
                    var serviceName = (string)state[0];
                    if (serviceName != null) {
                        var serviceItem = new ServiceItem(serviceName);
                        item.Image = serviceItem.Image;
                    }
                }
            }
        }

        private void Menu_Show_OnClick(object sender, EventArgs e) {
            this.ShowForm();
        }


        private void Menu_Service_DropDownOpening(object sender, EventArgs e) {
            var itemParent = (ToolStripMenuItem)sender;
            var state = itemParent.Tag as object[];
            if (state != null) {
                var serviceName = (string)state[0];
                var itemStart = (ToolStripMenuItem)state[1];
                var itemRestart = (ToolStripMenuItem)state[2];
                var itemStop = (ToolStripMenuItem)state[3];
                var serviceItem = new ServiceItem(serviceName);
                itemParent.Image = serviceItem.Image;
                itemStart.Enabled = serviceItem.CanStart;
                itemRestart.Enabled = serviceItem.CanRestart;
                itemStop.Enabled = serviceItem.CanStop;
            }
        }

        private void Menu_ServiceStart_OnClick(object sender, EventArgs e) {
            var serviceName = ((ToolStripMenuItem)sender).Tag as string;
            try {
                Transfer.Send(new Transfer("Start", serviceName));
            } catch (Win32Exception ex) {
                Medo.MessageBox.ShowError(null, "Cannot contact Seobiseu service.\n\n" + ex.Message);
            }
        }

        private void Menu_ServiceRestart_OnClick(object sender, EventArgs e) {
            var serviceName = ((ToolStripMenuItem)sender).Tag as string;
            var bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(delegate (object sender2, DoWorkEventArgs e2) {
                try {
                    Transfer.Send(new Transfer("Stop", serviceName));
                    using (var service = new ServiceController(serviceName)) {
                        service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 5, 0));
                    }
                    Transfer.Send(new Transfer("Start", serviceName));
                } catch (Win32Exception ex) {
                    Medo.MessageBox.ShowError(null, "Cannot contact Seobiseu service.\n\n" + ex.Message);
                }
            });
            bw.RunWorkerAsync();
        }

        private void Menu_ServiceStop_OnClick(object sender, EventArgs e) {
            var serviceName = ((ToolStripMenuItem)sender).Tag as string;
            try {
                Transfer.Send(new Transfer("Stop", serviceName));
            } catch (Win32Exception ex) {
                Medo.MessageBox.ShowError(null, "Cannot contact Seobiseu service.\n\n" + ex.Message);
            }
        }

        private void Menu_Exit_OnClick(object sender, EventArgs e) {
            Application.Exit();
        }



        #region Helpers

        private static Icon GetApplicationIcon() {
            IntPtr hLibrary = NativeMethods.LoadLibrary(Assembly.GetEntryAssembly().Location);
            if (!hLibrary.Equals(IntPtr.Zero)) {
                IntPtr hIcon = NativeMethods.LoadImage(hLibrary, "#32512", NativeMethods.IMAGE_ICON, 20, 20, 0);
                if (!hIcon.Equals(System.IntPtr.Zero)) {
                    Icon icon = Icon.FromHandle(hIcon);
                    if (icon != null) { return icon; }
                }
            }
            return null;
        }

        private static class NativeMethods {

            public const UInt32 IMAGE_ICON = 1;


            [DllImport("user32.dll", CharSet = CharSet.Unicode)]
            static extern internal IntPtr LoadImage(IntPtr hInstance, String lpIconName, UInt32 uType, Int32 cxDesired, Int32 cyDesired, UInt32 fuLoad);

            [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
            static extern internal IntPtr LoadLibrary(string lpFileName);

        }

        #endregion

    }
}
