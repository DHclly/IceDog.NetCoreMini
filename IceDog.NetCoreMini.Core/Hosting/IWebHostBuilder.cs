using IceDog.NetCoreMini.Core.Builder;
using IceDog.NetCoreMini.Core.Hosting.Server;
using System;

namespace IceDog.NetCoreMini.Core.Hosting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWebHostBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        IWebHostBuilder UseServer(IServer server);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        IWebHostBuilder Configure(Action<IApplicationBuilder> configure);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IWebHost Build();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urls"></param>
        /// <returns></returns>
        IWebHostBuilder UseHttpListener(params string[] urls);
    }
}
