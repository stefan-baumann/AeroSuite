using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    public static partial class EasingMethods
    {
        /// <summary>
        ///     <para>An easing method that accelerates from 0 to a velocity of 3.</para>
        ///     <para>Function: f(p) = p ^ 3</para>
        ///     <para>Derivative: f'(p) = 3 * p ^ 2</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double CubicEaseIn(double progress)
        {
            return (progress <= 0) ? 0 : (progress >= 1) ? 1 : Math.Pow(progress, 3);
        }

        /// <summary>
        ///     <para>An easing method that decelerates from 3 to a velocity of 0.</para>
        ///     <para>Function: f(p) = (p - 1) ^ 3 + 1</para>
        ///     <para>Derivative: f'(p) = 3 * (p - 1) ^ 2</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double CubicEaseOut(double progress)
        {
            return (progress <= 0) ? 0 : (progress >= 1) ? 1 : Math.Pow(progress - 1, 3) + 1;
        }

        private static EasingMethod cubicEaseInOut = EasingMethods.Chain(EasingMethods.CubicEaseIn, EasingMethods.CubicEaseOut);
        /// <summary>
        ///     <para>A combination of the <see cref="EasingMethods.CubicEaseIn"/> and <see cref="EasingMethods.CubicEaseOut"/> methods.</para>
        ///     <para>It accelerates from 0 to a velocity of 3 and then decelerates back to a velocity of 0.</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double CubicEaseInOut(double progress)
        {
            return cubicEaseInOut(progress);
        }
    }
}
