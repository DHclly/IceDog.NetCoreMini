using System;
using System.Collections.Generic;

namespace IceDog.NetCoreMini.Core.Http.Features
{
    /// <summary>
    /// 特性集合
    /// </summary>
    public interface IFeatureCollection : IDictionary<Type, object>
    {
        /// <summary>
        /// 获取特性
        /// </summary>
        /// <typeparam name="TFeature">特性类型</typeparam>
        /// <returns></returns>
        TFeature Get<TFeature>();
        /// <summary>
        /// 设置特性
        /// </summary>
        /// <typeparam name="TFeature">特性类型</typeparam>
        /// <param name="instance"></param>
        void Set<TFeature>(TFeature instance);
    }
}
