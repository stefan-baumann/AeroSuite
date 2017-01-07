using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Design;

namespace AeroSuite.Controls.Design
{
    /// <summary>
    /// Provides a ControlDesigner for the <see cref="NavigationButton"/> Control.
    /// </summary>
    internal class NavigationButtonDesigner
        : ControlDesignerBase<NavigationButton>
    {
        /// <summary>
        /// Initializes the control designer with the specified target control.
        /// </summary>
        /// <param name="target">The target control.</param>
        protected override void InitializeInternal(NavigationButton target)
        {
            this.ActionList = new NavigationButtonActionList(target);
            this.MainSelectionRules = SelectionRules.Moveable;
        }
    }


    /// <summary>
    /// Provides an ActionList for the <see cref="NavigationButton"/> Control.
    /// </summary>
    /// <seealso cref="AeroSuite.Controls.Design.DesignerActionListBase{AeroSuite.Controls.NavigationButton}" />
    internal class NavigationButtonActionList
            : DesignerActionListBase<NavigationButton>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationButtonActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public NavigationButtonActionList(IComponent component)
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
                yield return this.CreateItem(n => n.Type);
            }
        }
        
        public NavigationButtonType Type
        {
            get => this.Control.Type;
            set => this.Control.Type = value;
        }
    }
}
