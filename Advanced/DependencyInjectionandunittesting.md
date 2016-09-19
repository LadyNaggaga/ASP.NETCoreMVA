# Module 3: Dependency Injection 

#### *Modules goals*

- Using DI to register & resolve application service 
	- Create a service to assign request IDs
	- Register the request ID service in DI 
	- Create and add middleware that logs the request ID 
	- Adding external packages
-  Go through [Transient, Scoped, and Singleton](https://docs.asp.net/en/latest/fundamentals/dependency-injection.html)

## Creating a service to assign request IDs

- Create a new application using the ASP.NET Core Web Application (.NET Core) (Empty Template)
-  Create a folder in the application called `Services`
-  Create a new class file in the `Services` folder `RequestId`
-  In the file, create an interface `IRequestIdFactory` with a single method `string MakeRequestId()`
-  In the same file, create a class `RequestIdFactory` that implements `IRequestIdFactory` by using `Interlock.Increment` to make an increasing request ID
-  The file should look something like this:

  ``` C#
  public interface IRequestIdFactory
  {
      string MakeRequestId();
  }

  public class RequestIdFactory : IRequestIdFactory
  {
      private int _requestId;

      public string MakeRequestId() => Interlocked.Increment(ref _requestId).ToString();
  }
  ```

-  In the same file, create an interface `IRequestId` with a single property `string Id { get; }`
-  In the same file, create a class `RequestId` that implements `IRequestId` by taking an `IRequestIdFactory` in the constructor and calling its `MakeRequestId` method to get a new ID.
-  The whole file should now look something like this:

  ``` C#
  using System.Threading;

  public interface IRequestIdFactory
  {
      string MakeRequestId();
  }
  
  public class RequestIdFactory : IRequestIdFactory
  {
      private int _requestId;
  
      public string MakeRequestId() => Interlocked.Increment(ref _requestId).ToString();
  }
  
  public interface IRequestId
  {
      string Id { get; }
  }
  
  public class RequestId : IRequestId
  {
      private readonly string _requestId;
  
      public RequestId(IRequestIdFactory requestIdFactory)
      {
          _requestId = requestIdFactory.MakeRequestId();
      }
  
      public string Id => _requestId;
  }
  ```

## Register the request ID service in DI
- In the application's `Startup.cs` file, find the `ConfigureServices(IServiceCollection services)` method.
- Register the `IRequestIdFactory` service as a singleton: `services.AddSingleton<IRequestIdFactory, RequestIdFactory>();`
- Register the `IRequestId` service as scoped: `services.AddScoped<IRequestId, RequestId>();`
- The `ConfigureServices` method should now look something like this:

  ``` C#
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddSingleton<IRequestIdFactory, RequestIdFactory>();
      services.AddScoped<IRequestId, RequestId>();
  }
  ```

## Create and add a middleware that logs the request ID
-  Create a new folder in the application `Middleware`
-  In the folder, create a class `RequestIdMiddleware`
-  Create a constructor `public RequestIdMiddleware(RequestDelegate next, IRequestId requestId, ILogger<RequestIdMiddleware> logger)` and store the parameters in private fields
-  Add a method `public Task Invoke(HttpContext context)` and in its body log the request ID using the `ILogger` and `IRequestId` injected from the constructor
-  Your middleware class should look something like this:

  ``` C#
  using Microsoft.AspNetCore.Http;
  using Microsoft.Extensions.Logging;
  using System.Threading.Tasks;

  public class RequestIdMiddleware
  {
      private readonly RequestDelegate _next;
      private readonly ILogger<RequestIdMiddleware> _logger;
  
      public RequestIdMiddleware(RequestDelegate next, IRequestId requestId, ILogger<RequestIdMiddleware> logger)
      {
          _next = next;
          _logger = logger;
      }
  
      public Task Invoke(HttpContext context, IRequestId requestId)
      {
          _logger.LogInformation($"Request {requestId.Id} executing.");
  
          return _next(context);
      }
  }
  ```

-  Add the middleware to your pipeline back in `Startup.cs` by calling `app.UseMiddleware<RequestIdMiddleware>();` before the call to `app.Run()`:

  ``` C#
    app.UseMiddleware<RequestIdMiddleware>();

    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("Hello World!");
    });  
  ```

-  Change the Debug drop down in the toolbar to the application name as shown below.
  
  ![image](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/run-with-kestrel.png)

- Run the application. You should see the logging messages from the middleware in the console output.

# Adding a unit test project

Follow the instructions at https://xunit.github.io/docs/getting-started-dotnet-core.html to add an xUnit testing project.
