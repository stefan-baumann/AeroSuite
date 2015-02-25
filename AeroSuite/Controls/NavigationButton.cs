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
    /// A simple Back/Forward Button drawn by Windows via Visual Styles if available.
    /// </summary>
    /// <remarks>
    /// The button is drawn with Visual Styles (Navigation > BackButton/ForwardButton). If running on XP or another OS, the button is drawn manually (in a kinda-Metro-Style)
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Aero LinkLabel")]
    [Description("A simple Back/Forward Button drawn by Windows via Visual Styles if available.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Button))]
    public class NavigationButton
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
                return new Size(30, 30);
            }
        }

        private NavigationButtonType type { get; set; } = NavigationButtonType.Back;
        /// <summary>
        /// Indicates the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        //[DefaultValue(NavigationButtonType.Back)]
        [RefreshProperties(RefreshProperties.All)]
        [Description("Indicates the type.")]
        [Category("Appearance")]
        public NavigationButtonType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationButton"/> class.
        /// </summary>
        public NavigationButton()
        {
            this.type = NavigationButtonType.Back;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        /// <summary>
        /// Raises the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs"/> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (Application.RenderWithVisualStyles && VisualStyleRenderer.IsElementDefined(VisualStyleElement.CreateElement("Navigation", 0, 0))/*PlatformHelper.VistaOrHigher && PlatformHelper.VisualStylesEnabled*/) //This seems to be the right check according to the MSDN: https://msdn.microsoft.com/en-us/library/vstudio/ms171735(v=vs.100).aspx
            {
                this.PaintWithVisualStyles(e.Graphics);
            }
            else
            {
                this.PaintManually(e.Graphics);
            }

            base.OnPaint(e);
        }

        protected PushButtonState state { get; set; } = PushButtonState.Normal;

        /// <summary>
        /// Paints the button with visual styles.
        /// </summary>
        /// <param name="g">The targeted graphics.</param>
        protected virtual void PaintWithVisualStyles(Graphics g)
        {
            //Draw button
            new VisualStyleRenderer("Navigation", (int)this.type, (int)this.state).DrawBackground(g, this.DisplayRectangle);

            //Draw Focus Rectangle
            if (this.Focused && this.ShowFocusCues)
            {
                ControlPaint.DrawFocusRectangle(g, this.DisplayRectangle);
            }
        }

        /// <summary>
        /// Paints the button manually.
        /// </summary>
        /// <param name="g">The targeted graphics.</param>
        protected virtual void PaintManually(Graphics g)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Raises the <see cref="E:MouseEnter" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            this.state = PushButtonState.Hot;
            this.Invalidate();
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Raises the <see cref="MouseLeave" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            this.state = PushButtonState.Normal;
            this.Invalidate();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Raises the <see cref="MouseDown" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.state = PushButtonState.Pressed;
            this.Invalidate();
            base.OnMouseDown(e);
        }

        /// <summary>
        /// Raises the <see cref="MouseUp" /> event.
        /// </summary>
        /// <param name="e">The <see cref="MouseEventArgs"/> instance containing the event data.</param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.state = (e.X >= 0 && e.X < this.Width && e.Y >= 0 && e.Y < this.Height) ? PushButtonState.Hot : PushButtonState.Normal;
            this.Invalidate();
            base.OnMouseUp(e);
        }

        /// <summary>
        /// Raises the <see cref="GotFocus" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnGotFocus(EventArgs e)
        {
            this.Invalidate();
            base.OnGotFocus(e);
        }

        /// <summary>
        /// Raises the <see cref="LostFocus" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnLostFocus(EventArgs e)
        {
            this.Invalidate();
            base.OnLostFocus(e);
        }

        /// <summary>
        /// Raises the <see cref="EnabledChanged" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnEnabledChanged(EventArgs e)
        {
            this.state = this.Enabled ? PushButtonState.Normal : PushButtonState.Disabled;
            this.Invalidate();
            base.OnEnabledChanged(e);
        }

        /// <summary>
        /// Raises the <see cref="E:KeyDown" /> event.
        /// </summary>
        /// <param name="e">The <see cref="System.Windows.Forms.KeyEventArgs" /> instance containing the event data.</param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                this.InvokeOnClick(this, EventArgs.Empty);
            }

            base.OnKeyDown(e);
        }
    }



    /// <summary>
    /// The Type of a <see cref="NavigationButton"/>.
    /// </summary>
    public enum NavigationButtonType
    {
        /// <summary>
        /// Represents a backward button.
        /// </summary>
        Back = 1,
        /// <summary>
        /// Represents a forward button.
        /// </summary>
        Forward = 2
    }
}
