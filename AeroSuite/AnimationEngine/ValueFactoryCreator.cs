using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AeroSuite.AnimationEngine
{
    public static class ValueFactoryCreator
    {
        public static ValueFactory<List<T>> CreateListFactory<T>(ValueFactory<T> originalFactory)
        {
            return (startValue, targetValue, progress) =>
            {
                if (startValue == null)
                    throw new ArgumentNullException("startValue", "The start value must not be null.");
                if (targetValue == null)
                    throw new ArgumentNullException("targetValue", "The target value must not be null.");
                if (startValue.Count != targetValue.Count)
                    throw new ArgumentOutOfRangeException("targetValue", "The target value item count must be equal to the start value item count.");

                return startValue.Zip(targetValue, (start, target) => originalFactory(start, target, progress)).ToList();
            };
        }

        public static ValueFactory<T[]> CreateArrayFactory<T>(ValueFactory<T> originalFactory)
        {
            return (startValue, targetValue, progress) =>
            {
                if (startValue == null)
                    throw new ArgumentNullException("startValue", "The start value must not be null.");
                if (targetValue == null)
                    throw new ArgumentNullException("targetValue", "The target value must not be null.");
                if (startValue.Length != targetValue.Length)
                    throw new ArgumentOutOfRangeException("targetValue", "The target value item count must be equal to the start value item count.");

                return startValue.Zip(targetValue, (start, target) => originalFactory(start, target, progress)).ToArray();
            };
        }
    }
}
