using IceDog.NetCoreMini.Core.Hosting;
using IceDog.NetCoreMini.Core.Hosting.Server;
using IceDog.NetCoreMini.Core.Http;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class WebHost : IWebHost
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <param name="handler"></param>
        public WebHost(IServer server, RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IServer _server;
        /// <summary>
        /// 
        /// </summary>
        private readonly RequestDelegate _handler;
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task StartAsync() => _server.StartAsync(_handler);
    }
}
