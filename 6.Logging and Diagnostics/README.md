# Module 6: Logging and Diagnostics 
*Module goals: How to add logging to your application*
- *Setup your app for logging*
- *How to filter logs*
- *Add 3rd party logging provideres*
- *Web logging*
### Watch: [Getting Started with Logging and Diagnostics here ](https://mva.microsoft.com/en-US/training-courses/introduction-to-asp-net-core-1-0-16841?l=lVrHmeE6C_9406218965)

### Setup for logging 

- Use application from [introduction into ASP.NET Core 1.0](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Introduction/IntroductiontoASPNETCore.md)
- Add or verify the `Microsoft.Extensions.Logging.Console` package to the `project.json`
```
      "dependencies": {
        "Microsoft.Extensions.Logging.Console": "1.0.0"
      },
```

- Open `Startup.cs`  and update the `configure method`
```
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            var startupLogger = loggerFactory.CreateLogger<Startup>();
            ...
        }
```
- Include a log statement to the `Configure` method
```
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            var startupLogger = loggerFactory.CreateLogger<Startup>();

            startupLogger.LogInformation("Startup is complete!");
        }
```
- Change the Debug drop down in the toolbar to the application name and run the the application

![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/run-with-kestrel.png)

### Filtering Logs
 -  Ad a couple more logging statements 
```C#
 startupLogger.LogCritical("This is a critical message");
    startupLogger.LogDebug("This is a debug message");
    startupLogger.LogTrace("This is a trace message");
    startupLogger.LogWarning("This is a warning message");
    startupLogger.LogError("This is an error message");
```

- Change the minimum log level for the console logger in `Startup.cs`:

  ```C#
    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole(LogLevel.Trace);
        ...
    }
    ```
- Run the application

####  Adding other logging providers

- Add [serilog logger](Serilog logger) provider as dependency in `project.json`
    ```JSON
      "dependencies": {
        ...
        "Serilog.Extensions.Logging":  "1.0.0",
        "Serilog.Sinks.File": "2.0.0"
      },
    ```
- Configure Serilog in `Startup.cs` to write to a file called `logfile.txt` in the project root (resolving usings for System.IO and Serilog):
 ```C#
    public Startup(IHostingEnvironment env)
    {
        Configuration = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json")
                            .Build();

        var logFile = Path.Combine(env.ContentRootPath, "logfile.txt");

        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(logFile)
            .CreateLogger();
    }
    ```
- Add the Serilog provider in `Configure`:

    ```C#
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
    {
        loggerFactory.AddConsole();
        loggerFactory.AddSerilog();
        ...
    }
    ```
- Run the application and open a browser window with `http://localhost:8081/` as the address. You should observe a file called `logfile.txt` appear in your application root. 
- Close the conosle window and open the file, the application logs should be in there.


