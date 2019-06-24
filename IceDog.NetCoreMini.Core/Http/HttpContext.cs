using IceDog.NetCoreMini.Core.Http.Features;

namespace IceDog.NetCoreMini.Core.Http
{
    /// <summary>
    /// http 上下文
    /// </summary>
    public class HttpContext
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="features"></param>
        public HttpContext(IFeatureCollection features)
        {
            Request = new HttpRequest(features);
            Response = new HttpResponse(features);
        }

        /// <summary>
        /// 请求对象
        /// </summary>
        public HttpRequest Request { get; }
        /// <summary>
        /// 响应对象
        /// </summary>
        public HttpResponse Response { get; }
        
    }
}
