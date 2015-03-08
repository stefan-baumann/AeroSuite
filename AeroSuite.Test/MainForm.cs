using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
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
            //Load AeroSuite
            var library = Assembly.Load("AeroSuite");
            var exportedTypes = library.GetExportedTypes();
            var controls = exportedTypes.Where(t => typeof(Control).IsAssignableFrom(t) && !typeof(Form).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic && t.IsVisible && !t.IsGenericType);
            this.TypeComboBox.DataSource = controls.ToList();
            this.TypeComboBox.DisplayMember = "Name";
            this.TypeComboBox.SelectedIndex = 0;

            //Load stuff for TestDataProvider
            try
            {
                TestDataProvider.SmallImageList = new ImageList() { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(16, 16) };
                TestDataProvider.SmallImageList.Images.Add(new Icon(Properties.Resources.All_Users_Folder, new Size(16, 16)).ToBitmap());
                TestDataProvider.SmallImageList.Images.Add(new Icon(Properties.Resources.Pictures, new Size(16, 16)).ToBitmap());
                TestDataProvider.SmallImageList.Images.Add(new Icon(Properties.Resources.Recycle_Bin, new Size(16, 16)).ToBitmap());
                TestDataProvider.SmallImageList.Images.Add(new Icon(Properties.Resources.UAC, new Size(16, 16)).ToBitmap());
                TestDataProvider.SmallImageList.Images.Add(new Icon(Properties.Resources.Videos, new Size(16, 16)).ToBitmap());
                TestDataProvider.LargeImageList = new ImageList() { ColorDepth = ColorDepth.Depth32Bit, ImageSize = new Size(48, 48) };
                TestDataProvider.LargeImageList.Images.Add(new Icon(Properties.Resources.All_Users_Folder, new Size(48, 48)).ToBitmap());
                TestDataProvider.LargeImageList.Images.Add(new Icon(Properties.Resources.Pictures, new Size(48, 48)).ToBitmap());
                TestDataProvider.LargeImageList.Images.Add(new Icon(Properties.Resources.Recycle_Bin, new Size(48, 48)).ToBitmap());
                TestDataProvider.LargeImageList.Images.Add(new Icon(Properties.Resources.UAC, new Size(48, 48)).ToBitmap());
                TestDataProvider.LargeImageList.Images.Add(new Icon(Properties.Resources.Videos, new Size(48, 48)).ToBitmap());
            }
            catch (System.Reflection.TargetInvocationException)
            {
                //Throws an exception on Linux (don't know why yet)
            }
        }

        private Control currentControl;
        private void TypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.currentControl != null) this.currentControl.Dispose();
            var type = (Type)this.TypeComboBox.SelectedItem;
            this.splitContainer1.Panel1.Controls.Add(this.currentControl = (Control)Activator.CreateInstance(type));

            this.currentControl.Text = type.Name;
            this.currentControl.SizeChanged += (s, ea) => this.currentControl.Location = new Point((this.splitContainer1.Panel1.Width - this.currentControl.Width) / 2, (this.splitContainer1.Panel1.Height - this.currentControl.Height) / 2);
            this.currentControl.Location = new Point((this.splitContainer1.Panel1.Width - this.currentControl.Width) / 2, (this.splitContainer1.Panel1.Height - this.currentControl.Height) / 2);

            if (typeof(ITestControl).IsAssignableFrom(type))
            {
                (this.currentControl as ITestControl).PrepareForTests();
            }

            if (typeof (Button).IsAssignableFrom(type))
            {
                (this.currentControl as Button).UseVisualStyleBackColor = true;
            }


            this.ControlPropertyGrid.SelectedObject = this.currentControl;
        }

        private void HideGridButton_Click(object sender, EventArgs e)
        {
            this.ClientSize = new Size(this.splitContainer1.Panel1.Width, this.ClientSize.Height);
        }
    }
}
