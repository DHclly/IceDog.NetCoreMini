using IceDog.NetCoreMini.Core.Hosting;
using IceDog.NetCoreMini.Core.Hosting.Server;
using IceDog.NetCoreMini.Core.Http;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core
{
    /// <summary>
    /// web宿主
    /// </summary>
    public class WebHost : IWebHost
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="server">服务器</param>
        /// <param name="handler">处理程序</param>
        public WebHost(IServer server, RequestDelegate handler)
        {
            _server = server;
            _handler = handler;
        }

        /// <summary>
        /// 服务器
        /// </summary>
        private readonly IServer _server;
        /// <summary>
        /// 处理程序
        /// </summary>
        private readonly RequestDelegate _handler;

        /// <summary>
        /// 异步启动任务
        /// </summary>
        /// <returns></returns>
        public Task StartAsync() => _server.StartAsync(_handler);
    }
}
