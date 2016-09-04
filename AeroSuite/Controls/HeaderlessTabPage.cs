using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AeroSuite.Controls
{
    /// <summary>
    /// A tab page for the <see cref="HeaderlessTabControl"/>.
    /// </summary>
    /// <remarks>
    /// It is essentially just a Panel.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Headerless Tab Page")]
    [Description("A tab page for the HeaderlessTabControl.")]
    [ToolboxItem(false)]
    [ToolboxBitmap(typeof(HeaderlessTabPage))]
    public class HeaderlessTabPage
        : Panel
    {
        /// <summary>
        /// <para>This property had to be disabled due to it not being usable.</para>
        /// <para>Gets or sets the edges of the container to which a control is bound and determines how a control is resized with its parent.</para>
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override AnchorStyles Anchor
        {
            get
            {
                return base.Anchor;
            }
            set
            {
                base.Anchor = value;
            }
        }

        /// <summary>
        /// <para>This property had to be disabled due to it not being usable.</para>
        /// <para>Gets or sets which control borders are docked to its parent control and determines how a control is resized with its parent.</para>
        /// </summary>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override DockStyle Dock
        {
            get
            {
                return base.Dock;
            }
            set
            {
                base.Dock = value;
            }
        }

        /// <summary>
        /// <para>This property had to be disabled due to it not being usable.</para>
        /// <para>Gets or sets a value indicating whether the control can respond to user interaction.</para>
        /// </summary>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        /// <summary>
        /// <para>This property had to be disabled due to it not being usable.</para>
        /// <para>Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.</para>
        /// </summary>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        ///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
        ///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Point Location
        {
            get
            {
                return base.Location;
            }
            set
            {
                base.Location = value;
            }
        }

        /* Also make unaccessible:
         * MaximumSize
         * MinimumSize
         * PreferredSize
         * TabIndex
         * TabStop
         * Text
         * Visible
         */

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.ParentChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        /// <exception cref="System.InvalidOperationException">A HeaderlessTabPage cannot be put in anything other than a HeaderlessTabControl.</exception>
        protected override void OnParentChanged(EventArgs e)
        {
            if (!this.DesignMode && this.Parent != null && !(this.Parent is HeaderlessTabControl))
            {
                throw new InvalidOperationException("A HeaderlessTabPage cannot be put in anything other than a HeaderlessTabControl.");
            }

            base.OnParentChanged(e);
        }
    }
}
