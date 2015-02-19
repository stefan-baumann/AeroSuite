using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AeroSuite
{
    /// <summary>
    /// Provides the ability to set properties when being tested.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITestControl
    {
        /// <summary>
        /// Prepares the control for tests (setting properties etc.).
        /// </summary>
        void PrepareForTests();
    }
}
