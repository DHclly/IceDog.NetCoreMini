using IceDog.NetCoreMini.Core.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core.Builder
{
    /// <summary>
    /// 应用构建器
    /// </summary>
    public class ApplicationBuilder : IApplicationBuilder
    {
        /// <summary>
        /// 中间件列表
        /// </summary>
        private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        /// <summary>
        /// 构建一个用于处理http 请求的程序
        /// </summary>
        /// <returns></returns>
        public RequestDelegate Build()
        {
            //反转中间件列表的顺序
            _middlewares.Reverse();

            return httpContext =>
            {
                //在调用第一个中间件（最后注册）的时候，我们创建了一个 RequestDelegate 作为输入
                //，后者会将响应状态码设置为404。所以如果ASP.NET Core应用在没有注册任何中间
                //的情况下总是会返回一个404的响应。如果所有的中间件在完成了自身的请求处理
                //任务之后都选择将请求向后分发，同样会返回一个404响应。
                RequestDelegate next = context => 
                {
                    context.Response.StatusCode = 404;
                    return Task.CompletedTask;
                };

                foreach (var middleware in _middlewares)
                {
                    next = middleware(next);
                }
                return next(httpContext);
            };
        }
        /// <summary>
        /// 添加需要的中间件
        /// </summary>
        /// <param name="middleware">中间件</param>
        /// <returns></returns>
        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}
