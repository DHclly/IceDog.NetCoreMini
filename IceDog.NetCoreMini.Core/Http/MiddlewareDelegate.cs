namespace IceDog.NetCoreMini.Core.Http
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDelegate"></param>
    /// <returns></returns>
    /// <remarks>Equal to Func<RequestDelegate,RequestDelegate></remarks>
    public delegate RequestDelegate MiddlewareDelegate(RequestDelegate requestDelegate);
}
