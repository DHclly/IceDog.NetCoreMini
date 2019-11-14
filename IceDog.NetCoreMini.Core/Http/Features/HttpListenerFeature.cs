using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace IceDog.NetCoreMini.Core.Http.Features
{
    /// <summary>
    /// http监听器特性
    /// </summary>
    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        /// <summary>
        /// http监听器上下文
        /// </summary>
        private readonly HttpListenerContext _context;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public HttpListenerFeature(HttpListenerContext context) => _context = context;

        #region IHttpRequestFeature
        /// <summary>
        /// 请求url
        /// </summary>
        Uri IHttpRequestFeature.Url => _context.Request.Url;
        /// <summary>
        /// 请求头
        /// </s请求头ummary>
        NameValueCollection IHttpRequestFeature.Headers => _context.Request.Headers;
        /// <summary>
        /// 请求体
        /// </summary>
        Stream IHttpRequestFeature.Body => _context.Request.InputStream;
        #endregion

        #region IHttpResponseFeature

        /// <summary>
        /// 响应头
        /// </summary>
        NameValueCollection IHttpResponseFeature.Headers => _context.Response.Headers;
        /// <summary>
        /// 响应体
        /// </summary>
        Stream IHttpResponseFeature.Body => _context.Response.OutputStream;
        /// <summary>
        /// 响应状态码
        /// </summary>
        int IHttpResponseFeature.StatusCode { get => _context.Response.StatusCode; set => _context.Response.StatusCode = value; }
        #endregion
    }
}
