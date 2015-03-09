using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AeroSuite.Controls
{
    /// <summary>
    /// A CommandLink Button.
    /// </summary>
    /// <remarks>
    /// If used on Windows Vista or higher, the button will be a CommandLink: Basically the same functionality as a Button but looks different :P
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Command Link")]
    [Description("A CommandLink Button.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(CommandLink))]
    public class CommandLink
        : Button
    {
        private const int BS_COMMANDLINK = 0xE;
        private const int BCM_SETNOTE = 0x1609;

        /// <summary>
        /// Returns the default size.
        /// </summary>
        /// <value>
        /// The default size.
        /// </value>
        protected override Size DefaultSize
        {
            get
            {
                return new Size(180, 45); //Don't know if I should use 60 as default height instead as it is the default height when displaying a note
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandLink"/> class.
        /// </summary>
        public CommandLink()
            : base()
        {
            this.FlatStyle = FlatStyle.System;
        }

        private string note = string.Empty;
        /// <summary>
        /// The note text shown below the main text.
        /// </summary>
        /// <value>
        /// The note.
        /// </value>
        [Category("Appearance")]
        [Description("The note text shown below the main text.")]
        [Localizable(true)]
        public string Note
        {
            get
            {
                return this.note;

                //I currently don't use the following method as it would just cause a lot of overhead with no major advances

                //var textLength = (int)NativeMethods.SendMessage(this.Handle, 5643, IntPtr.Zero, IntPtr.Zero) + 1;
                //StringBuilder builder = new StringBuilder(textLength);
                //NativeMethods.SendMessage(this.Handle, 5642, ref textLength, builder);
                //return builder.ToString();
            }
            set
            {
                this.note = value;
                NativeMethods.SendMessage(this.Handle, BCM_SETNOTE, IntPtr.Zero, this.note);
            }
        }

        /// <summary>
        /// Overrides Button.CreateParams
        /// </summary>
        /// <value>
        /// The create parameters.
        /// </value>
        protected override CreateParams CreateParams
        {
            get
            {
                var param = base.CreateParams;
                if (PlatformHelper.VistaOrHigher) param.Style |= BS_COMMANDLINK;
                return param;
            }
        }
    }
}
