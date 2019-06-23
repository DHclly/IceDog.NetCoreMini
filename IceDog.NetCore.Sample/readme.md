# Read Me

此项目用于演示 .net core 的整个执行流程，参考链接：

https://www.cnblogs.com/artech/p/inside-asp-net-core-framework.html

当你在学习一个开发框架的时候不要只关注**编程层面**的东西，而应该将更多的精力集中到对**架构设计层面**的学习。

好的设计一定是“简单”的设计

计算机领域有一句非常经典的话：“任何问题都可以通过添加一个抽象层的方式来解决，如果解决不了，那就再加一层”。

## 执行流程

1. 通过`new WebHostBuilder()` 或者`WebHost.CreateDefaultBuilder()`创建`IWebHostBuilder webHostBuilder`
2. 通过`webHostBuilder.Build()`创建`IWebHost webHost`
3. `webHost.Run()` 启动程序

在步骤2之前，可以通过如下代码对webHostBuilder进行配置
```c#
webHostBuilder.UseKestrel();//配置kestrel服务器

//配置中间件处理请求的响应
.Configure(app => app.Run(async context => 
    await context.Response.WriteAsync("Hello World!")))
```
## 相关代码
默认代码

```c#
public static void Main(string[] args)
{
    IWebHost webHost = CreateWebHostBuilder(args).Build();
    webHost.Run();
}
/*
public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
        .UseStartup<Startup>();
*/
public static IWebHostBuilder CreateWebHostBuilder(string[] args)
{
    IWebHostBuilder webHostBuilder = WebHost.CreateDefaultBuilder(args);
    webHostBuilder.UseStartup<Startup>();
    return webHostBuilder;
}
```

演示代码

```csharp
public class Program
{
    public static void Main(string[] args) => new WebHostBuilder()
    .UseKestrel()
    .Configure(app => app.Run(async context => await context.Response.WriteAsync("Hello World!")))
    .Build()
    .Run();
}

public class Program
{
    public static void Main(string[] args) => WebHost.CreateDefaultBuilder()
    .UseKestrel()
    .Configure(app => app.Run(async context => await context.Response.WriteAsync("Hello World!")))
    .Build()
    .Run();
}
````