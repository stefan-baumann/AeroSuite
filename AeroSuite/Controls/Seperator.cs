using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.VisualStyles;

namespace AeroSuite.Controls
{
    /// <summary>
    /// A seperator line.
    /// </summary>
    /// <remarks>
    /// The line is drawn with Visual Styles (TaskDialog > FootnoteSeperator). If running on XP or another OS, the line is drawn manually.
    /// </remarks>
    [DesignerCategory("Code")]
    [Designer(typeof(SeperatorDesigner))]
    [DisplayName("Seperator")]
    [DefaultProperty("")]
    [Description("A seperator line drawn by Windows via Visual Styles if available.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Seperator))]
    public class Seperator
        : Control
    {
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
                return new Size(250, 2);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Seperator"/> class.
        /// </summary>
        public Seperator()
        {
            this.TabStop = false;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        /// <summary>
        /// Hidden because the property is not used
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), Bindable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                //Make it work in the test project & designer -> remove Exception
                //throw new NotSupportedException("This control does not support a Text");
            }
        }

        /// <summary>
        /// Raises the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Application.RenderWithVisualStyles && VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement("TaskDialog", 17, 0))/*PlatformHelper.VistaOrHigher && PlatformHelper.VisualStylesEnabled*/) //This seems to be the right check according to the MSDN: https://msdn.microsoft.com/en-us/library/vstudio/ms171735(v=vs.100).aspx
            {
                new VisualStyleRenderer("TaskDialog", 17, 0).DrawBackground(e.Graphics, this.DisplayRectangle);
            }
            else
            {
                e.Graphics.DrawLine(SystemPens.ControlDark, new Point(0, 0), new Point(this.Width, 0));
                e.Graphics.DrawLine(SystemPens.ControlLightLight, new Point(0, 1), new Point(this.Width, 1));
            }

            base.OnPaint(e);
        }



        /// <summary>
        /// Provides a ControlDesigner for the <see cref="Seperator"/> Control.
        /// </summary>
        internal class SeperatorDesigner
            : ControlDesigner
        {
            /// <summary>
            /// Returns selection rules for the <see cref="Seperator"/> Control.
            /// </summary>
            /// <value>
            /// The selection rules.
            /// </value>
            public override SelectionRules SelectionRules
            {
                get
                {
                    return SelectionRules.Moveable | SelectionRules.LeftSizeable | SelectionRules.RightSizeable;
                }
            }
        }
    }
}
