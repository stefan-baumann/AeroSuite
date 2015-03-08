using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ProgressBarState State
        {
            get
            {
                return this.state;
            }
            set
            {
                if (value != ProgressBarState.Normal && !PlatformHelper.VistaOrHigher && !PlatformHelper.VisualStylesEnabled)
                {
                    //This feature is not supported on other platforms.
                    return;
                }

                this.state = value;
                this.UpdateState();
            }
        }

        /// <summary>
        /// Indicates the current position of the progress bar. 
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public new int Value
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
        public new ProgressBarStyle Style
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
        /// Updates the state.
        /// </summary>
        protected void UpdateState()
        {
            if (PlatformHelper.VistaOrHigher)
            {
                NativeMethods.SendMessage(this.Handle, PBM_SETSTATE, new IntPtr((int)this.state), IntPtr.Zero);
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
                if (PlatformHelper.VistaOrHigher) param.Style |= PBS_SMOOTHREVERSE; //PBS_VERTICAL for a vertical ProgressBar; Create Properties for both styles
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
