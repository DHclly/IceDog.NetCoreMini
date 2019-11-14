using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core.Hosting
{
    /// <summary>
    /// web宿主接口
    /// </summary>
    public interface IWebHost
    {
        /// <summary>
        /// 异步启动任务
        /// </summary>
        /// <returns></returns>
        Task StartAsync();
    }
}
