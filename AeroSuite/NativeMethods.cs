using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AeroSuite
{
    /// <summary>
    /// Provides some methods from the user32 and uxtheme libraries.
    /// </summary>
    internal static class NativeMethods
    {
        private const string user32 = "user32.dll";
        private const string uxtheme = "uxtheme.dll";

        [DllImport(user32)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        [DllImport(user32, CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, string lParam);

        [DllImport(uxtheme, CharSet = CharSet.Unicode)]
        public extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);
    }
}
