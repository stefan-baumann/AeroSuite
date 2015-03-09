using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using AeroSuite.IconExtractor;

namespace AeroSuite.Controls
{
    /// <summary>
    /// A normal button with a Shield.
    /// </summary>
    /// <remarks>
    /// The shield is extracted from the system with LoadImage if possible. Otherwise the shield will be enabled by sending the BCM_SETSHIELD Message to the control.
    /// If the operating system is not Windows Vista or higher, no shield will be displayed as there's no such thing as UAC on the target system -> the shield is obsolete.
    /// </remarks>
    [DesignerCategory("Code")]
    [DisplayName("Shield Button")]
    [Description("A normal button with a Shield.")]
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(ShieldButton))]
    public class ShieldButton
        : Button
    {
        private const int BCM_SETSHIELD = 0x160C;
        private static bool? isSystemAbleToLoadShield = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShieldButton"/> class.
        /// </summary>
        public ShieldButton()
            : base()
        {
            this.Size = new Size((int)(this.Width * 1.5), this.Height + 1);
            if (PlatformHelper.VistaOrHigher)
            {
                //Only try to load the icon if it did not fail before
                if (!isSystemAbleToLoadShield.HasValue || isSystemAbleToLoadShield.Value) 
                {
                    try
                    {
                        var icon = IconExtractor.LoadIcon(IconType.Shield, SystemInformation.SmallIconSize);
                        if (icon != null)
                        {
                            this.Image = icon.ToBitmap();
                            this.TextImageRelation = TextImageRelation.ImageBeforeText;
                            this.ImageAlign = ContentAlignment.MiddleRight;

                            isSystemAbleToLoadShield = true;
                            return;
                        }
                    }
                    catch (PlatformNotSupportedException)
                    {
                        //This happens when the system does not support this call
                        //Prevent future calling
                        isSystemAbleToLoadShield = false;
                    }
                }
                
                //Preferred way not possible
                this.FlatStyle = FlatStyle.System;
                NativeMethods.SendMessage(this.Handle, BCM_SETSHIELD, IntPtr.Zero, new IntPtr(1));
            }
        }
    }
}
