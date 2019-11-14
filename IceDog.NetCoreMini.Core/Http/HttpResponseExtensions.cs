using IceDog.NetCoreMini.Core.Http;
using System.Text;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core.Extensions
{
    /// <summary>
    /// HttpResponse扩展方法 
    /// </summary>
    public static partial class HttpResponseExtensions
    {
        /// <summary>
        /// HttpResponse 异步写入
        /// </summary>
        /// <param name="response">响应</param>
        /// <param name="contents">内容</param>
        /// <returns></returns>
        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer: buffer, offset: 0, count: buffer.Length);
        }
    }
}
