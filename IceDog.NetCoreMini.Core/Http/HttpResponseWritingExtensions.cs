using IceDog.NetCoreMini.Core.Http;
using System.Text;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.Core.Extensions
{
    public static partial class HttpResponseWritingExtensions
    {

        public static Task WriteAsync(this HttpResponse response, string contents)
        {
            var buffer = Encoding.UTF8.GetBytes(contents);
            return response.Body.WriteAsync(buffer, 0, buffer.Length);
        }
    }
}
