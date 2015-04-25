using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    /// <summary>
    /// A class for providing transition values. You can use it as a base for animations of custom-drawn controls, game objects, ...
    /// </summary>
    /// <typeparam name="T">The type of value that should be transitioned.</typeparam>
    public class ValueProvider<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValueProvider{T}"/> class.
        /// </summary>
        /// <param name="startValue">The start value.</param>
        /// <param name="valueFactory">The value factory.</param>
        public ValueProvider(T startValue, ValueFactory<T> valueFactory)
            : this(startValue, valueFactory, EasingMethods.DefaultEase)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValueProvider{T}"/> class.
        /// </summary>
        /// <param name="startValue">The start value.</param>
        /// <param name="valueFactory">The value factory.</param>
        /// <param name="easingMethod">The easing method.</param>
        public ValueProvider(T startValue, ValueFactory<T> valueFactory, EasingMethod easingMethod)
        {
            this.StartValue = startValue;
            this.TargetValue = this.StartValue;
            this.StartTime = DateTime.Now;
            this.Duration = TimeSpan.Zero;

            this.EasingMethod = easingMethod;
            this.ValueFactory = valueFactory;
        }

        /// <summary>
        /// Gets or sets the easing method used for creating a smooth transition.
        /// </summary>
        /// <value>
        /// The easing method.
        /// </value>
        public virtual EasingMethod EasingMethod { get; set; }

        /// <summary>
        /// Gets or sets the value factory used for generating a value corresponding to the progress.
        /// </summary>
        /// <value>
        /// The value factory.
        /// </value>
        public virtual ValueFactory<T> ValueFactory { get; set; }

        /// <summary>
        /// Gets or sets the start value.
        /// </summary>
        /// <value>
        /// The start value.
        /// </value>
        public virtual T StartValue { get; set; }

        /// <summary>
        /// Gets or sets the target value.
        /// </summary>
        /// <value>
        /// The target value.
        /// </value>
        public virtual T TargetValue { get; set; }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public virtual DateTime StartTime { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public virtual TimeSpan Duration { get; set; }

        /// <summary>
        /// Returns a value corresponding to the current progress.
        /// </summary>
        /// <value>
        /// The current value.
        /// </value>
        public virtual T CurrentValue
        {
            get
            {
                double progress = this.CurrentProgress;
                if (progress >= 1) return this.TargetValue;
                return this.ValueFactory(this.StartValue, this.TargetValue, this.EasingMethod(progress));
            }
        }

        /// <summary>
        /// Returns the current progress.
        /// </summary>
        /// <value>
        /// The current progress.
        /// </value>
        public double CurrentProgress
        {
            get
            {
                var currentDuration = DateTime.Now - this.StartTime;
                if (currentDuration >= this.Duration)
                    return 1;
                return currentDuration.TotalMilliseconds/ this.Duration.TotalMilliseconds;
            }
        }

        /// <summary>
        /// Starts a transition from the current value to the specified target value.
        /// </summary>
        /// <param name="targetValue">The target value.</param>
        /// <param name="duration">The duration.</param>
        public virtual void StartTransition(T targetValue, TimeSpan duration)
        {
            this.StartTransition(this.CurrentValue, targetValue, duration);
        }

        /// <summary>
        /// Starts a transition from the specified start value to the specified target value.
        /// </summary>
        /// <param name="startValue">The start value.</param>
        /// <param name="targetValue">The target value.</param>
        /// <param name="duration">The duration.</param>
        public virtual void StartTransition(T startValue, T targetValue, TimeSpan duration)
        {
            this.StartValue = startValue;
            this.TargetValue = targetValue;
            this.StartTime = DateTime.Now;
            this.Duration = duration;
        }

        /// <summary>
        /// Cancels the current transition.
        /// </summary>
        public virtual void CancelTransition()
        {
            this.StartValue = this.CurrentValue;
            this.TargetValue = this.CurrentValue;
            this.StartTime = DateTime.Now;
            this.Duration = TimeSpan.Zero;
        }
    }
}
