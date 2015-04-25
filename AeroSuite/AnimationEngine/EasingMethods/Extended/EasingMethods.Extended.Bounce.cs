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
            //Some Constants for the bounce easing methods ^^
            private const double b = 7.5625, bF = 2.75;
            private const double bF1 = 1 / bF;
            private const double b2 = .75, bF2 = 2 / bF, bP2 = 1.5 / bF;
            private const double b3 = .9375, bF3 = 2.5 / bF, bP3 = 2.25 / bF;
            private const double b4 = .984375, bP4 = 2.625 / bF;

            /// <summary>
            ///     <para>An easing method that starts by bouncing up a little bit until it goes up to 1.0 with a big bounce.</para>
            ///     <para>No function formula here as there is no real mathematical formula for calculating the values for this transition. Instead, you can have a look at the GitHub Repository if you are interested in how it works internally.</para>
            /// </summary>
            /// <param name="progress">The time progress of the animation.</param>
            /// <returns>The value progress of the animation.</returns>
            public static double BounceEaseIn(double progress)
            {
                return (progress <= 0) ? 0 : (progress >= 1) ? 1 : 1 - EasingMethods.Extended.BounceEaseOut(1 - progress);
            }

            /// <summary>
            ///     <para>An easing method that first accelerates and then bounces a few times when reaching 1.0.</para>
            ///     <para>You can use this method in combination with the <see cref="EasingMethods.UpsideDown"/> method for a nice-looking fall and bounce animation like a rubber ball.</para>
            ///     <para>No function formula here as there is no real mathematical formula for calculating the values for this transition. Instead, you can have a look at the GitHub Repository if you are interested in how it works internally.</para>
            /// </summary>
            /// <param name="progress">The time progress of the animation.</param>
            /// <returns>The value progress of the animation.</returns>
            public static double BounceEaseOut(double progress)
            {
                return (progress <= 0) ? 0 : (progress >= 1) ? 1 : progress < bF1 ? b * Math.Pow(progress, 2) : progress < bF2 ? b * Math.Pow(progress - bP2, 2) + b2 : progress < bF3 ? b * Math.Pow(progress - bP3, 2) + b3 : b * Math.Pow(progress - bP4, 2) + b4;
            }
        }
    }
}
