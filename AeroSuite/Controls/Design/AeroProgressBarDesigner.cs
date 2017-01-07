using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AeroSuite.Controls.Design
{
    /// <summary>
    /// Provides a ControlDesigner for the <see cref="AeroProgressBar"/> Control.
    /// </summary>
    internal class AeroProgressBarDesigner
        : ControlDesignerBase<AeroProgressBar>
    {
        /// <summary>
        /// Initializes the control designer with the specified target control.
        /// </summary>
        /// <param name="target">The target control.</param>
        protected override void InitializeInternal(AeroProgressBar target)
        {
            this.ActionList = new AeroProgressBarActionList(target);
        }
    }

    /// <summary>
    /// Provides an ActionList for the <see cref="AeroProgressBar"/> Control.
    /// </summary>
    /// <seealso cref="AeroSuite.Controls.Design.DesignerActionListBase{AeroSuite.Controls.AeroProgressBar}" />
    internal class AeroProgressBarActionList
        : DesignerActionListBase<AeroProgressBar>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AeroProgressBarActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public AeroProgressBarActionList(IComponent component)
            : base(component)
        { }

        /// <summary>
        /// Returns the items of this designer action list.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        protected override IEnumerable<DesignerActionItem> Items
        {
            get
            {
                yield return this.CreateItem(c => c.Value);
                yield return this.CreateItem(c => c.State);
                yield return this.CreateItem(c => c.Style);
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public int Value
        {
            get => this.Control.Value;
            set
            {
                this.Control.Value = value;
                this.RefreshControl();
            }
        }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public ProgressBarState State
        {
            get => this.Control.State;
            set
            {
                this.Control.State = value;
                this.RefreshControl();
            }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        public ProgressBarStyle Style
        {
            get => this.Control.Style;
            set
            {
                this.Control.Style = value;
                this.RefreshControl();
            }
        }
    }
}
