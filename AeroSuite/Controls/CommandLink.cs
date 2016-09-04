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
        [Bindable(true)]
        public virtual string Note
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
                this.UpdateNote();
            }
        }

        /// <summary>
        /// Updates the note.
        /// </summary>
        protected void UpdateNote()
        {
            if (PlatformHelper.VistaOrHigher)
            {
                NativeMethods.SendMessage(this.Handle, BCM_SETNOTE, IntPtr.Zero, this.note);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether control's elements are aligned to support locales using right-to-left fonts.
        /// </summary>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
                this.UpdateNote();
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
