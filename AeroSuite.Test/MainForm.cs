using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroSuite.Test
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            var library = Assembly.Load("AeroSuite");
            var exportedTypes = library.GetExportedTypes();
            var controls = exportedTypes.Where(t => typeof(Control).IsAssignableFrom(t) && !typeof(Form).IsAssignableFrom(t));
            this.TypeComboBox.DataSource = controls.ToList();
            this.TypeComboBox.DisplayMember = "Name";
            this.TypeComboBox.SelectedIndex = 0;
        }

        private Control currentControl;
        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentControl != null) this.currentControl.Dispose();
            var type = (Type)this.TypeComboBox.SelectedItem;
            this.splitContainer1.Panel1.Controls.Add(this.currentControl = (Control)Activator.CreateInstance(type));

            this.currentControl.Text = type.Name;
            this.currentControl.Location = new Point((this.splitContainer1.Panel1.Width - this.currentControl.Width) / 2, (this.splitContainer1.Panel1.Height - this.currentControl.Height) / 2);
            this.currentControl.SizeChanged += (s, ea) => this.currentControl.Location = new Point((this.splitContainer1.Panel1.Width - this.currentControl.Width) / 2, (this.splitContainer1.Panel1.Height - this.currentControl.Height) / 2);

            if (typeof(ITestControl).IsAssignableFrom(type))
            {
                (this.currentControl as ITestControl).PrepareForTests();
            }


            this.ControlPropertyGrid.SelectedObject = this.currentControl;
        }
    }
}
