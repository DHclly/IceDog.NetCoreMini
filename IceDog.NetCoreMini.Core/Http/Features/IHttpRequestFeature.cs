using System;
using System.Collections.Specialized;
using System.IO;

namespace IceDog.NetCoreMini.Core.Http.Features
{
    /// <summary>
    /// http请求特性
    /// </summary>
    interface IHttpRequestFeature
    {
        /// <summary>
        /// 请求url
        /// </summary>
        Uri Url { get; }
        /// <summary>
        /// 请求头
        /// </summary>
        NameValueCollection Headers { get; }
        /// <summary>
        /// 请求体
        /// </summary>
        Stream Body { get; }
    }
}
