using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Reflection;
using System.ServiceProcess;
using System.ComponentModel;
using System.Globalization;

namespace Seobiseu {
    internal static class Tray {

        private static NotifyIcon Notify;

        internal static void Show(bool interactive = false) {
            Tray.Notify = new NotifyIcon();
            Tray.Notify.Icon = GetApplicationIcon();
            Tray.Notify.Text = "Seobiseu";
            Tray.Notify.Visible = true;

            Tray.Notify.DoubleClick += Menu_Show_OnClick;

            Tray.UpdateContextMenu();
        }

        internal static void UpdateContextMenu() {
            if (Tray.Notify == null) { return; }

            Tray.Notify.ContextMenuStrip = new ContextMenuStrip();
            Tray.Notify.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Show", null, Menu_Show_OnClick));
            Tray.Notify.ContextMenuStrip.Items[0].Font = new Font(Tray.Notify.ContextMenuStrip.Items[0].Font, FontStyle.Bold);
            Tray.Notify.ContextMenuStrip.Items.Add("-");

            bool needsSeparator = false;
            foreach (var serviceName in Settings.ServiceNames) {
                using (var service = new ServiceController(serviceName)) {
                    var item = new ServiceItem(serviceName);
                    var parent = new ToolStripMenuItem(item.DisplayName);
                    parent.DropDownItems.Add(new ToolStripMenuItem("Start", Medo.Resources.ManifestResources.GetBitmap("Seobiseu.Resources.Service-Start.png"), Menu_ServiceStart_OnClick) { Tag = item.ServiceName });
                    parent.DropDownItems.Add(new ToolStripMenuItem("Restart", Medo.Resources.ManifestResources.GetBitmap("Seobiseu.Resources.Service-Restart.png"), Menu_ServiceRestart_OnClick) { Tag = item.ServiceName });
                    parent.DropDownItems.Add(new ToolStripMenuItem("Stop", Medo.Resources.ManifestResources.GetBitmap("Seobiseu.Resources.Service-Stop.png"), Menu_ServiceStop_OnClick) { Tag = item.ServiceName });
                    parent.Tag = new object[] { item.ServiceName, parent.DropDownItems[0], parent.DropDownItems[1], parent.DropDownItems[2] };
                    parent.DropDownOpening += new EventHandler(Menu_Service_DropDownOpening);
                    Tray.Notify.ContextMenuStrip.Items.Add(parent);
                    Tray.Notify.ContextMenuStrip.Opening += new CancelEventHandler(Menu_Opening);
                }
                needsSeparator = true;
            }

            if (needsSeparator) { Tray.Notify.ContextMenuStrip.Items.Add("-"); }
            Tray.Notify.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Exit", null, Menu_Exit_OnClick));
        }

        internal static void Hide() {
            if (Tray.Notify != null) {
                Tray.Notify.Visible = false;
            }
        }


        private static void Menu_Opening(object sender, System.ComponentModel.CancelEventArgs e) {
            foreach (ToolStripItem item in Tray.Notify.ContextMenuStrip.Items) {
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

        private static void Menu_Show_OnClick(object sender, EventArgs e) {
            if ((App.MainForm == null) || (App.MainForm.IsDisposed)) { App.MainForm = new MainForm(); }
            App.MainForm.Show();
            App.MainForm.Activate();
        }


        static void Menu_Service_DropDownOpening(object sender, EventArgs e) {
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

        private static void Menu_ServiceStart_OnClick(object sender, EventArgs e) {
            var serviceName = ((ToolStripMenuItem)sender).Tag as string;
            Transfer.Send(new Transfer("Start", serviceName));
        }

        private static void Menu_ServiceRestart_OnClick(object sender, EventArgs e) {
            var serviceName = ((ToolStripMenuItem)sender).Tag as string;
            var bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(delegate(object sender2, DoWorkEventArgs e2) {
                Transfer.Send(new Transfer("Stop", serviceName));
                using (var service = new ServiceController(serviceName)) {
                    service.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 5, 0));
                }
                Transfer.Send(new Transfer("Start", serviceName));
            });
            bw.RunWorkerAsync();
        }

        private static void Menu_ServiceStop_OnClick(object sender, EventArgs e) {
            var serviceName = ((ToolStripMenuItem)sender).Tag as string;
            Transfer.Send(new Transfer("Stop", serviceName));
        }

        private static void Menu_Exit_OnClick(object sender, EventArgs e) {
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
