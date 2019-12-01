using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IceDog.NetCore.Sample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // one middleware
            //app.Use((next) => async (httpContext) =>
            //{
            //    await httpContext.Response.WriteAsync("1=>");
            //    await next(httpContext);
            //});
            app.UseOneMiddleware();

            // two middleware
            //app.Use((next) => async (httpContext) =>
            //{
            //    await httpContext.Response.WriteAsync("2=>");
            //    await next(httpContext);
            //});
            app.UseTwoMiddleware();

            // three middleware
            //app.Use((next) => async (httpContext) =>
            //{
            //    await httpContext.Response.WriteAsync("3=>");
            //    await next(httpContext);
            //});
            app.UseThreeMiddleware();

            // helloworld middleware
            app.Run(async httpContext => await httpContext.Response.WriteAsync("Hello World"));
        }
    }
}
