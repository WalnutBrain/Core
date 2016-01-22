using System;
using System.Collections.Generic;
using System.Linq;

namespace WalnutBrain
{
    public static class EnumerableExtensions
    {
        
        public static IEnumerable<Func<T1, T3>> Currying<T1, T2, T3>(this IEnumerable<Func<T1, T2, T3>> sequence, T2 value)
        {
            return sequence.Select<Func<T1, T2, T3>, Func<T1, T3>>(func =>a => func(a, value));
        }

        public static IEnumerable<Func<T2, T3>> Currying<T1, T2, T3>(this IEnumerable<Func<T1, T2, T3>> sequence, T1 value)
        {
            return sequence.Select<Func<T1, T2, T3>, Func<T2, T3>>(func => a => func(value, a));
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var e in enumerable)
            {
                action(e);
            }
        }

    }
}