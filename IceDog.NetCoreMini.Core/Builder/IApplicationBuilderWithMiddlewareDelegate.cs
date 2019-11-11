using IceDog.NetCoreMini.Core.Http;
using System;
namespace IceDog.NetCoreMini.Core.Builder
{
    /// <summary>
    /// 通过定义一些机制来配置Application的请求管道
    /// </summary>
    public interface IApplicationBuilderWithMiddlewareDelegate
    {
        /// <summary>
        /// 添加需要的中间件
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        IApplicationBuilder Use(MiddlewareDelegate middleware);
        /// <summary>
        /// 构建一个程序用于处理http 请求
        /// </summary>
        /// <returns></returns>
        RequestDelegate Build();
    }
}
