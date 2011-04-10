
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceProcess;

namespace Seobiseu {
    public class ServiceItem {

        public ServiceItem(ServiceController service) {
            this.DisplayName = service.DisplayName;
            this.ServiceName = service.ServiceName;
            this.Status = service.Status;
        }

        public ServiceItem(ServiceController service, string serviceName) {
            try {
                this.DisplayName = service.DisplayName;
                this.ServiceName = service.ServiceName;
                this.Status = service.Status;
            } catch (InvalidOperationException) {
                this.DisplayName = serviceName + "?";
                this.ServiceName = serviceName;
                this.Status = 0;
            }
        }

        public ServiceItem(string serviceName) {
            this.DisplayName = serviceName + "?";
            this.ServiceName = serviceName;
            this.Status = 0;
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
