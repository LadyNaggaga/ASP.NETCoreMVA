# Module 1: Getting Started with ASP.NET Core 

##Install the .NET Core SDK 
1. Go to https://dot.net and follow the instructions to download and install the .NET Core SDK for your OS

### Setting up on different environments
[**OS X**](https://www.microsoft.com/net/core#macos)

- Install Pre-requisites : Homebrew and OpenSSL
- .NET Core  for OS X

[**Windows**](https://www.microsoft.com/net/core#windows)

- Install .NET Core

[**Linux**](https://www.microsoft.com/net/core#ubuntu)

- Set up the apt-get that hosts the packages 
- Install .NET Core SDK

###Introduction to the .NET CLI

- Test that you have you .NET installed by going to commandline or terminal and typing 
```sh
    dotnet 
```
- Create and run your first console app
**Initialize code**
```sh
    mkdir hello

    cd hello

    dotnet new
```
**Run app**
```sh
    dotnet restore

    dotnet run
```
*For this section it's a good idea to have [Vs Code](https://code.visualstudio.com/) installed or you can open it in notepad.  You can download it [here](https://code.visualstudio.com/).*

- Take a look at the code in Vs Code 

```sh
    code .
```
####Edit Code 

*Option 1: Consider updating program.cs to accept input* 
```sh
    public static void Main(string[] args)
        {
            string myname;
            
            Console.WriteLine("What's your names");
            myname = Console.ReadLine();

            Console.WriteLine("Hello {0} thanks for using this material", myname);
            
        }
```
*Option 2: Hello World Console to Hello World Web*

- Add the Kestrel HTTP Server package as  dependency in the project.json file
```sh
    {
        "version": "1.0.0-*",
        "buildOptions": {
        "debugType": "portable",
        "emitEntryPoint": true
  },
        "dependencies": {},
        "frameworks": {
        "netcoreapp1.0": {
        "dependencies": {
            "Microsoft.NETCore.App": {
            "type": "platform",
            "version": "1.0.0"
        },
        "Microsoft.AspNetCore.Server.Kestrel": "1.0.0"
      },
      "imports": "dnxcore50"
    }
  }
}
```
- Restore the packages 
```sh
    dotnet restore
```
- Add a Startup.cs file that defines the request handling logic:
```sh
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace movingtoweb
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello I'm the Web");
            });
        }
    }
}
```
- Update Program.cs to setup and start the web host 
```sh
using System;
using Microsoft.AspNetCore.Hosting;

namespace movingtoweb
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
```
- Run the app 
```sh
    dotnet run
```
- Go to  http://localhost:5000 in your browser

### Extra 
Consider showing [dotnet watch](https://docs.asp.net/en/latest/tutorials/dotnet-watch.html?highlight=dotnet%20watch).
- Open project.json file and add the Microsoft.DotNet.Watcher.Tools see the below 

```
    "tools":{
         "Microsoft.DotNet.Watcher.Tools": "1.0.0-preview2-final"
          },
```
 
- Restore packages
```sh
    dotnet restore
```
![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/dotnetrestorewatcher.PNG)
- Show the dotnet commands using dotnet watch for example 
```sh
    dotnet run 
    changes to 
    dotnet watch run
```
![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/dotnetrunwatch.png)


## Resources
- [dot.net](https://www.microsoft.com/net) 
- [docs.microsoft.com](https://docs.microsoft.com/)

