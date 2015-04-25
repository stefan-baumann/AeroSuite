using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    public static partial class EasingMethods
    {
        private static double radianFactor = Math.PI / 2;

        /// <summary>
        ///     <para>An easing method that accelerates from 0 to a velocity of 1.</para>
        ///     <para>Function: f(p) = -cos(p) + 1</para>
        ///     <para>Derivative: f'(p) = sin(p)</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double SinusEaseIn(double progress)
        {
            return (progress <= 0) ? 0 : (progress >= 1) ? 1 : -Math.Cos(progress * radianFactor) + 1;
        }

        /// <summary>
        ///     <para>An easing method that decelerates from a velocity of 1 to 0.</para>
        ///     <para>Function: f(p) = sin(p)</para>
        ///     <para>Derivative: f'(p) = cos(p)</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double SinusEaseOut(double progress)
        {
            return (progress <= 0) ? 0 : (progress >= 1) ? 1 : Math.Sin(progress * radianFactor);
        }

        private static readonly EasingMethod sinusEaseInOut = EasingMethods.Chain(EasingMethods.SinusEaseIn, EasingMethods.SinusEaseOut);
        /// <summary>
        ///     <para>A combination of the <see cref="EasingMethods.SinusEaseIn"/> and <see cref="EasingMethods.SinusEaseOut"/> methods.</para>
        ///     <para>It accelerates from 0 to a velocity of 1 and then decelerates back to a velocity of 0.</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double SinusEaseInOut(double progress)
        {
            return sinusEaseInOut(progress);
        }
    }
}
