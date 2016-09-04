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
    /// A TextBox with cue banner support.
    /// </summary>
    /// <remarks>
    /// A cue banner is the text that is shown when the TextBox is empty.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Cue TextBox")]
    [Description("A TextBox with cue banner support.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(TextBox))]
    public class CueTextBox
        : TextBox
    {
        private const int EM_SETCUEBANNER = 0x1501;

        /// <summary>
        /// Initializes a new instance of the <see cref="CueTextBox"/> class.
        /// </summary>
        public CueTextBox()
            : base()
        {
            this.UpdateCue();
        }



        private string cue = string.Empty;
        /// <summary>
        /// The text shown on the Cue Banner.
        /// </summary>
        /// <value>
        /// The cue.
        /// </value>
        [Category("Appearance")]
        [Description("The text shown on the Cue Banner.")]
        [Localizable(true)]
        [Bindable(true)]
        public virtual string Cue
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

        private bool retainCue = false;
        /// <summary>
        /// Determines if the cue banner is shown even when the textbox is focused.
        /// </summary>
        /// <value>
        /// The cue.
        /// </value>
        [DefaultValue(false)]
        [Category("Appearance")]
        [Description("Determines if the cue banner is shown even when the textbox is focused.")]
        [Localizable(true)]
        [Bindable(true)]
        public virtual bool RetainCue
        {
            get
            {
                return this.retainCue;
            }
            set
            {
                this.retainCue = value;
                this.UpdateCue();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this is a multiline <see cref="T:System.Windows.Forms.TextBox" /> control.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">Multiline cannot be enabled.</exception>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        public override bool Multiline
        {
            get
            {
                return base.Multiline;
            }
            set
            {
                if (value)
                {
                    throw new InvalidOperationException("Multiline cannot be enabled.");
                }
            }
        }

        /// <summary>
        /// Updates the cue.
        /// </summary>
        private void UpdateCue()
        {
            if (this.IsHandleCreated && PlatformHelper.XpOrHigher)
            {
                NativeMethods.SendMessage(this.Handle, EM_SETCUEBANNER, (this.retainCue && PlatformHelper.VistaOrHigher) ? new IntPtr(1) : IntPtr.Zero, this.cue);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:HandleCreated" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            this.UpdateCue();
        }
    }
}
