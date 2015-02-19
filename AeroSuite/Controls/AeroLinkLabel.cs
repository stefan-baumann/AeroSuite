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
    /// A LinkLabel whose colors fit the Windows style. It also has a fixed hand cursor.
    /// </summary>
    /// <remarks>
    /// The colors are extracted with Visual Styles (TextStyle > HyperLinkText). If running on XP or another OS, the colors are taken from the <see cref="SystemColors"/> class.
    /// The cursor is the IDC_HAND cursor from the IDC_STANDARD_CURSORS enum.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Aero LinkLabel")]
    [Description("A LinkLabel whose colors fit the Windows style. It also has a fixed hand cursor.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(LinkLabel))]
    public class AeroLinkLabel
        : LinkLabel
    {
        private const int WM_SETCURSOR = 0x20;
        private const int IDC_HAND = 32649;

        protected Color defaultColor;
        protected Color activeColor;
        protected Color pressedColor;
        protected Color visitedColor;
        protected Color disabledColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AeroLinkLabel"/> class.
        /// </summary>
        public AeroLinkLabel()
            : base()
        {
            if (PlatformHelper.VistaOrHigher)
            {
                this.LinkBehavior = LinkBehavior.HoverUnderline;

                //Extract colors from visual styles
                this.defaultColor = new VisualStyleRenderer("TextStyle", 6, 1).GetColor(ColorProperty.TextColor);
                this.activeColor = new VisualStyleRenderer("TextStyle", 6, 2).GetColor(ColorProperty.TextColor);
                this.pressedColor = new VisualStyleRenderer("TextStyle", 6, 3).GetColor(ColorProperty.TextColor);
                this.visitedColor = new VisualStyleRenderer("TextStyle", 6, 0).GetColor(ColorProperty.TextColor);
                this.disabledColor = new VisualStyleRenderer("TextStyle", 6, 4).GetColor(ColorProperty.TextColor);
            }
            else
            {
                //Extract colors from system colors
                this.defaultColor = SystemColors.HotTrack;
                this.activeColor = SystemColors.HighlightText;
                this.pressedColor = SystemColors.HighlightText;
                this.visitedColor = this.VisitedLinkColor; //Don't know how to get this property so I use the default one.
                this.disabledColor = SystemColors.GrayText;
            }

            //Apply the extracted colors
            this.LinkColor = this.defaultColor;
            this.ActiveLinkColor = this.activeColor;
            this.VisitedLinkColor = this.visitedColor;
            this.DisabledLinkColor = this.disabledColor;
        }



        /// <summary>
        /// Raises the <see cref="E:MouseDown" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.ActiveLinkColor = this.pressedColor;

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="E:MouseUp" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.ActiveLinkColor = this.activeColor;

            base.OnMouseUp(e);
        }

        /// <summary>
        /// Processes the specified Windows Message.
        /// </summary>
        /// <param name="msg">The Message to process.</param>
        protected override void WndProc(ref Message m)
        {
            //Set the cursor to the Hand Cursor specified by the current Windows Theme
            if (m.Msg == WM_SETCURSOR && PlatformHelper.XpOrHigher)
            {
                NativeMethods.SetCursor(NativeMethods.LoadCursor(IntPtr.Zero, IDC_HAND));
                m.Result = IntPtr.Zero;
                return;
            }

            //Pass the WndProc call to the base
            base.WndProc(ref m);
        }
    }
}
