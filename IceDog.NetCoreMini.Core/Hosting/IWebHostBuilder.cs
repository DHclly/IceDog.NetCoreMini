using IceDog.NetCoreMini.Core.Builder;
using IceDog.NetCoreMini.Core.Hosting.Server;
using System;

namespace IceDog.NetCoreMini.Core.Hosting
{
    /// <summary>
    /// web宿主构建器接口
    /// </summary>
    public interface IWebHostBuilder
    {
        /// <summary>
        /// 使用服务
        /// </summary>
        /// <param name="server">服务</param>
        /// <returns></returns>
        IWebHostBuilder UseServer(IServer server);
        /// <summary>
        /// 配置构建器
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        IWebHostBuilder Configure(Action<IApplicationBuilder> configure);
        /// <summary>
        /// 构建宿主
        /// </summary>
        /// <returns></returns>
        IWebHost Build();
        /// <summary>
        /// 使用的http监听器
        /// </summary>
        /// <param name="urls">监听urls</param>
        /// <returns></returns>
        IWebHostBuilder UseHttpListener(params string[] urls);
    }
}
