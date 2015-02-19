using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AeroSuite
{
    /// <summary>
    /// A class that lets a test app specify some prototyping data. This data must be specified before using any ITestControl.
    /// </summary>
    public static class TestDataProvider
    {
        public static ImageList SmallImageList { get; set; }
        public static ImageList LargeImageList { get; set; }
    }
}
