using IceDog.NetCoreMini.Core.Feature;
using System;
using System.Collections.Generic;
using System.Text;

namespace IceDog.NetCoreMini.Core.Extensions
{
    public static partial class Extension
    {
        public static T Get<T>(this IFeatureCollection features) => features.TryGetValue(typeof(T), out var value) ? (T)value : default(T);

        public static IFeatureCollection Set<T>(this IFeatureCollection features, T feature)
        {
            features[typeof(T)] = feature;
            return features;
        }
    }
}
