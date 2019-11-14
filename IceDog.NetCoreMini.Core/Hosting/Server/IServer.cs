using IceDog.NetCoreMini.Core.Http;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core.Hosting.Server
{
    /// <summary>
    /// server类
    /// </summary>
    public interface IServer
    {
        /// <summary>
        /// 异步启动任务
        /// </summary>
        /// <param name="handler">http处理程序</param>
        /// <returns></returns>
        Task StartAsync(RequestDelegate handler);
    }
}
