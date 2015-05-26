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

        protected Color DefaultColor;
        protected Color ActiveColor;
        protected Color PressedColor;
        protected Color VisitedColor;
        protected Color DisabledColor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AeroLinkLabel"/> class.
        /// </summary>
        public AeroLinkLabel()
            : base()
        {
            this.AutoSize = true;

            if (PlatformHelper.VistaOrHigher && PlatformHelper.VisualStylesEnabled)
            {
                this.LinkBehavior = LinkBehavior.HoverUnderline;

                //Extract colors from visual styles
                this.DefaultColor = new VisualStyleRenderer("TextStyle", 6, 1).GetColor(ColorProperty.TextColor);
                this.ActiveColor = new VisualStyleRenderer("TextStyle", 6, 2).GetColor(ColorProperty.TextColor);
                this.PressedColor = new VisualStyleRenderer("TextStyle", 6, 3).GetColor(ColorProperty.TextColor);
                this.VisitedColor = new VisualStyleRenderer("TextStyle", 6, 0).GetColor(ColorProperty.TextColor);
                this.DisabledColor = new VisualStyleRenderer("TextStyle", 6, 4).GetColor(ColorProperty.TextColor);
            }
            else
            {
                //Extract colors from system colors
                this.DefaultColor = SystemColors.HotTrack;
                this.ActiveColor = SystemColors.Highlight;
                this.PressedColor = SystemColors.Highlight;
                this.VisitedColor = this.VisitedLinkColor; //Don't know how to get this property so I use the default one.
                this.DisabledColor = SystemColors.GrayText;
            }

            //Apply the extracted colors
            this.LinkColor = this.DefaultColor;
            this.ActiveLinkColor = this.ActiveColor;
            this.VisitedLinkColor = this.VisitedColor;
            this.DisabledLinkColor = this.DisabledColor;
        }



        /// <summary>
        /// Gets or sets the color used to display links in normal cases.
        /// </summary>
        /// <value>
        /// The color used to display links in normal cases.
        /// </value>
        /// <remarks>
        /// This property had to be overriden to prevent the designer creating code for it and so disabling the automatic style adaption.
        /// </remarks>
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual new Color LinkColor
        {
            get { return base.LinkColor; }
            set { base.LinkColor = value; }
        }

        /// <summary>
        /// Gets or sets the color used to display active links.
        /// </summary>
        /// <value>
        /// The color used to display active links.
        /// </value>
        /// <remarks>
        /// This property had to be overriden to prevent the designer creating code for it and so disabling the automatic style adaption.
        /// </remarks>
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual new Color ActiveLinkColor
        {
            get { return base.ActiveLinkColor; }
            set { base.ActiveLinkColor = value; }
        }

        /// <summary>
        /// Gets or sets the color used to display disabled links.
        /// </summary>
        /// <value>
        /// The color used to display disabled links.
        /// </value>
        /// <remarks>
        /// This property had to be overriden to prevent the designer creating code for it and so disabling the automatic style adaption.
        /// </remarks>
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual new Color DisabledLinkColor
        {
            get { return base.DisabledLinkColor; }
            set { base.DisabledLinkColor = value; }
        }

        /// <summary>
        /// Gets or sets the color used to display the link once it has been visited.
        /// </summary>
        /// <value>
        /// The color used to display the link once it has been visited..
        /// </value>
        /// <remarks>
        /// This property had to be overriden to prevent the designer creating code for it and so disabling the automatic style adaption.
        /// </remarks>
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual new Color VisitedLinkColor
        {
            get { return base.VisitedLinkColor; }
            set { base.VisitedLinkColor = value; }
        }

        /// <summary>
        /// Gets or sets a value that represents how the link will be underlined.
        /// </summary>
        /// <value>
        /// A value that represents how the link will be underlined.
        /// </value>
        /// <remarks>
        /// This property had to be overriden to prevent the designer creating code for it and so disabling the automatic style adaption.
        /// </remarks>
        [Category("Appearance")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual new LinkBehavior LinkBehavior
        {
            get { return base.LinkBehavior; }
            set { base.LinkBehavior = value; }
        }



        /// <summary>
        /// Raises the <see cref="E:MouseDown" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.ActiveLinkColor = this.PressedColor;

            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="E:MouseUp" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.ActiveLinkColor = this.ActiveColor;

            base.OnMouseUp(e);
        }

        /// <summary>
        /// Processes the specified Windows Message.
        /// </summary>
        /// <param name="m">The Message to process.</param>
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
