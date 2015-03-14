using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AeroSuite.Test.TestAdapters
{
    public abstract class TestAdapter
    {
        protected TestAdapter(Control control) { }
    }

    /// <summary>
    /// Provides methods for better and more productive testing of a control.
    /// </summary>
    public abstract class TestAdapterBase<T>
        : TestAdapter
        where T : Control
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestAdapterBase{T}"/> class.
        /// You can set values for easier testing here.
        /// </summary>
        /// <param name="control">The control.</param>
        protected TestAdapterBase(T control)
            : base(control)
        {
            this.Control = control;
        }

        /// <summary>
        /// Gets or sets the targeted control.
        /// </summary>
        /// <value>
        /// The control.
        /// </value>
        public T Control { get; set; }
    }
}