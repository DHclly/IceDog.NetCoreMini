using IceDog.NetCoreMini.Core;
using IceDog.NetCoreMini.Core.Extensions;
using IceDog.NetCoreMini.Core.Hosting;
using IceDog.NetCoreMini.Core.Http;
using System;
using System.Threading.Tasks;

namespace IceDog.NetCoreMini.WebServer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("server is running ，please visit http://localhost:5001/");
            //步骤 1 , 构建一个webHostBuilder对象
            //内部实现：
            //1.1 初始化属性 WebHostBuilder._server  type:IServer
            //1.2 初始化属性 WebHostBuilder._configures type:List<Action<IApplicationBuilder>>
            IWebHostBuilder webHostBuilder = new WebHostBuilder();

            //步骤 2，对WebHostBuilder._server赋值
            //内部实现：
            //2.1.通过传入的url构建一个 HttpListenerServer 对象，
            //初始化的内容有
            //2.1.1 实例化一个 HttpListener 对象并赋值给 HttpListenerServer._httpListener type:HttpListener
            //2.1.2 把传入的 url 赋值给 HttpListenerServer._urls type:string[]
            //2.2 把HttpListenerServer 对象实例赋值给WebHostBuilder._server  type:IServer
            webHostBuilder = webHostBuilder.UseHttpListener("http://localhost:5001/");

            //步骤 3，对WebHostBuilder._configures添加值，类型是Action<IApplicationBuilder>
            webHostBuilder = webHostBuilder.Configure(app =>
                {
                    app.Use(OneMiddleware)
                    .Use(TwoMiddleware)
                    .Use(ThreeMiddleware);
                });
            //步骤 4，通过webHostBuilder构建webHost对象
            //内部实现：
            //4.1 构建一个 ApplicationBuilder 对象
            //初始化的内容有
            //4.1.1 初始化属性 ApplicationBuilder._middlewares type:List<Func<RequestDelegate, RequestDelegate>>
            IWebHost webHost = webHostBuilder.Build();
            Task startTask = webHost.StartAsync();
            startTask.Wait();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public static RequestDelegate OneMiddleware(RequestDelegate next)
        => async context =>
        {
            await context.Response.WriteAsync("1=>");
            await next(context);
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public static RequestDelegate TwoMiddleware(RequestDelegate next)
        => async context =>
        {
            await context.Response.WriteAsync("2=>");
            await next(context);
        };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public static RequestDelegate ThreeMiddleware(RequestDelegate next)
        => async (context) =>
          {
              await context.Response.WriteAsync("3");
              await next(context);
          };
    }
}
