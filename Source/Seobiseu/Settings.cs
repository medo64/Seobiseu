using System;
using System.Collections.Generic;
using System.Text;

namespace Seobiseu {
    internal static class Settings {

        public static string[] ServiceNames {
            get { return Medo.Configuration.Settings.Read("ServiceNames", "").Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries); }
            set { Medo.Configuration.Settings.Write("ServiceNames", string.Join("|", value)); }
        }

    }
}
