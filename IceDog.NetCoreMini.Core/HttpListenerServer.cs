using IceDog.NetCoreMini.Core.Extensions;
using IceDog.NetCoreMini.Core.Feature;
using IceDog.NetCoreMini.Core.Hosting.Server;
using IceDog.NetCoreMini.Core.Http;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpListenerServer : IServer
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly HttpListener _httpListener;
        /// <summary>
        /// 
        /// </summary>
        private readonly string[] _urls;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urls"></param>
        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            _urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" };
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        /// <returns></returns>
        public async Task StartAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));
            _httpListener.Start();
            while (true)
            {
                var listenerContext = await _httpListener.GetContextAsync();
                var feature = new HttpListenerFeature(listenerContext);
                var features = new FeatureCollection()
                    .Set<IHttpRequestFeature>(feature)
                    .Set<IHttpResponseFeature>(feature);
                var httpContext = new HttpContext(features);
                await handler(httpContext);
                listenerContext.Response.Close();
            }
        }
    }
}
