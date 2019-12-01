# IceDog.NetCoreMini

迷你版的ASP.NET Core框架。



参考链接：

200行代码，7个对象——让你了解ASP.NET Core框架的本质 - Artech - 博客园
https://www.cnblogs.com/artech/p/inside-asp-net-core-framework.html

## IceDog.NetCore.Sample

此项目为官方的默认项目，起参照作用

现在框架版本是 .net core 3.0



## IceDog.NetCoreMini.Core

实现的mini版 .net core 框架，代码位置和官方框架（.net core 3.0）的代码命名空间大部分是一致的



## IceDog.NetCoreMini.WebServer

测试的web项目，运行此项目即可

## 代码运行流程说明记录

代码是在IceDog.NetCoreMini.WebServer中运行的

```c#
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
            //1.1 初始化属性 WebHostBuilder._configures type:List<Action<IApplicationBuilder>>
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
            //4.4 构建一个webHost对象
            //内部实现：
            //4.4.1 给 WebHost._server 赋值 WebHostBuilder._server ，而WebHostBuilder._server是步骤 2 的 HttpListenerServer 对象实例
            //4.4.2 给 WebHost._handler 赋值的是步骤 4.3 生成的委托函数
            IWebHost webHost = webHostBuilder.Build();
            //步骤 5 调用 WebHost.StartAsync()，启动webhost
            //内部实现：
            //5.1 调用 _server.StartAsync(_handler)，这里的 _server，来源于步骤 4.4.1，即 HttpListenerServer 对象实例
            //5.1.1 把步骤 2.1.2 填充给 HttpListenerServer.Prefixes，指定此HttpListenerServer 对象实例可以处理的url前缀
            //5.1.2 启动实例进行监听 HttpListenerServer.Start();
            //5.1.3 创建一个死循环进行轮询监听请求
            //5.1.4 await _httpListener.GetContextAsync(),等候异步获取上下文，此处会卡住
            //，直到有一个请求进来，然后生成此请求的上下文
            //5.1.5 访问 http://localhost:5001/
            //5.1.6 有请求进来 ，获取此请求的上下文 HttpListenerContext 对象 listenerContext，
            //然后通过此上下文构建一个 HttpListenerFeature 对象 feature
            //5.1.7 赋值 HttpListenerFeature._context 为 HttpListenerContext 对象 listenerContext
            //5.1.8 构建一个 FeatureCollection 对象 features
            //5.1.9 通过 features.Set<IHttpRequestFeature>(feature); 把 feature 对象转换为 IHttpRequestFeature 赋值给 features
            //5.1.10 通过 features.Set<IHttpResponseFeature>(feature); 把 feature 对象转换为 IHttpResponseFeature 赋值给 features
            //5.1.11 var httpContext = new HttpContext(features); 使用 features 对象来构建一个我们定义的 HttpContext 对象
            //到这里已经通过feature 层把 HttpListenerContext 转换成我们自定义的 HttpContext
            //5.1.12 HttpContext 内部自然是使用 features 构建 HttpRequest HttpResponse 对象
            //5.1.13 await handler(httpContext); 调用 WebHost._handler 并传入 httpContext 对象，
            //handler步骤 4.3 生成的委托函数 ,通过搜索 <步骤5.1.13> 可以定位代码
            //handler委托函数内部实现：
            //5.1.13.1 创建一个 next 委托函数，设置响应状态码为404，返回为Task（可以看作是异步的Void返回）
            //5.1.13.2 遍历当前 ApplicationBuilder._middlewares，分别进行 next = middleware(next) 运算，形成的结果是
            //5.1.13.3 第一次执行 next = middleware(next) ， 此时 middleware 为 ThreeMiddleware 委托函数，
            //接收的是响应返回为404的委托函数，返回是是 ThreeMiddleware 委托函数
            //5.1.13.4 第一次执行 next = middleware(next) ， 此时middleware为 TwoMiddleware 委托函数，
            //接收的是 ThreeMiddleware 委托函数，返回是是 TwoMiddleware 委托函数
            //5.1.13.5 第一次执行 next = middleware(next) ， 此时middleware为 OneMiddleware 委托函数，
            //接收的是 TwoMiddleware 委托函数，返回是是 OneMiddleware 委托函数
            //5.1.13.6 当前next 赋值的是 OneMiddleware委托函数，返回是 OneMiddleware 委托函数
            //步骤 6 listenerContext.Response.Close(); 释放当前 listenerContext.Response 所占有的资源
            Task startTask = webHost.StartAsync();
            startTask.Wait();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        //public static RequestDelegate OneMiddleware(RequestDelegate next)
        //=> async context =>
        //{
        //    await context.Response.WriteAsync("1=>");
        //    await next(context);
        //};
        public static RequestDelegate OneMiddleware(RequestDelegate next)
        {
            Console.WriteLine("执行 OneMiddleware 委托");

            return async context =>
            {
                Console.WriteLine("执行 OneMiddleware 返回的委托");
                await context.Response.WriteAsync("1=>");
                await next(context);
            };
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        //public static RequestDelegate TwoMiddleware(RequestDelegate next)
        //=> async context =>
        //{
        //    await context.Response.WriteAsync("2=>");
        //    await next(context);
        //};
        public static RequestDelegate TwoMiddleware(RequestDelegate next)
        {
            Console.WriteLine("执行 TwoMiddleware 委托");
            return async context =>
            {
                Console.WriteLine("执行 TwoMiddleware 返回的委托");
                await context.Response.WriteAsync("2=>");
                await next(context);
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        //public static RequestDelegate ThreeMiddleware(RequestDelegate next)
        //=> async (context) =>
        //  {
        //      await context.Response.WriteAsync("3");
        //      await next(context);
        //  };

        public static RequestDelegate ThreeMiddleware(RequestDelegate next)
        {
            Console.WriteLine("执行 ThreeMiddleware 委托");
            return async (context) =>
            {
                Console.WriteLine("执行 ThreeMiddleware 返回的委托");
                await context.Response.WriteAsync("3");
                await next(context);
            };
        }
    }
}

```

