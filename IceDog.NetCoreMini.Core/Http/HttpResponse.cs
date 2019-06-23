using IceDog.NetCoreMini.Core.Extensions;
using IceDog.NetCoreMini.Core.Feature;
using System;
using System.Collections.Specialized;
using System.IO;

namespace IceDog.NetCoreMini.Core.Http
{
    /// <summary>
    /// http 响应
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IHttpResponseFeature _feature;
        /// <summary>
        /// 响应头
        /// </summary>
        public NameValueCollection Headers => _feature.Headers;
        /// <summary>
        /// 响应流
        /// </summary>
        public Stream Body => _feature.Body;
        /// <summary>
        /// 响应状态码
        /// </summary>
        public int StatusCode { get => _feature.StatusCode; set => _feature.StatusCode = value; }
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        /// <param name="features"></param>
        public HttpResponse(IFeatureCollection features) => _feature = features.Get<IHttpResponseFeature>();
    }
}
