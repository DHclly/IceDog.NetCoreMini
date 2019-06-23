using IceDog.NetCoreMini.Core.Http;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core.Hosting.Server
{
    /// <summary>
    /// 
    /// </summary>
    public interface IServer
    {
        Task StartAsync(RequestDelegate handler);
    }
}
