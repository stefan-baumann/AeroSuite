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
    /// An aero-styled TreeView.
    /// </summary>
    /// <remarks>
    /// A TreeView with the "Explorer"-WindowTheme applied.
    /// If the operating system is Windows XP or older, nothing will be changed.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Aero TreeView")]
    [Description("An aero-styled TreeView.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(TreeView))]
    public class AeroTreeView
        : TreeView
    {
        private const int TVS_EX_DOUBLEBUFFER = 0x4;
        private const int TVS_EX_AUTOHSCROLL = 0x20;
        private const int TVS_EX_FADEINOUTEXPANDOS = 0x40;
        private const int TVS_NOHSCROLL = 0x8000;
        private const int TVM_SETEXTENDEDSTYLE = 0x112c;
        private const int TVM_GETEXTENDEDSTYLE = 0x112d;

        /// <summary>
        /// Initializes a new instance of the <see cref="AeroTreeView"/> class.
        /// </summary>
        public AeroTreeView()
            : base()
        {
            //Altough those settings should be compatible to every OS, there's no need to set it on Non-Aero Systems
            if (PlatformHelper.VistaOrHigher)
            {
                this.HotTracking = true;
                this.ShowLines = false;
            }
        }



        /// <summary>
        /// The value of the HotTracking property. The HotTracking property determines if nodes are highlighted as the mousepointer passes over them.
        /// </summary>
        /// <value>
        /// A value that determines if nodes are highlighted as the mousepointer passes over them.
        /// </value>
        /// <remarks>
        /// This property had to be overriden to prevent the designer creating code for it and so disabling the automatic style adaption.
        /// </remarks>
        [Category("Behaviour")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual new bool HotTracking
        {
            get { return base.HotTracking; }
            set { base.HotTracking = value; }
        }

        /// <summary>
        /// The ShowLines property determines if lines are drawn between nodes in the tree view.
        /// </summary>
        /// <value>
        /// A value that determines if lines are drawn between nodes in the tree view.
        /// </value>
        /// <remarks>
        /// This property had to be overriden to prevent the designer creating code for it and so disabling the automatic style adaption.
        /// </remarks>
        [Category("Behaviour")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual new bool ShowLines
        {
            get { return base.ShowLines; }
            set { base.ShowLines = value; }
        }



        /// <summary>
        /// Raises the <see cref="E:HandleCreated" /> event.
        /// </summary>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (PlatformHelper.VistaOrHigher)
            {
                NativeMethods.SetWindowTheme(this.Handle, "explorer", null);

                var lParam = NativeMethods.SendMessage(this.Handle, TVM_GETEXTENDEDSTYLE, IntPtr.Zero, IntPtr.Zero);
                lParam = new IntPtr(lParam.ToInt32() | (TVS_EX_AUTOHSCROLL | TVS_EX_FADEINOUTEXPANDOS | TVS_EX_DOUBLEBUFFER));
                NativeMethods.SendMessage(this.Handle, TVM_SETEXTENDEDSTYLE, IntPtr.Zero, lParam);
            }
        }

        /// <summary>
        /// Overrides <see cref="TreeView.CreateParams"/>
        /// </summary>
        /// <value>
        /// A System.Windows.Forms.CreateParams that contains the required creation parameters when the handle to the control is created.
        /// </value>
        protected override CreateParams CreateParams
        {
            get
            {
                //If we are on a machine on which we don't want to apply any styles just return the value given by the base TreeView.
                if (!PlatformHelper.VistaOrHigher)
                {
                    return base.CreateParams;
                }

                var parameters = base.CreateParams;
                parameters.Style = parameters.Style | TVS_NOHSCROLL;
                return parameters;
            }
        }
    }
}
