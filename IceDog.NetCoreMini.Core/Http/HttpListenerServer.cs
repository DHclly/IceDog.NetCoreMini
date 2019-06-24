using IceDog.NetCoreMini.Core.Hosting.Server;
using IceDog.NetCoreMini.Core.Http.Features;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core.Http
{
    /// <summary>
    /// http监听服务
    /// </summary>
    public class HttpListenerServer : IServer
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly HttpListener _httpListener;
        /// <summary>
        /// 监听的url列表
        /// </summary>
        private readonly string[] _urls;
        /// <summary>
        /// 自定义构造函数
        /// </summary>
        /// <param name="urls"></param>
        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            var port = new Random().Next(3000, 10000);
            _urls = urls.Any() ? urls : new string[] { $"http://localhost:{port}/" };
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

                var features = new FeatureCollection();
                features.Set<IHttpRequestFeature>(feature);
                features.Set<IHttpResponseFeature>(feature);

                var httpContext = new HttpContext(features);

                await handler(httpContext);
                listenerContext.Response.Close();
            }
        }
    }
}
