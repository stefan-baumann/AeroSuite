using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AeroSuite.Controls
{
    /// <summary>
    /// A TextBox used for setting & displaying keyboard shortcuts/hotkeys.
    /// </summary>
    /// <remarks>
    /// This control implements the 'msctls_hotkey32' common control which means that all the actions are handled by windows internally so that the language is 
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Hotkey Box")]
    [Description("A TextBox used for hotkeys.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(TextBox))]
    public class HotkeyBox
        : TextBox
    {
        private const int HKM_SETHOTKEY = 1025;
        private const int HKM_GETHOTKEY = 1026;
        private const int HKM_SETRULES = 1027;

        /// <summary>
        /// Gets the required creation parameters when the control handle is created.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams baseParams = base.CreateParams;

                baseParams.ClassName = "msctls_hotkey32";

                return baseParams;
            }
        }

        /// <summary>
        /// Gets or sets the hotkey selected in this hotkey box.
        /// </summary>
        /// <value>
        /// The hotkey.
        /// </value>
        [Category("Data")]
        [Description("The hotkey selected in this hotkey box.")]
        [Localizable(false)]
        [Bindable(true)]
        public Keys Hotkey
        {
            get
            {
                return (Keys)(NativeMethods.SendMessage(this.Handle, HKM_GETHOTKEY, IntPtr.Zero, IntPtr.Zero));
            }
            set
            {
                NativeMethods.SendMessage(this.Handle, HKM_SETHOTKEY, (IntPtr)value, IntPtr.Zero);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set { }
        }
    }
}
