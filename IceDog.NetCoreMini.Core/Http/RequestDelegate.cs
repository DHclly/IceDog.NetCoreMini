using IceDog.NetCoreMini.Core.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core.Http
{
    /// <summary>
    /// 能处理 http请求的函数
    /// </summary>
    /// <param name="context">http 上下文</param>
    /// <returns></returns>
    /// <remarks>Equal to Func<HttpContext,Task></remarks>
    public delegate Task RequestDelegate(HttpContext context);
}
