using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Seobiseu {
    public partial class OptionsForm : Form {
        
        public OptionsForm() {
            InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
        }


        Medo.Configuration.RunOnStartup runOnStartup = new Medo.Configuration.RunOnStartup(Medo.Configuration.RunOnStartup.Current.Title, Medo.Configuration.RunOnStartup.Current.ExecutablePath, "/tray");


        private void OptionsForm_Load(object sender, EventArgs e) {
            chbRunOnStartup.Checked = runOnStartup.RunForCurrentUser;
        }


        private void btnOK_Click(object sender, EventArgs e) {
            runOnStartup.RunForCurrentUser = chbRunOnStartup.Checked;
        }

    }
}
