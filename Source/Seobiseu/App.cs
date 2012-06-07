using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace Seobiseu {
    internal static class App {

        public static MainForm MainForm = null;
        public static Form ThreadProxyForm = null;

        [STAThread]
        static void Main() {
            bool createdNew;
            var mutexSecurity = new MutexSecurity();
            mutexSecurity.AddAccessRule(new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow));
            using (var setupMutex = new Mutex(false, @"Global\JosipMedved_Seobiseu", out createdNew, mutexSecurity)) {

                System.Windows.Forms.Application.EnableVisualStyles();
                System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Medo.Configuration.Settings.Read("CultureName", "en-US"));

                Medo.Application.UnhandledCatch.ThreadException += new EventHandler<ThreadExceptionEventArgs>(UnhandledCatch_ThreadException);
                Medo.Application.UnhandledCatch.Attach();

                App.ThreadProxyForm = new Form();
                App.ThreadProxyForm.CreateControl();
                App.ThreadProxyForm.Handle.GetType();

                Medo.Application.SingleInstance.NewInstanceDetected += new EventHandler<Medo.Application.NewInstanceEventArgs>(SingleInstance_NewInstanceDetected);
                if (Medo.Application.SingleInstance.IsOtherInstanceRunning) {
                    var currProcess = Process.GetCurrentProcess();
                    foreach (var iProcess in Process.GetProcessesByName(currProcess.ProcessName)) {
                        try {
                            if (iProcess.Id != currProcess.Id) {
                                NativeMethods.AllowSetForegroundWindow(iProcess.Id);
                                break;
                            }
                        } catch (Win32Exception) { }
                    }
                }
                Medo.Application.SingleInstance.Attach();


                if (Settings.UseNotificationArea) {
                    Tray.Show();
                }

                if (Medo.Application.Args.Current.ContainsKey("tray") == false) {
                    App.MainForm = new MainForm();
                    App.MainForm.Show();
                }

                Application.Run();
                Tray.Hide();
            }
        }



        private static void UnhandledCatch_ThreadException(object sender, ThreadExceptionEventArgs e) {
#if !DEBUG
            Medo.Diagnostics.ErrorReport.ShowDialog(null, e.Exception, new Uri("http://jmedved.com/feedback/"));
#else
            throw e.Exception;
#endif
        }


        private static void SingleInstance_NewInstanceDetected(object sender, Medo.Application.NewInstanceEventArgs e) {
            try {
                NewInstanceDetectedProcDelegate method = new NewInstanceDetectedProcDelegate(NewInstanceDetectedProc);
                App.ThreadProxyForm.Invoke(method);
            } catch (Exception) { }
        }

        private delegate void NewInstanceDetectedProcDelegate();

        private static void NewInstanceDetectedProc() {
            if (App.MainForm == null) { App.MainForm = new MainForm(); }
            if (App.MainForm.IsHandleCreated == false) {
                App.MainForm.CreateControl();
                App.MainForm.Handle.GetType();
            }

            App.MainForm.Show();
            if (App.MainForm.WindowState == FormWindowState.Minimized) { App.MainForm.WindowState = FormWindowState.Normal; }
            App.MainForm.Activate();
        }



        private static class NativeMethods {

            [DllImportAttribute("user32.dll", EntryPoint = "AllowSetForegroundWindow")]
            [return: MarshalAsAttribute(UnmanagedType.Bool)]
            public static extern bool AllowSetForegroundWindow(int dwProcessId);

        }

    }
}
