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
    /// A host control for a <see cref="ViewBase"/>
    /// </summary>
    /// <remarks>
    /// The perfect alternative to a <see cref="HeaderlessTabControl"/>: It works on every imaginable platform without any bugs but also unfolds the possibility to create advanced single-window applications with a lot of OOP-compliance as you can easily create a class per View.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Display")]
    [Description("A host control for Views.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(Panel))]
    public class Display
        : ContainerControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Display"/> class.
        /// </summary>
        public Display()
        {
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.DesignMode)
            {
                e.Graphics.DrawString(this.Name + "\n Specify a View in the View property to display it.", this.Font, SystemBrushes.ControlDarkDark, new Rectangle(0, 0, this.Width, this.Height), new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center, Trimming = StringTrimming.EllipsisCharacter });
            }

            base.OnPaint(e);
        }

        /// <summary>
        /// Occurs when the displayed View changed.
        /// </summary>
        public event EventHandler ViewChanged;

        private ViewBase view;
        /// <summary>
        /// Gets or sets the view displayed.
        /// </summary>
        /// <value>
        /// The view.
        /// </value>
        public virtual ViewBase View
        {
            get
            {
                return this.view;
            }
            set
            {
                //if (value == this.view)
                //{
                //    return;
                //}

                this.ClearViewInternal();

                if (this.view != null)
                {
                    this.view.ClearDisplayInternal();
                }

                this.SetViewInternal(value, true);
            }
        }

        /// <summary>
        /// Clears the view internally.
        /// </summary>
        internal virtual void ClearViewInternal()
        {
            if (this.view != null && this.Controls.Contains(this.view))
            {
                this.Controls.Remove(this.view);
            }

            this.view = null;
        }

        /// <summary>
        /// Sets the view internally.
        /// </summary>
        /// <param name="view">The new view for this display.</param>
        /// <param name="notifyView">if set to <c>true</c> the view will be notified so that it can adapt to the change.</param>
        internal virtual void SetViewInternal(ViewBase view, bool notifyView)
        {
            this.view = view;

            if (this.view != null)
            {
                if (notifyView)
                {
                    this.view.SetDisplayInternal(this, false);

                    if (this.ViewChanged != null)
                    {
                        this.ViewChanged(this, EventArgs.Empty);
                    }
                }

                this.Controls.Add(this.view);
            }
        }
    }
}
