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

        public static Form ThreadProxyForm = null;
        public static TrayContext TrayContext = null;

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
                Medo.Application.Restart.Register("/restart");

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

                TrayContext = new TrayContext();
                if (Settings.UseNotificationArea) { TrayContext.ShowIcon(); }

                if (Medo.Application.Args.Current.ContainsKey("tray") == false) {
                    TrayContext.ShowForm();
                }

                Application.Run(TrayContext);
                TrayContext.HideIcon();
            }
        }



        private static void UnhandledCatch_ThreadException(object sender, ThreadExceptionEventArgs e) {
#if !DEBUG
            Medo.Diagnostics.ErrorReport.ShowDialog(null, e.Exception, new Uri("https://medo64.com/feedback/"));
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
            if (App.TrayContext != null) {
                App.TrayContext.ShowForm();
            }
        }



        private static class NativeMethods {

            [DllImportAttribute("user32.dll", EntryPoint = "AllowSetForegroundWindow")]
            [return: MarshalAsAttribute(UnmanagedType.Bool)]
            public static extern bool AllowSetForegroundWindow(int dwProcessId);

        }

    }
}
