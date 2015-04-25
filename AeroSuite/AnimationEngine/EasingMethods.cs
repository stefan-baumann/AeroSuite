using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    /// <summary>
    ///     <para>Provides methods and algorithms for creating smooth transitions.</para>
    ///     <para>I'd like to citate http://easings.net/ here - they have a great explanation about what easing functions are and why they are important for making an animation feel realistic:</para>
    ///     <para>"Easing functions specify the rate of change of a parameter over time. Objects in real life don’t just start and stop instantly, and almost never move at a constant speed. When we open a drawer, we first move it quickly, and slow it down as it comes out. Drop something on the floor, and it will first accelerate downwards, and then bounce back up after hitting the floor."</para>
    ///     <para>You can also find a great introduction to this topic at https://gilmoreorless.github.io/sydjs-preso-easing/. </para>
    /// </summary>
    /// <remarks>
    ///     <para>These methods are based on "Robert Penner's Easing Functions".</para>
    ///     <para>They can be found at http://robertpenner.com/easing/ and they are distributed as open source under the BSD License.</para>
    /// </remarks>
    public static partial class EasingMethods
    {
        /// <summary>
        /// Chains the two specified easing methods.
        /// </summary>
        /// <param name="first">The first easing method.</param>
        /// <param name="second">The second easing method.</param>
        /// <returns>An easing method that uses the first specified easing method for the first half of the animation and the second one for the second half of the animation.</returns>
        public static EasingMethod Chain(this EasingMethod first, EasingMethod second)
        {
            return (double progress) => (progress < 0.5) ? .5 * first(progress * 2) : .5 + .5 * second((progress - .5) * 2);
        }

        /// <summary>
        /// Inverts the specified easing method.
        /// </summary>
        /// <param name="method">The easing method that should be inverted.</param>
        /// <returns>An inverted version of the specified easing method.</returns>
        public static EasingMethod Invert(this EasingMethod method)
        {
            return (double progress) => 1 - method(1 - progress);
            
        }

        /// <summary>
        /// Creates an upside down version of the specified easing method.
        /// </summary>
        /// <param name="method">The easing method.</param>
        /// <returns>An upside down version of the specified easing method</returns>
        public static EasingMethod UpsideDown(this EasingMethod method)
        {
            return (double progress) => 1 - method(progress);
        }

        /// <summary>
        /// Reverses the specified easing method.
        /// </summary>
        /// <param name="method">The easing method.</param>
        /// <returns>A reverse version of the specified easing method.</returns>
        public static EasingMethod Reverse(this EasingMethod method)
        {
            return (double progress) => method(1 - progress);
        }
    }
}
