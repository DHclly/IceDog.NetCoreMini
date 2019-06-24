using System;
using System.Collections.Generic;

namespace IceDog.NetCoreMini.Core.Http.Features
{
    /// <summary>
    /// 特性集合
    /// </summary>
    public class FeatureCollection : Dictionary<Type, object>, IFeatureCollection
    {
        /// <summary>
        /// 获取特性
        /// </summary>
        /// <typeparam name="TFeature">特性类型</typeparam>
        /// <returns></returns>
        public TFeature Get<TFeature>()
        {
            return this.TryGetValue(typeof(TFeature), out var value) ? (TFeature)value : default(TFeature);
        }
        /// <summary>
        /// 设置特性
        /// </summary>
        /// <typeparam name="TFeature">特性类型</typeparam>
        /// <param name="instance"></param>
        public void Set<TFeature>(TFeature instance)
        {
            this[typeof(TFeature)] = instance;
        }
    }
}
