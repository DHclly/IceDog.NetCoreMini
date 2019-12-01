using IceDog.NetCoreMini.Core.Builder;
using IceDog.NetCoreMini.Core.Hosting.Server;
using IceDog.NetCoreMini.Core.Http;
using System;
using System.Collections.Generic;

namespace IceDog.NetCoreMini.Core.Hosting
{
    /// <summary>
    /// web宿主构建器
    /// </summary>
    public class WebHostBuilder : IWebHostBuilder
    {
        /// <summary>
        /// 服务
        /// </summary>
        private IServer _server;
        /// <summary>
        /// 配置构建器列表
        /// </summary>
        private readonly List<Action<IApplicationBuilder>> _configures;
        public WebHostBuilder()
        {
            _configures = new List<Action<IApplicationBuilder>>();
        }
        /// <summary>
        /// 配置构建器
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }
        /// <summary>
        /// 使用服务
        /// </summary>
        /// <param name="server">服务</param>
        /// <returns></returns>
        public IWebHostBuilder UseServer(IServer server)
        {
            _server = server;
            return this;
        }
        /// <summary>
        /// 构建宿主
        /// </summary>
        /// <returns></returns>
        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();
            foreach (var configure in _configures)
            {
                configure(builder);
            }
            return new WebHost(_server, builder.Build());
        }
        /// <summary>
        /// 使用的http监听器
        /// </summary>
        /// <param name="urls">监听urls</param>
        /// <returns></returns>
        public IWebHostBuilder UseHttpListener(params string[] urls)
        {
            return this.UseServer(new HttpListenerServer(urls));
        }
    }
}
