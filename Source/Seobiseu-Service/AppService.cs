using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;

namespace SeobiseuService {
    internal class AppService : ServiceBase {

        private static AppService _instance = new AppService();
        public static AppService Instance { get { return _instance; } }

        private AppService() {
            this.AutoLog = true;
            this.CanStop = true;
            this.ServiceName = "Seobiseu";
        }

        protected override void OnStart(string[] args) {
            ServiceThread.Start();
        }

        protected override void OnStop() {
            ServiceThread.Stop();
        }

    }
}
