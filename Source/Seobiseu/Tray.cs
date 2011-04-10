﻿using System;
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
                    var item = new ServiceItem(service, serviceName);
                    var parent = new ToolStripMenuItem(item.DisplayName);
                    parent.DropDownItems.Add(new ToolStripMenuItem("Start", Medo.Resources.ManifestResources.GetBitmap("Seobiseu.Resources.Service-Start.png"), Menu_ServiceStart_OnClick) { Tag = item.ServiceName });
                    parent.DropDownItems.Add(new ToolStripMenuItem("Stop", Medo.Resources.ManifestResources.GetBitmap("Seobiseu.Resources.Service-Stop.png"), Menu_ServiceStop_OnClick) { Tag = item.ServiceName });
                    parent.Tag = new object[] { item.ServiceName, parent.DropDownItems[0], parent.DropDownItems[1] };
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
                        using (var service = new ServiceController(serviceName)) {
                            try {
                                item.Image = Medo.Resources.ManifestResources.GetBitmap("Seobiseu.Resources.Service-Status-" + ((int)service.Status).ToString(CultureInfo.InvariantCulture) + ".png");
                            } catch (InvalidOperationException) {
                                item.Image = null;
                            }
                        }
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
            var state = ((ToolStripMenuItem)sender).Tag as object[];
            if (state != null) {
                var serviceName = (string)state[0];
                var itemStart = (ToolStripMenuItem)state[1];
                var itemStop = (ToolStripMenuItem)state[2];
                using (var service = new ServiceController(serviceName)) {
                    try {
                        itemStart.Enabled = ((service.Status == ServiceControllerStatus.Stopped) || (service.Status == ServiceControllerStatus.Paused));
                        itemStop.Enabled = (service.Status == ServiceControllerStatus.Running);
                    } catch (InvalidOperationException) {
                        itemStart.Enabled = false;
                        itemStop.Enabled = false;
                    }
                }
            }
        }

        private static void Menu_ServiceStart_OnClick(object sender, EventArgs e) {
            var serviceName = ((ToolStripMenuItem)sender).Tag as string;
            Transfer.Send(new Transfer("Start", serviceName));
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