##  注意

### 1.为什么访问了一次浏览器控制台会输出两次请求内容？

```
执行 ThreeMiddleware 委托
执行 TwoMiddleware 委托
执行 OneMiddleware 委托
执行 OneMiddleware 返回的委托
执行 TwoMiddleware 返回的委托
执行 ThreeMiddleware 返回的委托
执行 ThreeMiddleware 委托
执行 TwoMiddleware 委托
执行 OneMiddleware 委托
执行 OneMiddleware 返回的委托
执行 TwoMiddleware 返回的委托
执行 ThreeMiddleware 返回的委托
```

原因是浏览器请求了两次

```
http://localhost:5001/
http://localhost:5001/favicon.ico
```

### 2.中间件的格式没有看明白

展开格式就可看明白了，如下所示，两者是一样的

```c#
public static RequestDelegate OneMiddleware(RequestDelegate next)
=> async context =>
{
    await context.Response.WriteAsync("1=>");
    await next(context);
};

public static RequestDelegate OneMiddleware(RequestDelegate next)
{
	Console.WriteLine("执行 OneMiddleware 委托");
	RequestDelegate requestDelegate = async delegate (HttpContext context)
	{
		Console.WriteLine("执行 OneMiddleware 返回的委托");
		await context.Response.WriteAsync("1=>");
		await next(context);
	};
	return requestDelegate;
}
```

### 3.为什么ApplicationBuilder.Build()里面要对middlewares进行反转顺序？

因为最终处理 http请求的是中间件的返回委托，而不是中间件，中间件起到连接的作用，

先存储的是 one 、two、three 中间件委托函数。

然后看build函数的返回委托函数里面第一个执行的返回委托是**最后一个中间件**返回委托，
因此想要先执行 OneMiddleWare 的返回委托，那么 OneMiddleWare 就得在最后进行调用得到返回委托，
然后赋值给 next，进行执行委托。

实质上就是执行 `Func<RequestDelegate, RequestDelegate>` 委托，然后返回`RequestDelegate`
委托，然后执行此委托。

如果熟悉javascript这种函数是一等公民的语言的话是很容易理解的，如下所示

```js
var appBuilder = {
    middlewares: [],
    build() {
        this.middlewares.reverse();
        return (arr) => {
            let next = (arr) => arr.push("end");
            this.middlewares.forEach(middleware => {
                next = middleware(next)
            });
            return next(arr);
        }
    }
}

const oneMiddleWare = (next) => {
    return text => {
        arr.push("1=>");
        next(arr);
    }
}

const twoMiddleWare = (next) => {
    return text => {
        arr.push("2=>");
        next(arr);
    }
}

const threeMiddleWare = (next) => {
    return arr => {
        arr.push("3=>");
        next(arr);
    }
}

appBuilder.middlewares.push(oneMiddleWare);
appBuilder.middlewares.push(twoMiddleWare);
appBuilder.middlewares.push(threeMiddleWare);

var arr = [];
let app = appBuilder.build();
app(arr);
console.log(arr);//1=>,2=>,3=>,end
document.write(arr);//1=>,2=>,3=>,end
```