# Module 2: Building Custom Middleware

*Module Goal: How to build custom Middleware*

### Understanding the Diagnostics Middleware
*This module can work on it's own or as a continuation of [using the Middleware](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Introduction/IntroductiontoASPNETCore.md#using-the-middleware)*

- Open up the `project.json` you will notice the `Microsoft.AspNetCore.Diagnostics` package 

```
"dependencies": {
    "Microsoft.NETCore.App": {
      "version": "1.0.0",
      "type": "platform"
    },
    "Microsoft.AspNetCore.Diagnostics": "1.0.0",

  },

```
- Go to `Configure()` method in `Startup.cs` ensure that your new buggy code occurs after the exception page is wired up

```
 public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run((context) =>
            {
                throw new InvalidOperationException("Did I do that ? ~Steve Urkel");
            });
        }
```
- Run your application and browse to `http://localhost:8081` *(or localhost:5000 if you are working in a new app)*
- The debugger will break at the InvalidOperationException

![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/execption.PNG)

- Hit F5 again to continue and you should see an application exception page in the browser

![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/appexception.PNG)

### Adding an handler for non-development environments

- Add exception handler middleware to the Configure method.
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(subApp =>
                {
                    subApp.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("<strong> Application error. Please contact support. </strong>");
                        await context.Response.WriteAsync(new string(' ', 512));  // Padding for IE
                    });
                });


            }
```
- Run the application in "Production" and open a browser window with http://localhost:8081/ as the address. Type F5 at the exception and you should see the custom error page instead of the exception.

### Showing custom pgaes for non 500 status codes

- Change the middleware throwing the exception message to instead set a 404 status code

```
    app.Run((context) =>
            {
                context.Response.StatusCode = 404;
                return Task.FromResult(0);
            });
```

-  Add the status code pages middleware above the exception handler middleware in Configure method 

```
        app.UseStatusCodePages(subApp =>
                {
                    subApp.Run(async context =>
                    {
                        context.Response.ContentType = "text/html";
                        await context.Response.WriteAsync("<strong> Application error. Please contact support. </strong>");
                        await context.Response.WriteAsync(new string(' ', 512));  // Padding for IE
                    });
                });

```
- Run the application and open a browser window with http://localhost:8081/ as the address. You should see the custom error page instead of the browser's default 404 page.

### Extra

If you have time add authentication with third party authentication using ```dotnet user-secrets```

Resources 
- [More on Custom Middleware](https://msdn.microsoft.com/en-us/magazine/mt707525.aspx)
- [Middleware in general](https://docs.asp.net/en/latest/fundamentals/middleware.html?highlight=middleware)
