using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IceDog.NetCore.Sample
{
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// one middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseOneMiddleware(this IApplicationBuilder builder)
        {
            builder.Use((next) => async (httpContext) =>
            {
                await httpContext.Response.WriteAsync("1=>");
                await next(httpContext);
            });
            return builder;
        }
        /// <summary>
        /// two middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseTwoMiddleware(this IApplicationBuilder builder)
        {
            builder.Use((next) => async (httpContext) =>
            {
                await httpContext.Response.WriteAsync("2=>");
                await next(httpContext);
            });
            return builder;
        }
        /// <summary>
        /// three middleware
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseThreeMiddleware(this IApplicationBuilder builder)
        {
            builder.Use((next) => async (httpContext) =>
            {
                await httpContext.Response.WriteAsync("3=>");
                await next(httpContext);
            });
            return builder;
        }
    }
}
