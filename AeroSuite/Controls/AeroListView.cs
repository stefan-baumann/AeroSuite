using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AeroSuite.Controls
{
    /// <summary>
    /// An aero-styled ListView.
    /// </summary>
    /// <remarks>
    /// A ListView with the "Explorer"-WindowTheme applied and.
    /// If the operating system is Windows XP or older, nothing will be changed.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Aero ListView")]
    [Description("An aero-styled ListView.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ListView))]
    public class AeroListView
        : ListView, ITestControl
    {
        private const int LVS_EX_DOUBLEBUFFER = 0x10000;
        private const int LVM_SETEXTENDEDLISTVIEWSTYLE = 4150;

        /// <summary>
        /// Initializes a new instance of the <see cref="AeroListView"/> class.
        /// </summary>
        public AeroListView()
            : base()
        {
            this.FullRowSelect = true;
        }



        /// <summary>
        /// Raises the <see cref="E:HandleCreated" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (PlatformHelper.VistaOrHigher)
            {
                NativeMethods.SetWindowTheme(this.Handle, "explorer", null);
                NativeMethods.SendMessage(this.Handle, LVM_SETEXTENDEDLISTVIEWSTYLE, new IntPtr(LVS_EX_DOUBLEBUFFER), new IntPtr(LVS_EX_DOUBLEBUFFER));
            }
        }



        /// <summary>
        /// Prepares the control for tests (setting properties etc.).
        /// </summary>
        void ITestControl.PrepareForTests()
        {
            this.SmallImageList = TestDataProvider.SmallImageList;
            this.LargeImageList = TestDataProvider.LargeImageList;
            this.Items.AddRange(new string[] { "First Item", "Second Item", "Third Item", "Fourth Item", "Fifth Item" }.Select((s, i) => new ListViewItem(s, i)).ToArray());
            this.Size = new Size(400, 300);
        }
    }
}
