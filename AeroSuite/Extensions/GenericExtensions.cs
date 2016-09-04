using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite
{
    /// <summary>
    /// Provides some generic extension methods applicable to most objects.
    /// </summary>
    public static class GenericExtensions
    {
        #region Do

        /// <summary>
        /// Executes a specified action on an object and then returns the object.
        /// </summary>
        /// <typeparam name="T">The type of the specified object.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="action">The action.</param>
        /// <returns>The specified object.</returns>
        public static T Do<T>(this T target, Action<T> action)
        {
            action(target);
            return target;
        }

        /// <summary>
        /// Executes a specified action on an object if a specified condition is fulfilled and then returns the object.
        /// </summary>
        /// <typeparam name="T">The type of the specified object.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="action">The action.</param>
        /// <returns>The specified object.</returns>
        public static T DoIf<T>(this T target, Func<bool> condition, Action<T> action)
        {
            if (condition())
            {
                action(target);
            }
            return target;
        }

        /// <summary>
        /// Executes a specified action on an object if a specified condition is fulfilled and then returns the object.
        /// </summary>
        /// <typeparam name="T">The type of the specified object.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="action">The action.</param>
        /// <returns>The specified object.</returns>
        public static T DoIf<T>(this T target, Func<T, bool> condition, Action<T> action)
        {
            if (condition(target))
            {
                action(target);
            }
            return target;
        }

        #endregion

        #region Modify

        /// <summary>
        /// Modifies the specified object by passing it to the specified method and then returns the result.
        /// </summary>
        /// <typeparam name="TIn">The type of the specified object.</typeparam>
        /// <typeparam name="TOut">The type of the object returned by the specified method.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="func">The function.</param>
        /// <returns>The object returned by the specified method.</returns>
        public static TOut Modify<TIn, TOut>(this TIn target, Func<TIn, TOut> func)
        {
            return func(target);
        }

        /// <summary>
        /// Modifies the specified object by passing it to the specified method if a specified condition is fulfilled and then returns the result.
        /// </summary>
        /// <typeparam name="T">The type of the specified object.</typeparam>
        /// <typeparam name="TOut">The type of the object returned by the specified method.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="condition">The condition.</param>
        /// <param name="func">The function.</param>
        /// <returns>The object returned by the specified method or the original object depending on whether the condition was fulfilled.</returns>
        public static T ModifyIf<T>(this T target, Func<bool> condition, Func<T, T> func)
        {
            return condition() ? func(target) : target;
        }

        #endregion
    }
}
