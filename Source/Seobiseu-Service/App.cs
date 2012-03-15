using System;
using System.Configuration.Install;
using System.Globalization;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace SeobiseuService {
    internal static class App {

        [STAThread]
        internal static void Main() {
            System.AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            if (Medo.Application.Args.Current.ContainsKey("Interactive")) {

                Tray.Show();
                ServiceThread.Start();
                Tray.SetStatusToRunningInteractive();
                Application.Run();
                ServiceThread.Stop();
                Tray.Hide();
                Environment.Exit(0);

            } else if (Medo.Application.Args.Current.ContainsKey("Install")) {

                ManagedInstallerClass.InstallHelper(new string[] { Assembly.GetExecutingAssembly().Location });
                System.Environment.Exit(0);

            } else if (Medo.Application.Args.Current.ContainsKey("Uninstall")) {

                try {
                    ManagedInstallerClass.InstallHelper(new string[] { "/u", Assembly.GetExecutingAssembly().Location });
                    System.Environment.Exit(0);
                } catch (System.Configuration.Install.InstallException) { //no service with that name
                    System.Environment.Exit(-1);
                }

            } else {

                if (Environment.UserInteractive) {
                    Tray.Show();
                    ServiceStatusThread.Start();
                    Application.Run();
                    ServiceStatusThread.Stop();
                    Tray.Hide();
                    Environment.Exit(0);
                } else {
                    ServiceBase.Run(new ServiceBase[] { AppService.Instance });
                }

            }
        }


        internal static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
#if !DEBUG
            Medo.Diagnostics.ErrorReport.SaveToTemp(e.ExceptionObject as Exception);
            AppService.Instance.ExitCode = 1064; //ERROR_EXCEPTION_IN_SERVICE
            AppService.Instance.AutoLog = false;
            Thread.Sleep(1000); //just to sort it properly in event log.
            Environment.Exit(AppService.Instance.ExitCode);
#else
            throw (e.ExceptionObject as Exception);
#endif
        }

    }
}
