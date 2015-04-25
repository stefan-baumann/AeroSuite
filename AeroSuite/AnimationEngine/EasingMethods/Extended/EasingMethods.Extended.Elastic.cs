using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    public static partial class EasingMethods
    {
        public static partial class Extended
        {
            private const double elast = 2 * Math.PI / .3;

            /// <summary>
            ///     <para>An easing method that starts by swinging up and down a little bit and then goes up to 1.0.</para>
            ///     <para>Do not use this method if you do not have a proper method for handling progress values smaller than 0.0.</para>
            ///     <para>No function formula here as there is no real mathematical formula for calculating the values for this transition. Instead, you can have a look at the GitHub Repository if you are interested in how it works internally.</para>
            /// </summary>
            /// <param name="progress">The time progress of the animation.</param>
            /// <returns>The value progress of the animation.</returns>
            public static double ElasticEaseIn(double progress)
            {
                return (progress <= 0) ? 0 : (progress >= 1) ? 1 : -Math.Pow(2, 10 * (progress - 1)) * Math.Sin((progress - 1.075) * elast);
            }

            /// <summary>
            ///     <para>An easing method that starts by swinging up a bit above 1.0 and then swings for a little bit until it stays still.</para>
            ///     <para>Do not use this method if you do not have a proper method for handling progress values greater than 1.0.</para>
            ///     <para>No function formula here as there is no real mathematical formula for calculating the values for this transition. Instead, you can have a look at the GitHub Repository if you are interested in how it works internally.</para>
            /// </summary>
            /// <param name="progress">The time progress of the animation.</param>
            /// <returns>The value progress of the animation.</returns>
            public static double ElasticEaseOut(double progress)
            {
                return (progress <= 0) ? 0 : (progress >= 1) ? 1 : Math.Pow(2, -10 * progress) * Math.Sin((progress - .075) * elast) + 1;
            }

            private static readonly EasingMethod elasticEaseInOut = EasingMethods.Chain(EasingMethods.Extended.ElasticEaseIn, EasingMethods.Extended.ElasticEaseOut);
            /// <summary>
            ///     <para>A combination of the <see cref="EasingMethods.Extended.ElasticEaseIn"/> and <see cref="EasingMethods.Extended.ElasticEaseOut"/> methods.</para>
            ///     <para>Do not use this method if you do not have a proper method for handling progress values greater than 1.0 and lower than 0.0.</para>
            /// </summary>
            /// <param name="progress">The time progress of the animation.</param>
            /// <returns>The value progress of the animation.</returns>
            public static double ElasticEaseInOut(double progress)
            {
                return elasticEaseInOut(progress);
            }
        }
    }
}
