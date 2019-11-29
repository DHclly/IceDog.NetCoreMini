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
                    //4.2.1 调用 ApplicationBuilder.Use方法添加 OneMiddleware
                    //实质上是为 ApplicationBuilder._middlewares 赋值
                    app.Use(OneMiddleware);
                    //4.2.2 调用 ApplicationBuilder.Use方法添加 TwoMiddleware
                    //实质上是为 ApplicationBuilder._middlewares 赋值
                    app.Use(TwoMiddleware);
                    //4.2.3 调用 ApplicationBuilder.Use方法添加 ThreeMiddleware
                    //实质上是为 ApplicationBuilder._middlewares 赋值
                    app.Use(ThreeMiddleware);

                    //此时middleware 的顺序是 one、two、three
                });
            //步骤 4，通过webHostBuilder构建webHost对象
            //内部实现：
            //4.1 构建一个 ApplicationBuilder 对象
            //初始化的内容有
            //4.1.1 初始化属性 ApplicationBuilder._middlewares type:List<Func<RequestDelegate, RequestDelegate>>
            //4.2 遍历 WebHostBuilder._configures，依次执行通过对WebHostBuilder.Configure（如步骤3）添加的action委托
            //实质上是为 4.1 构建的 ApplicationBuilder 对象赋值
            //跳转到 上面的4.2.1
            //4.3 调用 ApplicationBuilder.Build()方法返回一个委托函数 type:RequestDelegate
            //RequestDelegate 的定义:Task RequestDelegate(HttpContext context)
            //内部实现：
            //4.3.1 反转 ApplicationBuilder._middlewares ，此时middleware 的顺序是 three、two、one
            //4.3.2 返回一个返回是 ApplicationBuilder._middlewares 最后一个委托函数（这里是 OneMiddleware ）的委托函数
            //4.3.2 创建一个 next 委托函数，设置响应状态码为404，返回为Task（可以看作是异步的Void返回）
            //4.3.3 遍历当前 ApplicationBuilder._middlewares，分别进行 next = middleware(next) 运算，形成的结果是
            //4.3.4 第一次执行 next = middleware(next) ， 此时 middleware 为 ThreeMiddleware 委托函数，
            //接收的是响应返回为404的委托函数，返回是是 ThreeMiddleware 委托函数
            //4.3.5 第一次执行 next = middleware(next) ， 此时middleware为 TwoMiddleware 委托函数，
            //接收的是 ThreeMiddleware 委托函数，返回是是 TwoMiddleware 委托函数
            //4.3.6 第一次执行 next = middleware(next) ， 此时middleware为 OneMiddleware 委托函数，
            //接收的是 TwoMiddleware 委托函数，返回是是 OneMiddleware 委托函数
            //4.3.7 当前next 赋值的是 OneMiddleware委托函数，返回是 OneMiddleware 委托函数
            //4.4 构建一个webHost对象
            //内部实现：
            //4.4.1 给 WebHost._server 赋值 WebHostBuilder._server ，而WebHostBuilder._server是步骤 2 的 HttpListenerServer 对象实例
            //4.4.2 给 WebHost._handler 赋值 步骤 4.3 生成的委托函数
            IWebHost webHost = webHostBuilder.Build();
            //步骤 5 调用 WebHost.StartAsync(WebHost._handler)
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
