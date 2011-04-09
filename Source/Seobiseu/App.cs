using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace Seobiseu {
    internal static class App {

        [STAThread]
        internal static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }

    }
}
