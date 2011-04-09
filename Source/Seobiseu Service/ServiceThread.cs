using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace SeobiseuService {
    internal static class ServiceThread {

        private static Thread Thread;
        private static ManualResetEvent CancelEvent;

        public static void Start() {
            if (ServiceThread.Thread != null) { return; }

            ServiceThread.CancelEvent = new ManualResetEvent(false);
            ServiceThread.Thread = new Thread(Run);
            ServiceThread.Thread.Name = "Service";
            ServiceThread.Thread.IsBackground = true;
            ServiceThread.Thread.CurrentCulture = CultureInfo.InvariantCulture;
            ServiceThread.Thread.Start();
        }

        public static void Stop() {
            try {
                ServiceThread.CancelEvent.Set();
                if (ServiceThread.Handle != null) { Transfer.Cancel(ServiceThread.Handle); }
                while (ServiceThread.Thread.IsAlive) { Thread.Sleep(10); }
                ServiceThread.Thread = null;
            } catch { }
        }


        private static SafeHandleMinusOneIsInvalid Handle = null;

        private static void Run() {
            while (!ServiceThread.CancelEvent.WaitOne(0, false)) {
                try {
                    ServiceThread.Handle = Transfer.Create();

                    while (!ServiceThread.CancelEvent.WaitOne(0, false)) {
                        var trans = Transfer.Receive(ServiceThread.Handle);

                        try {

                            switch (trans.Command) {

                                case "Start": {
                                        var service = new ServiceController(trans.ServiceName);
                                        service.Start();
                                    } break;

                                case "Stop": {
                                        var service = new ServiceController(trans.ServiceName);
                                        service.Stop();
                                    } break;

                            }

                        } catch (InvalidOperationException ex) {
                            Trace.TraceWarning(ex.Message);
                        }

                    }

                } catch (Win32Exception) {
                } catch (ThreadAbortException) {
                } finally {
                    if (ServiceThread.Handle != null) { ServiceThread.Handle.Close(); }
                }
            }
        }

    }
}
