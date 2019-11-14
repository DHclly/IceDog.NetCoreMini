using IceDog.NetCoreMini.Core.Http;
using System;
namespace IceDog.NetCoreMini.Core.Builder
{
    /// <summary>
    /// 通过定义一些机制来配置Application的请求管道
    /// </summary>
    public interface IApplicationBuilder
    {
        /// <summary>
        /// 添加需要的中间件
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware);
        /// <summary>
        /// 构建一个用于处理http 请求的程序
        /// </summary>
        /// <returns></returns>
        RequestDelegate Build();
    }
}
