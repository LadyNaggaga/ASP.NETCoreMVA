# Module 2: Introduction into ASP.NET Core 


*Module goal: In this module we are taking the audience from Console App to WebApp. During during the module will be introduced to the following:*
- *Create a new web app both in Visual Studio and commandline*
- *How to run an application with IIS or Kestrel*
- *An intro to using the middleware. E.g. Serving Static files*

## Create a new Web Application 

*For this section you can either use VS Code or Visual Studio 2017 RTM.* 

- Open up Visual Studio
- Create a new ASP.NET Core application 

    Go to File New Project ->.NETCore -> ASP.NET Core Web Application (.NET Core)
    ![image](https://cloud.githubusercontent.com/assets/2546640/23097413/12b3d5de-f601-11e6-83e7-548dddd63159.png)
    ![image](https://cloud.githubusercontent.com/assets/2546640/23097436/ba329502-f601-11e6-99e6-2a6f21cd3193.png)
    

**This can also be done in the commandline with**
  
    ```
    dotnet new web
    ```
*if done in commandline open program in vs code to show file structure*
     
## Running the application under IIS or on Kestrel 
- Change the Debug drop down in the toolbar to the application name
    ![image](https://cloud.githubusercontent.com/assets/2546640/23097455/40937bfc-f602-11e6-941f-f78a50799bc3.png)

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
- Go to the `Debug` tab --> Application properties--> Debug and change `Launch URL` to `http://localhost:8081`
    ![image](https://cloud.githubusercontent.com/assets/2546640/23097466/89975a26-f602-11e6-835f-ccf7fb6629d9.PNG) 
   
- Run the application and navigate to the root. It should show the hello world middleware running on port 8081.

> **Note:** If the page does not load correctly, verify that the console application host is running and refresh the browser.

## Using the Middleware

### Serving static Pages
- Add the `Microsoft.AspNetCore.StaticFiles` package to `csproj`: 

*To edit your csproj in Visual Studio: Right click on your application name and select edit csproj*

![image](https://cloud.githubusercontent.com/assets/2546640/23097477/d0004d9c-f602-11e6-89b3-a898ed01c931.PNG)

Option 1: Edit by hand 
  ```XML
 <Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>netcoreapp1.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Folder Include="wwwroot\" />
  </ItemGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.ApplicationInsights.AspNetCore" Version="2.0.0" />
    <PackageReference Include="Microsoft.AspNetCore" Version="1.0.3" />
    <PackageReference Include="Microsoft.AspNetCore.StaticFiles" Version="1.1.0" />
  </ItemGroup>

</Project>
  ```
  Option 2: Use NuGet package manager

  ![image](https://cloud.githubusercontent.com/assets/2546640/23097484/f721881e-f602-11e6-8539-e4d6b9f1626f.PNG)

- Save `csproj`. Visual Studio will immediately begin restoring the StaticFiles NuGet package.

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
    <h1> Hello Again!</h1>
</body>
</html>
```

- Run the application and navigate to the root. It should show the hello world middleware.
- Navigate to `index.html` and it should show the static page in `wwwroot`.

![image](https://cloud.githubusercontent.com/assets/2546640/23097492/36a63aa2-f603-11e6-88b5-3762c987d8ca.PNG)


## Adding default document support

- Change the static files middleware in `Startup.cs` from `app.UseStaticFiles()` to `app.UseFileServer()`.
- Run the application. The default page `index.html` should show when navigating to the root of the site.

## Enabling directory browsing 
Replace to `app.UseFileServer()` to 
`app.UseFileServer(enableDirectoryBrowsing: true)`

## Changing environments

- The default environment in visual studio is development. In the property pages(shift+F4) you can see this is specified by the environment variables section:

  ![image](https://cloud.githubusercontent.com/assets/2546640/23097502/78ffc4b8-f603-11e6-978c-e19063b3d94d.PNG)
  
- Add some code to the `Configure` method in `Startup.cs` to print out the environment name. Make sure you comment out the UseFileServer middleware. Otherwise you'll still get the same default static page.

  ```C#
  public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
  {
      loggerFactory.AddConsole();

      if (env.IsDevelopment())
      {
          app.UseDeveloperExceptionPage();
      }

      app.Run(async (context) =>
      {
          await context.Response.WriteAsync($"Hello World! {env.EnvironmentName}");
      });
  }
  ```

- Run the application and it should print out `Hello World! Development`. 
- Change the application to run in the `Production` environment by changing the `ASPNETCORE_ENVIRONMENT` environment variable on the `Debug` property page:

![image](https://cloud.githubusercontent.com/assets/2546640/23097506/9e710aae-f603-11e6-9e7c-77756f0361af.PNG)


- Run the application and it should print out `Hello World! Production`.

