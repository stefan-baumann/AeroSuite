using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    /// <summary>
    /// A method that provides corresponding values of a specified type for a progress value.
    /// </summary>
    /// <typeparam name="T">The type.</typeparam>
    /// <param name="startValue">The start value.</param>
    /// <param name="targetValue">The target value.</param>
    /// <param name="progress">The progress.</param>
    /// <returns>A value corresponding to the progress.</returns>
    public delegate T ValueFactory<T>(T startValue, T targetValue, double progress);
}
