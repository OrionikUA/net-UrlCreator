using System;
using System.Collections.Generic;
using System.Linq;

namespace OrionikUA.UrlCreator
{
    internal static class Helpers
    {
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> original) where TValue : ICloneable
        {
            Dictionary<TKey, TValue> ret = new Dictionary<TKey, TValue>(original.Count, original.Comparer);
            foreach (KeyValuePair<TKey, TValue> entry in original)
            {
                ret.Add(entry.Key, (TValue)entry.Value.Clone());
            }
            return ret;
        }

        public static List<T> Clone<T>(this IList<T> list) where T : ICloneable
        {
            return list.Select(x => (T)x.Clone()).ToList();
        }
    }
}
