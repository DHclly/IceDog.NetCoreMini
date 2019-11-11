namespace IceDog.NetCoreMini.Core.Http
{
    /// <summary>
    /// 中间件委托
    /// </summary>
    /// <param name="requestDelegate"><请求委托/param>
    /// <returns>请求委托</returns>
    /// <remarks>Equal to Func<RequestDelegate,RequestDelegate></remarks>
    public delegate RequestDelegate MiddlewareDelegate(RequestDelegate requestDelegate);
}
