﻿using IceDog.NetCoreMini.Core.Extensions;
using IceDog.NetCoreMini.Core.Feature;
using System;
using System.Collections.Specialized;
using System.IO;

namespace IceDog.NetCoreMini.Core.Http
{
    /// <summary>
    /// http 请求
    /// </summary>
    public class HttpRequest
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IHttpRequestFeature _feature;
        /// <summary>
        /// 请求的url
        /// </summary>
        public Uri Url => _feature.Url;
        /// <summary>
        /// 请求头
        /// </summary>
        public NameValueCollection Headers => _feature.Headers;
        /// <summary>
        /// 请求体
        /// </summary>
        public Stream Body => _feature.Body;

        /// <summary>
        /// 自定义构造函数
        /// </summary>
        /// <param name="features"></param>
        public HttpRequest(IFeatureCollection features) => _feature = features.Get<IHttpRequestFeature>();
    }
}
