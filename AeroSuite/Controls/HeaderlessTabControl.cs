using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace AeroSuite.Controls
{
    /// <summary>
    /// A TabControl that does not have headers at run time.
    /// At design time, it behaves & looks just like a "normal" TabControl.
    /// </summary>
    /// <remarks>
    /// The TCM_ADJUSTRECT message is suppressed at run time to remove the headers.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Headerless TabControl")]
    [Description("A TabControl that does not have headers at run time.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(TabControl))]
    public class HeaderlessTabControl
        : TabControl
    {
        private const int TCM_ADJUSTRECT = 0x1328;

        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderlessTabControl"/> class.
        /// </summary>
        public HeaderlessTabControl()
        { }

        /// <summary>
        /// Processes the specified Windows Message.
        /// </summary>
        /// <param name="m">The message.</param>
        protected override void WndProc(ref Message m)
        {
            if (!this.DesignMode && m.Msg == TCM_ADJUSTRECT)
            {
                //Do not execute the base class code for TCM_ADJUSTRECT
                m.Result = new IntPtr(1);
                return;
            }

            base.WndProc(ref m);
        }
    }
}
