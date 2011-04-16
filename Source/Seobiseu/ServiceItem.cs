
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;
using System.Drawing;
using System.Globalization;

namespace Seobiseu {
    public class ServiceItem {

        public ServiceItem(string serviceName) {
            try {
                using (var service = new ServiceController(serviceName)) {
                    this.DisplayName = service.DisplayName;
                    this.ServiceName = service.ServiceName;
                    this.Status = service.Status;
                }
            } catch (InvalidOperationException) {
                this.DisplayName = serviceName + "?";
                this.ServiceName = serviceName;
                this.Status = 0;
            }
        }

        public ServiceItem(ServiceController service) {
            try {
                this.ServiceName = service.ServiceName;
            } catch (InvalidOperationException) {
                this.ServiceName = "";
            }
            try {
                this.DisplayName = service.DisplayName;
            } catch (InvalidOperationException) {
                this.DisplayName = this.ServiceName + "?";
            }
            try {
                this.Status = service.Status;
            } catch (InvalidOperationException) {
                this.Status = 0;
            }
        }

        public string DisplayName { get; private set; }
        public string ServiceName { get; private set; }
        public ServiceControllerStatus Status { get; private set; }


        public Image Image {
            get {
                if ((this.Status == 0) || ((int)this.Status > 7)) {
                    return null;
                } else {
                    return Medo.Resources.ManifestResources.GetBitmap("Seobiseu.Resources.Service-Status-" + ((int)this.Status).ToString(CultureInfo.InvariantCulture) + ".png");
                }
            }
        }

        public bool CanStart {
            get { return ((this.Status == ServiceControllerStatus.Stopped) || (this.Status == ServiceControllerStatus.Paused)); }
        }

        public bool CanRestart {
            get { return (this.Status == ServiceControllerStatus.Running); }
        }

        public bool CanStop {
            get { return (this.Status == ServiceControllerStatus.Running); }
        }

        public string StatusText {
            get {
                switch (this.Status) {
                    case ServiceControllerStatus.Stopped: return "Stopped.";
                    case ServiceControllerStatus.StartPending: return "Start pending...";
                    case ServiceControllerStatus.StopPending: return "Stop pending...";
                    case ServiceControllerStatus.Running: return "Running.";
                    case ServiceControllerStatus.ContinuePending: return "Continue pending...";
                    case ServiceControllerStatus.PausePending: return "Pause pending...";
                    case ServiceControllerStatus.Paused: return "Paused.";
                    default: { return "Unknown state."; }
                }
            }
        }

        public int StatusIndex {
            get {
                switch (this.Status) {
                    case ServiceControllerStatus.Stopped: return 1;
                    case ServiceControllerStatus.StartPending: return 2;
                    case ServiceControllerStatus.StopPending: return 3;
                    case ServiceControllerStatus.Running: return 4;
                    case ServiceControllerStatus.ContinuePending: return 5;
                    case ServiceControllerStatus.PausePending: return 6;
                    case ServiceControllerStatus.Paused: return 7;
                    default: { return 0; }
                }
            }
        }


        public override bool Equals(object obj) {
            var other = obj as ServiceItem;
            if ((other != null) && (this.ServiceName == other.ServiceName)) { return true; }
            return false;
        }

        public override int GetHashCode() {
            return this.ServiceName.GetHashCode();
        }

        public override string ToString() {
            return this.DisplayName;
        }

    }
}
