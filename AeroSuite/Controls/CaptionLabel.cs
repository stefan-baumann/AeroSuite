using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AeroSuite.Controls
{
    /// <summary>
    /// A Label with big blue text that is used as an caption, header or important instruction.
    /// </summary>
    /// <remarks>
    /// The colors are extracted with Visual Styles (TextStyle > MainInstruction). If running on XP or another OS, the colors are taken from the <see cref="SystemColors"/> class.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Caption Label")]
    [Description("A Label with big blue text that is used as an caption, header or important instruction.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Label))]
    public class CaptionLabel
        : Label
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CaptionLabel"/> class.
        /// </summary>
        public CaptionLabel()
            : base()
        {
            this.AutoSize = true;
            this.Font = new Font(SystemFonts.MessageBoxFont.FontFamily, 12); //Need to find a way to get this information from the system (via visual styles)
            if (PlatformHelper.VistaOrHigher && PlatformHelper.VisualStylesEnabled)
                this.ForeColor = new VisualStyleRenderer("TextStyle", 1, 0).GetColor(ColorProperty.TextColor);
            else
                this.ForeColor = SystemColors.Highlight;
        }

        /// <summary>
        /// The foreground color of the control.
        /// </summary>
        /// <value>
        /// The foreground color of the control.
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
        /// Retrieves the current font for this control. This will be the font used by default for painting and text in the control.
        /// </summary>
        /// <value>
        /// The current font for this control.
        /// </value>
        /// <remarks>
        /// This property had to be overriden to prevent the designer creating code for it and so disabling the automatic style adaption.
        /// </remarks>
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual new Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }
    }
}
