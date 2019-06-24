using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;

namespace IceDog.NetCoreMini.Core.Http.Features
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpListenerFeature : IHttpRequestFeature, IHttpResponseFeature
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly HttpListenerContext _context;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public HttpListenerFeature(HttpListenerContext context) => _context = context;
        /// <summary>
        /// 
        /// </summary>
        Uri IHttpRequestFeature.Url => _context.Request.Url;
        /// <summary>
        /// 
        /// </summary>
        NameValueCollection IHttpRequestFeature.Headers => _context.Request.Headers;
        /// <summary>
        /// 
        /// </summary>
        NameValueCollection IHttpResponseFeature.Headers => _context.Response.Headers;
        /// <summary>
        /// 
        /// </summary>
        Stream IHttpRequestFeature.Body => _context.Request.InputStream;
        /// <summary>
        /// 
        /// </summary>
        Stream IHttpResponseFeature.Body => _context.Response.OutputStream;
        /// <summary>
        /// 
        /// </summary>
        int IHttpResponseFeature.StatusCode { get => _context.Response.StatusCode; set => _context.Response.StatusCode = value; }
    }
}
