using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
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

        public static ValueFactory<Tuple<T1, T2>> CreateTupleFactory<T1, T2>(ValueFactory<T1> t1Factory, ValueFactory<T2> t2Factory)
        {
            return (startValue, targetValue, progress) =>
            {
                return Tuple.Create(t1Factory(startValue.Item1, targetValue.Item1, progress), t2Factory(startValue.Item2, targetValue.Item2, progress));
            };
        }

        public static ValueFactory<Tuple<T1, T2, T3>> CreateTupleFactory<T1, T2, T3>(ValueFactory<T1> t1Factory, ValueFactory<T2> t2Factory, ValueFactory<T3> t3Factory)
        {
            return (startValue, targetValue, progress) =>
            {
                return Tuple.Create(t1Factory(startValue.Item1, targetValue.Item1, progress), t2Factory(startValue.Item2, targetValue.Item2, progress), t3Factory(startValue.Item3, targetValue.Item3, progress));
            };
        }

        public static ValueFactory<Tuple<T1, T2, T3, T4>> CreateTupleFactory<T1, T2, T3, T4>(ValueFactory<T1> t1Factory, ValueFactory<T2> t2Factory, ValueFactory<T3> t3Factory, ValueFactory<T4> t4Factory)
        {
            return (startValue, targetValue, progress) =>
            {
                return Tuple.Create(t1Factory(startValue.Item1, targetValue.Item1, progress), t2Factory(startValue.Item2, targetValue.Item2, progress), t3Factory(startValue.Item3, targetValue.Item3, progress), t4Factory(startValue.Item4, targetValue.Item4, progress));
            };
        }

        public static ValueFactory<Tuple<T1, T2, T3, T4, T5>> CreateTupleFactory<T1, T2, T3, T4, T5>(ValueFactory<T1> t1Factory, ValueFactory<T2> t2Factory, ValueFactory<T3> t3Factory, ValueFactory<T4> t4Factory, ValueFactory<T5> t5Factory)
        {
            return (startValue, targetValue, progress) =>
            {
                return Tuple.Create(t1Factory(startValue.Item1, targetValue.Item1, progress), t2Factory(startValue.Item2, targetValue.Item2, progress), t3Factory(startValue.Item3, targetValue.Item3, progress), t4Factory(startValue.Item4, targetValue.Item4, progress), t5Factory(startValue.Item5, targetValue.Item5, progress));
            };
        }

        public static ValueFactory<Tuple<T1, T2, T3, T4, T5, T6>> CreateTupleFactory<T1, T2, T3, T4, T5, T6>(ValueFactory<T1> t1Factory, ValueFactory<T2> t2Factory, ValueFactory<T3> t3Factory, ValueFactory<T4> t4Factory, ValueFactory<T5> t5Factory, ValueFactory<T6> t6Factory)
        {
            return (startValue, targetValue, progress) =>
            {
                return Tuple.Create(t1Factory(startValue.Item1, targetValue.Item1, progress), t2Factory(startValue.Item2, targetValue.Item2, progress), t3Factory(startValue.Item3, targetValue.Item3, progress), t4Factory(startValue.Item4, targetValue.Item4, progress), t5Factory(startValue.Item5, targetValue.Item5, progress), t6Factory(startValue.Item6, targetValue.Item6, progress));
            };
        }

        public static ValueFactory<Tuple<T1, T2, T3, T4, T5, T6, T7>> CreateTupleFactory<T1, T2, T3, T4, T5, T6, T7>(ValueFactory<T1> t1Factory, ValueFactory<T2> t2Factory, ValueFactory<T3> t3Factory, ValueFactory<T4> t4Factory, ValueFactory<T5> t5Factory, ValueFactory<T6> t6Factory, ValueFactory<T7> t7Factory)
        {
            return (startValue, targetValue, progress) =>
            {
                return Tuple.Create(t1Factory(startValue.Item1, targetValue.Item1, progress), t2Factory(startValue.Item2, targetValue.Item2, progress), t3Factory(startValue.Item3, targetValue.Item3, progress), t4Factory(startValue.Item4, targetValue.Item4, progress), t5Factory(startValue.Item5, targetValue.Item5, progress), t6Factory(startValue.Item6, targetValue.Item6, progress), t7Factory(startValue.Item7, targetValue.Item7, progress));
            };
        }
    }
}
