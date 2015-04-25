using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    public static partial class EasingMethods
    {
        /// <summary>
        ///     <para>An easing method that accelerates from 0 to a velocity of 5.</para>
        ///     <para>Function: f(p) = p ^ 5</para>
        ///     <para>Derivative: f'(p) = 5 * p ^ 4</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double QuinticEaseIn(double progress)
        {
            return (progress <= 0) ? 0 : (progress >= 1) ? 1 : Math.Pow(progress, 5);
        }

        /// <summary>
        ///     <para>An easing method that decelerates from a velocity of 5 to 0.</para>
        ///     <para>Function: f(p) = -(p - 1) ^ 5 + 1</para>
        ///     <para>Derivative: f'(p) = -5 * (p - 1) ^ 4</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double QuinticEaseOut(double progress)
        {
            return (progress <= 0) ? 0 : (progress >= 1) ? 1 : Math.Pow(progress - 1, 5) + 1;
        }

        private static readonly EasingMethod quinticEaseInOut = EasingMethods.Chain(EasingMethods.QuinticEaseIn, EasingMethods.QuinticEaseOut);
        /// <summary>
        ///     <para>A combination of the <see cref="EasingMethods.QuinticEaseIn"/> and <see cref="EasingMethods.QuinticEaseOut"/> methods.</para>
        ///     <para>It accelerates from 0 to a velocity of 5 and then decelerates back to a velocity of 0.</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double QuinticEaseInOut(double progress)
        {
            return quinticEaseInOut(progress);
        }
    }
}
