using System.Collections.Specialized;
using System.IO;

namespace IceDog.NetCoreMini.Core.Http.Features
{
    /// <summary>
    /// 响应特性
    /// </summary>
    interface IHttpResponseFeature
    {
        /// <summary>
        /// 响应头
        /// </summary>
        NameValueCollection Headers { get; }
        /// <summary>
        /// 响应体
        /// </summary>
        Stream Body { get; }
        /// <summary>
        /// 响应状态码
        /// </summary>
        int StatusCode { get; set; }
    }
}
