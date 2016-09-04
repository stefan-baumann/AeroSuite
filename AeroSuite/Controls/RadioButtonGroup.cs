using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;

namespace AeroSuite.Controls
{
    /// <summary>
    /// A list of radio buttons based on a collection of objects for easy creation of radio button groups.
    /// </summary>
    /// <remarks>
    /// Basically a FlowLayoutPanel that flows "TopDown" with a radio button for every entry in the input list.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("RadioButton Group")]
    [Description("A list of radio buttons based on a collection of objects for easy creation of radio button groups.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(CheckedListBox))]
    public class RadioButtonGroup
        : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadioButtonGroup"/> class.
        /// </summary>
        public RadioButtonGroup()
            : base()
        {
            this.Size = new Size(200, 350);

            this.RadioButtonContainer = new FlowLayoutPanel() { AutoScroll = true, Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, WrapContents = false };
            for (int i = 0; i < 10; i++)
            {
                RadioButton radioButton = new RadioButton() { AutoEllipsis = true, AutoSize = false, Text = "Option " + i, Margin = new Padding(0) };
                radioButton.Width = this.RadioButtonContainer.ClientRectangle.Width;
                this.RadioButtonContainer.Controls.Add(radioButton);
            }
            this.Controls.Add(this.RadioButtonContainer);
        }

        /// <summary>
        /// Raises the <see cref="E:Resize" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (this.RadioButtonContainer != null)
            {
                foreach (RadioButton radioButton in this.RadioButtonContainer.Controls)
                {
                    radioButton.Width = this.RadioButtonContainer.ClientRectangle.Width;
                }
            }
        }

        private FlatStyle flatStyle = FlatStyle.Standard;
        /// <summary>
        /// Gets or sets the flat style.
        /// </summary>
        /// <value>
        /// The flat style.
        /// </value>
        public FlatStyle FlatStyle
        {
            get
            {
                return this.flatStyle;
            }
            set
            {
                this.flatStyle = value;

                if (this.RadioButtonContainer != null)
                {
                    foreach (RadioButton radioButton in this.RadioButtonContainer.Controls)
                    {
                        radioButton.FlatStyle = this.flatStyle;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the RadioButton container.
        /// </summary>
        /// <value>
        /// The RadioButton container.
        /// </value>
        protected FlowLayoutPanel RadioButtonContainer { get; private set; }
    }
}