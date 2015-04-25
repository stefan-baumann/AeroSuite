using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    public static partial class EasingMethods
    {
        /// <summary>
        ///     <para>An easing method that accelerates from 0 to a velocity of 2.</para>
        ///     <para>Function: f(p) = p ^ 2</para>
        ///     <para>Derivative: f'(p) = 2 * p</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double QuadraticEaseIn(double progress)
        {
            return (progress <= 0) ? 0 : (progress >= 1) ? 1 : progress * progress;
        }

        /// <summary>
        ///     <para>An easing method that decelerates from a velocity of 2 to 0.</para>
        ///     <para>Function: f(p) = -(p * (p - 2))</para>
        ///     <para>Derivative: f'(p) = 2 - 2 * p</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double QuadraticEaseOut(double progress)
        {
            return (progress <= 0) ? 0 : (progress >= 1) ? 1 : -(progress * (progress - 2));
        }

        private static readonly EasingMethod quadraticEaseInOut = EasingMethods.Chain(EasingMethods.QuadraticEaseIn, EasingMethods.QuadraticEaseOut);
        /// <summary>
        ///     <para>A combination of the <see cref="EasingMethods.QuadraticEaseIn"/> and <see cref="EasingMethods.QuadraticEaseOut"/> methods.</para>
        ///     <para>It accelerates from 0 to a velocity of 2 and then decelerates back to a velocity of 0.</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double QuadraticEaseInOut(double progress)
        {
            return quadraticEaseInOut(progress);
        }
    }
}
