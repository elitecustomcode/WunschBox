using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WunschBox
{
    public partial class Settings : Form
    {
        Form1 _form;
        public Settings(Form1 form)
        {
            InitializeComponent();
            _form = form;
            trackBar1.Value = Convert.ToInt32(Properties.Settings.Default.Opacity.ToString("p0").Replace(" %", ""));
            textBoxAppName.Text = Properties.Settings.Default.Application;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var percentage = trackBar1.Value;

            Properties.Settings.Default.Opacity = ((double)percentage / 100);
            if (!String.IsNullOrEmpty(textBoxAppName.Text))
            {
                Properties.Settings.Default.Application = textBoxAppName.Text;
            }
            Properties.Settings.Default.Save();
            _form.ChangeOpacity(Properties.Settings.Default.Opacity);
            this.Close();
        }
    }
}
