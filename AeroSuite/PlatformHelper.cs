using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroSuite
{
    public static class PlatformHelper
    {
        public static bool Win32NT
        {
            get
            {
                return Environment.OSVersion.Platform == PlatformID.Win32NT;
            }
        }

        public static bool XpOrHigher
        {
            get
            {
                return PlatformHelper.Win32NT && Environment.OSVersion.Version.Major >= 5;
            }
        }

        public static bool VistaOrHigher
        {
            get
            {
                return PlatformHelper.Win32NT && Environment.OSVersion.Version.Major >= 6;
            }
        }

        public static bool SevenOrHigher
        {
            get
            {
                return PlatformHelper.Win32NT && (Environment.OSVersion.Version >= new Version(6, 1));
            }
        }

        public static bool EightOrHigher
        {
            get
            {
                return PlatformHelper.Win32NT && (Environment.OSVersion.Version >= new Version(6, 2, 9200));
            }
        }
    }
}
