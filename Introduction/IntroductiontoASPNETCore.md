 # Module 2: Introduction into ASP.NET Core 

*Module goal: In this module we are taking the audience from Console App to WebApp. During during the module will be introduced to the following:*
- *Create a new web app both in VS2017 and commandline*
- *How to run an application with IIS or Kestrel*
- *An intro to using the middleware. E.g. Serving Static files*

#### Watch:[Update Link- Getting Started with Middleware MVA here.](https://mva.microsoft.com/en-US/training-courses/introduction-to-asp-net-core-1-0-16841?l=yCG2vdE6C_6406218965)
## Create a new Web Application 

**For this section you can either use VS Code or Visual Studio 2017 RTM. If you would like to do this section in VS Code please checkout the [cross platform section](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/CrossPlatform/IntroductiontoASPNETCore.md).**

- Open up Visual Studio 2017
- Create a new ASP.NET Core application 

    Go to File New Project ->.NETCore -> ASP.NET Core Web Application (.NET Core)

    ![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/Filenew.png)
    ![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/Filenew_empty.png)

**This can also be done in the commandline with**
  
    ```
    dotnet new web
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
- Go to the `Debug` tab --> Application properties--> Debug and change `Launch URL` to `http://localhost:8081`
     
    ![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/ChangePorts.png)
   
- Run the application and navigate to the root. It should show the hello world middleware running on port 8081.

> **Note:** If the page does not load correctly, verify that the console application host is running and refresh the browser.

## Using the Middleware

### Serving static Pages
- Add the `Microsoft.AspNetCore.StaticFiles` package to `csproj`: 

*To edit your csproj in Visual Studio: Right click on your application name and select edit csproj*
![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/editcsproj.png)

Option 1:Edit by hand 
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
  
  ![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/nugetUI.png)
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

![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/helloagain.PNG)

## Adding default document support

- Change the static files middleware in `Startup.cs` from `app.UseStaticFiles()` to `app.UseFileServer()`.
- Run the application. The default page `index.html` should show when navigating to the root of the site.

## Changing environments

- The default environment in visual studio is development. In the property pages(shift+F4) you can see this is specified by the environment variables section:

  ![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/Env_Var.PNG)

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
 
![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/Env_Var_Prd.PNG)

- Run the application and it should print out `Hello World! Production`.

