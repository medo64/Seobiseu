
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;

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
