# Module 2: Introduction into ASP.NET Core 1.0

## Create a new Web Application 

**For this section you can either use VS Code or Visual Studio 2015 with update 3. If you would like to do this section in VS Code please checkout the [cross platform section](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/CrossPlatform/IntroductiontoASPNETCore.md).**

- Open up Visual Studio 2015 
- Create a new ASP.NET Core application 

    Go to File New Project -> C# -> ASP.NET Core Web Application (.NET Core)

    ![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/Filenew.png)
**This can also be done in the commandline with**
    ```sh
    dotnet new -t web

    ```
*if done in commandline open program in vs code to show file structure*
     
## Running the application under IIS or on Kestrel 
- Change the Debug drop down in the toolbar to the application name

    ![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/run-with-kestrel.png)

- Run the application and navigate to the root. It should show the hello world middleware.
- Change the port to `8081` by adding a call to `UseUrls` in the `Program.cs`:

   ```
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:8081")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
            host.Run();

        }
    }
   ```
- Navigate to the project properties (by right clicking on the project, and selection `Properties`)
- Go to the `Debug` tab and change `Launch URL` to `http://localhost:8081`

   ![image](https://cloud.githubusercontent.com/assets/95136/15806095/157c4c32-2b3c-11e6-91db-b231aa113c31.png)

- Run the application and navigate to the root. It should show the hello world middleware running on port 8081.

> **Note:** If the page does not load correctly, verify that the console application host is running and refresh the browser.

## Using the Middleware

### Serving static Pages
- Add the `Microsoft.AspNetCore.StaticFiles` package to `project.json`:

  ```JSON
  "dependencies": {
    "Microsoft.NETCore.App": {
      "version": "1.0.0",
      "type": "platform"
    },
    "Microsoft.AspNetCore.Diagnostics": "1.0.0",
    
    "Microsoft.AspNetCore.Server.IISIntegration": "1.0.0",
    "Microsoft.AspNetCore.Server.Kestrel": "1.0.0",
    "Microsoft.Extensions.Logging.Console": "1.0.0",
    "Microsoft.AspNetCore.StaticFiles": "1.0.0"
  },
  ```
  
- Save `project.json`. Visual Studio will immediately begin restoring the StaticFiles NuGet package.

- Go to `Startup.cs` in the `Configure` method and add `UseStaticFiles` before the hello world middleware:

  ```C#
  public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
  {
      loggerFactory.AddConsole();

      if (env.IsDevelopment())
      {
          app.UseDeveloperExceptionPage();
      }

      app.UseStaticFiles();

      app.Run(async (context) =>
      {
          await context.Response.WriteAsync("Hello World!");
      });
  }
```
  
- Create a file called `index.html` with the following contents in the `wwwroot` folder:

  ```html
  <!DOCTYPE html>
  <html>
  <head>
      <meta charset="utf-8" />
      <title></title>
  </head>
  <body>
      <h1>Hello from ASP.NET Core!</h1> 
  </body>
  </html>
  ```

- Run the application and navigate to the root. It should show the hello world middleware.
- Navigate to `index.html` and it should show the static page in `wwwroot`.

## Adding default document support

- Change the static files middleware in `Startup.cs` from `app.UseStaticFiles()` to `app.UseFileServer()`.
- Run the application. The default page `index.html` should show when navigating to the root of the site.

## Changing environments

- The default environment in visual studio is development. In the property pages you can see this is specified by the environment variables section:

  ![image](https://cloud.githubusercontent.com/assets/95136/15806164/a57a79a2-2b3d-11e6-9551-9e106036e0c0.png)

- Add some code to the `Configure` method in `Startup.cs` to print out the environment name. Make sure you comment out the UseFileServer middleware. Otherwise you'll still get the same default static page.

  ```C#
  public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
  {
      loggerFactory.AddConsole();

      if (env.IsDevelopment())
      {
          app.UseDeveloperExceptionPage();
      }

      //app.UseFileServer();

      app.Run(async (context) =>
      {
          await context.Response.WriteAsync($"Hello World! {env.EnvironmentName}");
      });
  }
```
- Run the application and it should print out `Hello World! Development`. 
- Change the application to run in the `Production` environment by changing the `ASPNETCORE_ENVIRONMENT` environment variable on the `Debug` property page:
 
  ![image](https://cloud.githubusercontent.com/assets/95136/15806196/9b52efee-2b3e-11e6-851b-35765d5b2a4d.png)

- Run the application and it should print out `Hello World! Production`.

## Setup the configuration system

- Add the `Microsoft.Extensions.Configuration.Json` package to `project.json`:
 
  ```JSON
  "dependencies": {
    "Microsoft.NETCore.App": {
      "version": "1.0.0",
      "type": "platform"
    },
    "Microsoft.AspNetCore.Diagnostics": "1.0.0",
    
    "Microsoft.AspNetCore.Server.IISIntegration": "1.0.0",
    "Microsoft.AspNetCore.Server.Kestrel": "1.0.0",
    "Microsoft.Extensions.Logging.Console": "1.0.0",
    "Microsoft.AspNetCore.StaticFiles": "1.0.0",
    "Microsoft.Extensions.Configuration.Json": "1.0.0"
  },
  ```
1. Add a `Configuration` property to `Startup.cs` of type `IConfigurationRoot`:

```C#
  public class Startup
  {
      ...
      public IConfigurationRoot Configuration { get; set; }
      ...
  }
```

1. Also in `Startup.cs`, add a constructor to the Startup class that configures the configuration system:

  ```C#
  public Startup()
  {
      Configuration = new ConfigurationBuilder()
                          .AddJsonFile("appsettings.json")
                          .Build();
  }
  ```
- Run the application. It should fail with an exception saying that it cannot find the `'appsettings.json'`.
- Create a file in the root of the project called `appsettings.json` with the following content:
  
  ```JSON
  {
    "message": "Hello from configuration"
  }
  ```
  
- Modify the `Startup` constructor in `Startup.cs` to inject `IHostingEnvironment` and use it to set the base path for the configuration system to the `ContentRootPath`:

  ```C#
  public Startup(IHostingEnvironment env)
  {
      Configuration = new ConfigurationBuilder()
                          .SetBasePath(env.ContentRootPath)
                          .AddJsonFile("appsettings.json")
                          .Build();
  }
  ```
  
- In `Startup.cs` modify the `Configure` method to print out the configuration key in the http response:

```C#
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        //app.UseFileServer();

        app.Run(async (context) =>
        {
            await context.Response.WriteAsync($"{Configuration["message"]}");
        });
    }
```

- Run the application and it should print out `Hello from config`.

## Extra
- Add support for reloading the configuration without an application restart.
- Replace the JSON configuration provider with the XML configuration provider
- Write a custom configuration provider

