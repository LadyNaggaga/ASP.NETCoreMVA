# Module 1: Getting Started with ASP.NET Core 

*Module goals: Introduce your audience to .NET Core and the available tools and resources.*
*Spend time on the .NET CLI show new,run,and watch*

##Install the .NET Core SDK 
1. Go to https://dot.net and follow the instructions to download and install the .NET Core SDK for your OS

### Setting up on different environments
[**OS X**](https://www.microsoft.com/net/core#macos)

- Install Pre-requisites : [Homebrew](http://brew.sh/) and [OpenSSL](https://www.openssl.org/)
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

    dotnet new console
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
            string name;
            
            Console.WriteLine("What's your name?");
            name = Console.ReadLine();

            Console.WriteLine($"Hello {name} thanks for using this material");
            
        }
```
*Option 2: Hello World Console to Hello World Web*

- Let's introduce ASP.NET Core to our application as a  dependency in the csproj file.
*Edit by hand*
```sh
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>netcoreapp1.0</TargetFramework>
    <ItemGroup>
        <PackageReference Include= "Microsoft.AspNetCore" Version="1.0.0" />
</Project>
   ```
*Add package using dotnet cli*
- You can also add a package using the dotnet CLI
`dotnet add package Microsoft.AspNetCore`
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
                return context.Response.WriteAsync("Hello Web!");
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
Consider showing [dotnet watch](https://docs.microsoft.com/en-us/aspnet/core/tutorials/dotnet-watch).
- Open csproj file and add the Microsoft.DotNet.Watcher.Tools see the below 

```
    <DotNetCliToolReference Include="Microsoft.DotNet.Watcher.Tools" Version="*" />
```
 
- Restore packages
```sh
    dotnet restore
```
- Show the dotnet commands using dotnet watch for example 
```sh
    dotnet run 
    changes to 
    dotnet watch run
```


## Resources
- [dot.net](https://www.microsoft.com/net) 

- [docs.microsoft.com](https://docs.microsoft.com/)

