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
    /// A vertical progress bar control with extended features.
    /// </summary>
    /// <remarks>
    /// This progress bar is a vertically displayed version of the <see cref="AeroProgressBar"/>.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Vertical Aero ProgressBar")]
    [Description("A vertical progress bar control with extended features.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(VerticalAeroProgressBar))]
    public class VerticalAeroProgressBar
        : AeroProgressBar
    {
        private const int PBS_VERTICAL = 0x4;

        /// <summary>
        /// Initializes a new instance of the <see cref="VerticalAeroProgressBar"/> class.
        /// </summary>
        public VerticalAeroProgressBar()
            : base()
        {
            this.Size = new Size(base.Height, base.Width);
        }

        /// <summary>
        /// Overrides AeroProgressBar.CreateParams
        /// </summary>
        /// <value>
        /// The create parameters.
        /// </value>
        protected override CreateParams CreateParams
        {
            get
            {
                var param = base.CreateParams;
                param.Style |= PBS_VERTICAL;
                return param;
            }
        }
    }
}
