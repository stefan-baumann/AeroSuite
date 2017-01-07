using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace AeroSuite.Controls.Design
{
    /// <summary>
    /// A base class for easier creation of designer action lists.
    /// </summary>
    /// <typeparam name="TControl">The type of the targeted control.</typeparam>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public abstract class DesignerActionListBase<TControl>
        : DesignerActionList where TControl : Control, new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DesignerActionListBase{TControl}"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        /// <exception cref="System.InvalidOperationException">The specified control is not compatible with this designer.</exception>
        protected DesignerActionListBase(IComponent component)
            : base(component)
        {
            if (!typeof(TControl).IsAssignableFrom(component.GetType()))
            {
                throw new InvalidOperationException("The specified control is not compatible with this designer.");
            }
        }

        /// <summary>
        /// Gets the control this designer action list is targeted at.
        /// </summary>
        /// <value>
        /// The target control.
        /// </value>
        protected TControl Control => this.Component as TControl;

        /// <summary>
        /// Dynamically creates a <see cref="DesignerActionPropertyItem"/> with the name, display name, category and description of the property specified by the expression.
        /// </summary>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="property">The targeted property.</param>
        /// <returns></returns>
        protected DesignerActionPropertyItem CreateItem<TProp>(Expression<Func<TControl, TProp>> property)
        {
            MemberInfo member = ((MemberExpression)property.Body).Member;
            return new DesignerActionPropertyItem(
                memberName: member.Name,
                displayName: ((DisplayNameAttribute)member.GetCustomAttributes(typeof(DisplayNameAttribute), true).FirstOrDefault())?.DisplayName ?? member.Name,
                category: ((CategoryAttribute)member.GetCustomAttributes(typeof(CategoryAttribute), true).FirstOrDefault())?.Category ?? string.Empty,
                description: ((DescriptionAttribute)member.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault())?.Description ?? string.Empty);
        }



        /// <summary>
        /// Returns the items of this designer action list.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        protected abstract IEnumerable<DesignerActionItem> Items { get; }

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.
        /// </returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection collection = new DesignerActionItemCollection { new DesignerActionHeaderItem("Appearance") };
            foreach (DesignerActionItem item in this.Items)
            {
                collection.Add(item);
            }
            return collection;
        }

        /// <summary>
        /// Refreshes the targeted control.
        /// </summary>
        protected void RefreshControl()
        {
            (this.GetService(typeof(DesignerActionUIService)) as DesignerActionUIService).Refresh(this.Control);
        }
    }
}
