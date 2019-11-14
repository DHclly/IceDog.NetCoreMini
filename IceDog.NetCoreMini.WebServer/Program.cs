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
            MainAsync().Wait();
        }
        private static async Task MainAsync()
        {
            Console.WriteLine("server is running ，please visit http://localhost:5001/");
            await new WebHostBuilder()
                .UseHttpListener("http://localhost:5001/")
                .Configure(app =>
                {
                    app.Use(OneMiddleware)
                    .Use(TwoMiddleware)
                    .Use(ThreeMiddleware);
                })
                .Build()
                .StartAsync();
        }
        public static RequestDelegate OneMiddleware(RequestDelegate next)
        => async context =>
        {
            await context.Response.WriteAsync("1=>");
            await next(context);
        };

        public static RequestDelegate TwoMiddleware(RequestDelegate next)
        => async context =>
        {
            await context.Response.WriteAsync("2=>");
            await next(context);
        };

        public static RequestDelegate ThreeMiddleware(RequestDelegate next)
        => context => context.Response.WriteAsync("3");
    }
}
