using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using AeroSuite.Test.TestAdapters;

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
            //Load AeroSuite
            var library = Assembly.Load("AeroSuite");
            var exportedTypes = library.GetExportedTypes();
            var controls = exportedTypes.Where(t => typeof(Control).IsAssignableFrom(t) && !typeof(Form).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic && t.IsVisible && !t.IsGenericType);
            this.TypeComboBox.DataSource = controls.ToList();
            this.TypeComboBox.DisplayMember = "Name";
            this.TypeComboBox.SelectedIndex = 0;
        }

        private Control currentControl;
        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    if (this.currentControl != null) this.currentControl.Dispose();
                }
                catch
                {
                    //Ignore
                }

                var type = (Type)this.TypeComboBox.SelectedItem;
                this.splitContainer1.Panel1.Controls.Add(this.currentControl = (Control)Activator.CreateInstance(type));

                this.currentControl.Text = type.Name;
                this.currentControl.SizeChanged += (s, ea) => this.currentControl.Location = new Point((this.splitContainer1.Panel1.Width - this.currentControl.Width) / 2, (this.splitContainer1.Panel1.Height - this.currentControl.Height) / 2);
                this.currentControl.Location = new Point((this.splitContainer1.Panel1.Width - this.currentControl.Width) / 2, (this.splitContainer1.Panel1.Height - this.currentControl.Height) / 2);

                //Set up the control for testing - the test adapter is not used any further at the moment.
                var testAdapter = TestAdapters.TestAdapters.Item(this.currentControl);

                //To fix the issue with the button getting a white background color assigned randomly when being created.
                if (typeof(Button).IsAssignableFrom(type))
                {
                    (this.currentControl as Button).UseVisualStyleBackColor = true;
                }


                this.ControlPropertyGrid.SelectedObject = this.currentControl;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An " + ex.GetType().Name + " occured: " + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HideGridButton_Click(object sender, EventArgs e)
        {
            this.ClientSize = new Size(this.splitContainer1.Panel1.Width, this.ClientSize.Height);
        }
    }
}
