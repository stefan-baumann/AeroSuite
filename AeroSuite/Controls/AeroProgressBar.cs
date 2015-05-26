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
    /// A progress bar control with extended features.
    /// </summary>
    /// <remarks>
    /// This progress bar supports states (normal, paused and error) and goes backwards smoothly.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Aero ProgressBar")]
    [Description("A progress bar control with extended features.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ProgressBar))]
    public class AeroProgressBar
        : ProgressBar
    {
        private const int WM_USER = 0x400;
        private const int PBM_SETSTATE = WM_USER + 16;
        private const int PBM_GETSTATE = WM_USER + 17; //Not used atm
        private const int PBS_SMOOTHREVERSE = 0x10;

        private ProgressBarState state = ProgressBarState.Normal;
        /// <summary>
        /// Indicates the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [Category("Appearance")]
        [Description("The state of the progressbar. Typically indicated by the color of the bar (green, yellow, red)")]
        public virtual ProgressBarState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
                if (!PlatformHelper.VistaOrHigher && !PlatformHelper.VisualStylesEnabled)
                {
                    //This feature is not natively supported on other platforms.
                    //As the bar color on Windows XP (classic theme) is indicated by the ForeColor property, we can try setting that property instead so we are able to recreate that effect on some systems at least.
                    switch (value)
                    {
                        case ProgressBarState.Paused:
                            this.ForeColor = Color.Gold;
                            break;
                        case ProgressBarState.Error:
                            this.ForeColor = Color.Firebrick;
                            break;
                        default:
                            this.ForeColor = SystemColors.Highlight;
                            break;
                    }
                }
                else
                {
                    this.UpdateState();
                }
            }
        }

        /// <summary>
        /// Indicates the current position of the progress bar. 
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public virtual new int Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                if (this.State != ProgressBarState.Normal)
                {
                    throw new InvalidOperationException("It is not possible to modify the progress of a ProgressBar in error or paused state.");
                }

                base.Value = value;
            }
        }

        /// <summary>
        /// Indicates the manner in which progress should be indicated on the progress bar.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        public virtual new ProgressBarStyle Style
        {
            get
            {
                return base.Style;
            }
            set
            {
                base.Style = value;
                //This must be called to recreate the coloring effect.
                this.UpdateState();
            }
        }

        /// <summary>
        /// The foreground color of the progress bar. Represents the bar color on Windows XP with classic theming and Linux.
        /// </summary>
        /// <value>
        /// The foreground color of the progress bar.
        /// </value>
        /// <remarks>
        /// This property had to be overriden to prevent the designer creating code for it and so disabling the automatic style adaption.
        /// </remarks>
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual new Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// Updates the state.
        /// </summary>
        protected void UpdateState()
        {
            if (PlatformHelper.VistaOrHigher && PlatformHelper.VisualStylesEnabled)
            {
                NativeMethods.SendMessage(this.Handle, PBM_SETSTATE, new IntPtr((int)this.state), IntPtr.Zero);
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
                this.UpdateState();
            }
        }

        /// <summary>
        /// Overrides ProgressBar.CreateParams
        /// </summary>
        /// <value>
        /// The create parameters.
        /// </value>
        protected override CreateParams CreateParams
        {
            get
            {
                var param = base.CreateParams;
                if (PlatformHelper.VistaOrHigher) param.Style |= PBS_SMOOTHREVERSE;
                return param;
            }
        }
    }



    /// <summary>
    /// A progress state used for the <see cref="AeroProgressBar"/>.
    /// </summary>
    /// <remarks>
    /// Only supported on Windows Vista or higher.
    /// </remarks>
    public enum ProgressBarState
    {
        /// <summary>
        /// The normal state (usually a green bar)
        /// </summary>
        Normal = 1,
        /// <summary>
        /// The error state (usually a red bar; it is not possible to modify the progress bar's value when this state is applied)
        /// </summary>
        Error = 2,
        /// <summary>
        /// The paused state (usually a yellow bar; it is not possible to modify the progress bar's value when this state is applied)
        /// </summary>
        Paused = 3
    }
}
