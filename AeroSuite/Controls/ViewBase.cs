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
    /// <para>A base class for creating Views that can be displayed in a <see cref="Display"/>.</para>
    /// <para>There are three ways for creating Views:</para>
    /// <para>1.: Create a file of type "Inherited User Control" (you can do it by pressind Crtl+Shift+A in Visual Studio and then selecting "Windows Forms > Inherited User Control"; then select <see cref="ViewBase"/> as the class to inherit from)</para>
    /// <para>2.: Create a new form or UserControl and edit the code so that it inherits from <see cref="ViewBase"/>.</para>
    /// <para>3.: Create a new class and make it inherit from <see cref="ViewBase"/>. However, Designer Code will be generated in the main code file.</para>
    /// </summary>
    /// <remarks>
    /// Combined with a <see cref="Display"/>, this is the perfect alternative to the <see cref="HeaderlessTabControl"/>.
    /// </remarks>
    //[DesignerCategory("Code")]
    [DisplayName("View Base")]
    [Description("A base Control for Views. Not to be put directly on a form.")]
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(TabPage))]
    public class ViewBase
        : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewBase"/> class.
        /// </summary>
        public ViewBase()
        {
            this.BackColor = SystemColors.Window;
            this.Font = SystemFonts.MessageBoxFont;
            this.Size = new Size(750, 500);
            this.Dock = DockStyle.Fill;

            this.SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private Display display;
        /// <summary>
        /// Gets or sets the display.
        /// </summary>
        /// <value>
        /// The display.
        /// </value>
        public virtual Display Display
        {
            get
            {
                return this.display;
            }
            set
            {
                if (value == this.display)
                {
                    return;
                }

                if (this.display != null)
                {
                    this.display.ClearViewInternal();
                }

                this.SetDisplayInternal(value, true);
            }
        }
        
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.ContainerControl.OnParentChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        /// <exception cref="System.InvalidOperationException">A View cannot be put in anything other than a Display.</exception>
        protected override void OnParentChanged(EventArgs e)
        {
            if (!this.DesignMode && this.Parent != null && !(this.Parent is Display))
            {
                throw new InvalidOperationException("A View cannot be put in anything other than a Display.");
            }

            base.OnParentChanged(e);
        }

        /// <summary>
        /// Clears the display internal.
        /// </summary>
        internal virtual void ClearDisplayInternal()
        {
            this.display = null;
        }

        /// <summary>
        /// Sets the display internally.
        /// </summary>
        /// <param name="display">The new display this view will be displayed in.</param>
        /// <param name="notifyDisplay">if set to <c>true</c> the display will be notified so that it can adapt to the change.</param>
        internal virtual void SetDisplayInternal(Display display, bool notifyDisplay)
        {
            this.display = display;

            if (this.display != null)
            {
                if (notifyDisplay)
                {
                    this.display.SetViewInternal(this, false);
                }
            }
        }
    }
}
