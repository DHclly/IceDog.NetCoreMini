using IceDog.NetCoreMini.Core.Builder;
using IceDog.NetCoreMini.Core.Builder.Internal;
using IceDog.NetCoreMini.Core.Hosting.Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace IceDog.NetCoreMini.Core.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    public class WebHostBuilder : IWebHostBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        private IServer _server;
        /// <summary>
        /// 
        /// </summary>
        private readonly List<Action<IApplicationBuilder>> _configures = new List<Action<IApplicationBuilder>>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public IWebHostBuilder UseServer(IServer server)
        {
            _server = server;
            return this;
        }
        /// <summary>
        /// 
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
    }
}
