using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Seobiseu {
    internal static class App {

        private static readonly Mutex SetupMutex = new Mutex(false, @"Global\JosipMedved_Seobiseu");
        public static MainForm MainForm = null;


        [STAThread]
        static void Main() {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Medo.Configuration.Settings.Read("CultureName", "en-US"));

            Medo.Application.UnhandledCatch.ThreadException += new EventHandler<ThreadExceptionEventArgs>(UnhandledCatch_ThreadException);
            Medo.Application.UnhandledCatch.Attach();

            if (Settings.UseNotificationArea) {
                Tray.Show();
            }

            if (Medo.Application.Args.Current.ContainsKey("tray") == false) {
                App.MainForm = new MainForm();
                App.MainForm.Show();
            }

            Application.Run();
            Tray.Hide();

            SetupMutex.Close();
        }



        private static void UnhandledCatch_ThreadException(object sender, ThreadExceptionEventArgs e) {
#if !DEBUG
            Medo.Diagnostics.ErrorReport.ShowDialog(null, e.Exception, new Uri("http://jmedved.com/ErrorReport/"));
#else
            throw e.Exception;
#endif
        }


        private static class NativeMethods {

            [DllImport("Shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern UInt32 SetCurrentProcessExplicitAppUserModelID(String AppID);

        }

    }
}
