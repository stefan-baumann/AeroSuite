using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AeroSuite.Controls.Design
{
    /// <summary>
    /// A base class for control designers
    /// </summary>
    /// <typeparam name="TControl">The type of the targeted control.</typeparam>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public abstract class ControlDesignerBase<TControl>
        : ControlDesigner where TControl : Control, new()
    {
        /// <summary>
        /// Initializes the designer with the specified component.
        /// </summary>
        /// <param name="component">The <see cref="T:System.ComponentModel.IComponent" /> to associate the designer with. This component must always be an instance of, or derive from, <see cref="T:System.Windows.Forms.Control" />.</param>
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            this.InitializeInternal(component as TControl);
        }

        /// <summary>
        /// Initializes the control designer with the specified target control.
        /// </summary>
        /// <param name="target">The target control.</param>
        protected abstract void InitializeInternal(TControl target);

        /// <summary>
        /// Returns the <see cref="Control"/> this control designer instance is targeted at.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public TControl Target
            => this.Control as TControl;

        /// <summary>
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (this.ActionList != null)
                {
                    return new DesignerActionListCollection(new[] { this.ActionList });
                }
                else
                {
                    return new DesignerActionListCollection();
                }
            }
        }

        /// <summary>
        /// Gets or sets the primary action list of this control designer.
        /// </summary>
        /// <value>
        /// The action list.
        /// </value>
        public DesignerActionList ActionList { get; set; }

        private SelectionRules selectionRules = SelectionRules.AllSizeable | SelectionRules.Moveable;
        /// <summary>
        /// Gets or sets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        public SelectionRules MainSelectionRules
        {
            get => this.selectionRules;
            set => this.selectionRules = value;
        }

        /// <summary>
        /// Gets the selection rules that indicate the movement capabilities of a component.
        /// </summary>
        /// <value>
        /// The selection rules.
        /// </value>
        public override SelectionRules SelectionRules => this.selectionRules;
    }
}
