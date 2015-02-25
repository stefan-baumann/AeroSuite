using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace AeroSuite
{
    /// <summary>
    /// Provides helper functions for platform management
    /// </summary>
    public static class PlatformHelper
    {
        /// <summary>
        /// Returns a indicating whether the Operating System is Windows 32 NT based.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows 32 NT based; otherwise, <c>false</c>.
        /// </value>
        public static bool Win32NT
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Win32NT;
            }
        }

        /// <summary>
        /// Returns a value indicating whether the Operating System is Windows XP or higher.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows 8 or higher; otherwise, <c>false</c>.
        /// </value>
        public static bool XpOrHigher
        {
            get
            {
                return PlatformHelper.Win32NT && Environment.OSVersion.Version.Major >= 5;
            }
        }

        /// <summary>
        /// Returns a value indicating whether the Operating System is Windows Vista or higher.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows Vista or higher; otherwise, <c>false</c>.
        /// </value>
        public static bool VistaOrHigher
        {
            get
            {
                return PlatformHelper.Win32NT && Environment.OSVersion.Version.Major >= 6;
            }
        }

        /// <summary>
        /// Returns a value indicating whether the Operating System is Windows 7 or higher.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows 7 or higher; otherwise, <c>false</c>.
        /// </value>
        public static bool SevenOrHigher
        {
            get
            {
                return PlatformHelper.Win32NT && (Environment.OSVersion.Version >= new Version(6, 1));
            }
        }

        /// <summary>
        /// Returns a value indicating whether the Operating System is Windows 8 or higher.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the Operating System is Windows 8 or higher; otherwise, <c>false</c>.
        /// </value>
        public static bool EightOrHigher
        {
            get
            {
                return PlatformHelper.Win32NT && (Environment.OSVersion.Version >= new Version(6, 2, 9200));
            }
        }

        /// <summary>
        /// Returns a value indicating whether Visual Styles are supported by the Operating System.
        /// </summary>
        /// <value>
        /// <c>true</c> if Visual Styles are supported by the Operating System; otherwise, <c>false</c>.
        /// </value>
        public static bool VisualStylesSupported
        {
            get
            {
                return VisualStyleInformation.IsSupportedByOS;
            }
        }

        /// <summary>
        /// Returns a value indicating whether Visual Styles are enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if Visual Styles are enabled; otherwise, <c>false</c>.
        /// </value>
        public static bool VisualStylesEnabled
        {
            get
            {
                return VisualStyleInformation.IsEnabledByUser;
            }
        }

        public static bool UseVisualStyles
        {
            get
            {
                return VisualStyleRenderer.IsSupported;
            }
        }
    }
}
