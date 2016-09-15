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
- Add edit code. Consider updating program.cs to accept input 
```sh
    public static void Main(string[] args)
        {
            string myname;
            
            Console.WriteLine("What's your names");
            myname = Console.ReadLine();

            Console.WriteLine("Hello {0} thanks for using this material", myname);
            
        }
```
### Resources
- [dot.net](https://www.microsoft.com/net) 
- [docs.microsoft.com](https://docs.microsoft.com/)

