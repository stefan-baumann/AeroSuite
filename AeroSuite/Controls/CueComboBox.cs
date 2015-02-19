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
    /// A ComboBox with cue banner support.
    /// </summary>
    /// <remarks>
    /// A cue banner is the text that is shown when the ComboBox does not have a selected item.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Cue TextBox")]
    [Description("A TextBox with cue banner support.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(TextBox))]
    public class CueComboBox
        : ComboBox, ITestControl
    {
        private const int CB_SETCUEBANNER = 0x1703;

        /// <summary>
        /// Initializes a new instance of the <see cref="CueComboBox"/> class.
        /// </summary>
        public CueComboBox()
            : base()
        { }



        private string cue = string.Empty;
        /// <summary>
        /// The text shown on the Cue Banner.
        /// </summary>
        /// <value>
        /// The cue.
        /// </value>
        [Category("Appearance")]
        [Description("The text shown on the Cue Banner.")]
        public string Cue
        {
            get
            {
                return this.cue;
            }
            set
            {
                this.cue = value;
                this.UpdateCue();
            }
        }

        /// <summary>
        /// Updates the cue.
        /// </summary>
        private void UpdateCue()
        {
            if (this.IsHandleCreated && PlatformHelper.VistaOrHigher)
            {
                NativeMethods.SendMessage(this.Handle, CB_SETCUEBANNER, IntPtr.Zero, this.cue);
            }
        }

        /// <summary>
        /// Raises the <see cref="HandleCreated" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.UpdateCue();
        }



        /// <summary>
        /// Prepares the control for tests (setting properties etc.).
        /// </summary>
        void ITestControl.PrepareForTests()
        {
            this.Text = string.Empty;
            this.Cue = "No item selected.";
            this.Items.AddRange(new string[] { "First Item", "Second Item", "Third Item", "Fourth Item", "Fifth Item" });
        }
    }
}
