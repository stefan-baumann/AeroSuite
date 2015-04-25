using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    public static partial class EasingMethods
    {
        /// <summary>
        ///     <para>The default easing method. It makes a transition feel very realistic.</para>
        ///		<para>For this easing method, there is no "In" our "Out" variation - there is just one single method that you can use on it's own.</para>
        ///		<para>This method is based off this css transition: cubic-bezier(.25,.1,.25,1). I translated the internals of the cubic-bezier method and simplified them so that they work for this single method only.</para>
        ///     <para>Function: f(p) = .3p + 2.4p^2 - 1.7p^3</para>
        ///     <para>Derivative: f'(p) = -5.1p^2 + 4.8p + .3</para>
        /// </summary>
        /// <param name="progress">The time progress of the animation.</param>
        /// <returns>The value progress of the animation.</returns>
        public static double DefaultEase(double progress)
        {
            return (progress <= 0) ? 0 : (progress >= 1) ? 1 : .3 * progress + 2.4 * Math.Pow(progress, 2) - 1.7 * Math.Pow(progress, 3);
        }
    }
}
