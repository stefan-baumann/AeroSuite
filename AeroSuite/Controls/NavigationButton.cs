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
    /// A simple Back/Forward Button drawn by Windows via Visual Styles if available.
    /// </summary>
    /// <remarks>
    /// The button is drawn with Visual Styles (Navigation > BackButton/ForwardButton). If running on XP or another OS, the button is drawn manually (in a kinda-Metro-Style)
    /// </remarks>
    [DesignerCategory("Code")]
    [Designer(typeof(NavigationButtonDesigner))]
    [DisplayName("Navigation Button")]
    [Description("A simple Back/Forward Button drawn by Windows via Visual Styles if available.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(NavigationButton))]
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

        private NavigationButtonType type = NavigationButtonType.Back;
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
        public virtual NavigationButtonType Type
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

        protected PushButtonState state = PushButtonState.Normal;

        /// <summary>
        /// Paints the button with visual styles.
        /// </summary>
        /// <param name="g">The targeted graphics.</param>
        protected virtual void PaintWithVisualStyles(Graphics g)
        {
            //Draw button
            new VisualStyleRenderer("Navigation", (int)this.type, this.Enabled ? (int)this.state : (int)PushButtonState.Disabled).DrawBackground(g, this.DisplayRectangle);

            //Draw Focus Rectangle
            if (this.Focused && this.ShowFocusCues)
            {
                ControlPaint.DrawFocusRectangle(g, this.DisplayRectangle);
            }
        }


        protected Pen normalPen;
        protected Brush hoverBrush;
        protected Pen hoverArrowPen;
        protected Brush pressedBrush;
        protected Pen pressedArrowPen;
        protected Pen disabledPen;
        /// <summary>
        /// Paints the button manually.
        /// </summary>
        /// <param name="g">The targeted graphics.</param>
        protected virtual void PaintManually(Graphics g)
        {
            if (normalPen == null)
            {
                normalPen = new Pen(SystemColors.ControlDark, 2);
                hoverBrush = new SolidBrush(SystemColors.ControlDark);
                hoverArrowPen = new Pen(this.BackColor, 2);
                pressedBrush = new SolidBrush(SystemColors.ControlDarkDark);
                pressedArrowPen = new Pen(this.BackColor, 2);
                disabledPen = new Pen(SystemColors.ControlLight, 2);
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (GraphicsPath gp = new GraphicsPath())
            {
                var innerRect = new Rectangle(4, 4, this.Width - 8, this.Height - 8);
                if (this.Type == NavigationButtonType.Back)
                    gp.AddLines(new PointF[] { new PointF(innerRect.X + innerRect.Width * 0.5f, innerRect.Y + innerRect.Height * 0.25f), new PointF(innerRect.X + innerRect.Width * 0.25f, innerRect.Y + innerRect.Height * 0.5f), new PointF(innerRect.X + innerRect.Width * 0.5f, innerRect.Y + innerRect.Height * 0.75f) });
                else
                    gp.AddLines(new PointF[] { new PointF(innerRect.X + innerRect.Width * 0.5f, innerRect.Y + innerRect.Height * 0.25f), new PointF(innerRect.X + innerRect.Width * 0.75f, innerRect.Y + innerRect.Height * 0.5f), new PointF(innerRect.X + innerRect.Width * 0.5f, innerRect.Y + innerRect.Height * 0.75f) });
                gp.StartFigure();
                gp.AddLine(new PointF(innerRect.X + innerRect.Width * 0.25f, innerRect.Y + innerRect.Height * 0.5f), new PointF(innerRect.X + innerRect.Width * 0.75f, innerRect.Y + innerRect.Height * 0.5f));

                if (!this.Enabled)
                {
                    g.DrawEllipse(this.disabledPen, new Rectangle(5, 5, this.Width - 10, this.Height - 10));
                    g.DrawPath(this.disabledPen, gp);
                }
                else
                {
                    switch (this.state)
                    {
                        case PushButtonState.Normal:
                            g.DrawEllipse(this.normalPen, new Rectangle(5, 5, this.Width - 10, this.Height - 10));
                            g.DrawPath(this.normalPen, gp);
                            break;
                        case PushButtonState.Hot:
                            g.FillEllipse(this.hoverBrush, new Rectangle(4, 4, this.Width - 8, this.Height - 8));
                            g.DrawPath(this.hoverArrowPen, gp);
                            break;
                        case PushButtonState.Pressed:
                            g.FillEllipse(this.pressedBrush, new Rectangle(4, 4, this.Width - 8, this.Height - 8));
                            g.DrawPath(this.pressedArrowPen, gp);
                            break;
                        default:
                            g.DrawEllipse(this.disabledPen, new Rectangle(5, 5, this.Width - 10, this.Height - 10));
                            g.DrawPath(this.disabledPen, gp);
                            break;
                    }
                }
            }
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

        protected override void Dispose(bool disposing)
        {
            //Dispose brushes and pens if manual drawing was used
            if (this.normalPen != null)
            {
                this.normalPen.Dispose();
                this.hoverBrush.Dispose();
                this.hoverArrowPen.Dispose();
                this.pressedBrush.Dispose();
                this.pressedArrowPen.Dispose();
                this.disabledPen.Dispose();
            }

            base.Dispose(disposing);
        }




        /// <summary>
        /// Provides a ControlDesigner for the <see cref="NavigationButton"/> Control.
        /// </summary>
        internal class NavigationButtonDesigner
            : ControlDesigner
        {
            /// <summary>
            /// Returns selection rules for the <see cref="NavigationButton"/> Control.
            /// </summary>
            /// <value>
            /// The selection rules.
            /// </value>
            public override SelectionRules SelectionRules
            {
                get
                {
                    return SelectionRules.Moveable;
                }
            }
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
