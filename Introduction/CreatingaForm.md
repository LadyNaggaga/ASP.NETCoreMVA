# Creating a Form 

*Module goal: In this module we are taking the audience how to build a simple CRUD (Create Read Update Delete)application. During  the module will be introduced to the following:*
- *Learn how to create a Form with ASP.NET Core*
- *Overview of the to ASP.NET Core Web Application template*
- *Introduction to [Web API](https://docs.asp.net/en/latest/tutorials/first-web-api.html)*

## Create a new web Application
- Open up Visual Studio
- Create a new ASP.NET Core application 

    Go to File New Project ->.NETCore -> ASP.NET Core Web Application (.NET Core)
    ![image](https://cloud.githubusercontent.com/assets/2546640/23097413/12b3d5de-f601-11e6-83e7-548dddd63159.png)
   
- Enter the name MusicStore
- Select the Web Application template
![image](https://cloud.githubusercontent.com/assets/2546640/23229530/a1022f06-f90e-11e6-8a57-5e6861c53d8a.PNG)
- Click Change authentication and Select Individual User Accounts
![image](https://cloud.githubusercontent.com/assets/2546640/23229622/f24133a8-f90e-11e6-8fae-a1652cab2478.PNG)


**This can also be done in the commandline with**
  
    ```
    dotnet new mvc
    ```
### Create a Model 
Make a list of Music albums
- Right click on the Model folder --> Add --> Class --> albums
```C#
  public class albums
    {
        public int ID { get; set; }
        public String Artist { get; set; }
        public String Album { get; set; }
        public String Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public decimal Price { get; set; }
    }
```
### Create a controller 
- Right click on the Controller folder --> Add --> Controller 
- Select the MVC Controller with Views, using Entity Framework 

![image](https://cloud.githubusercontent.com/assets/2546640/23244833/09e91150-f956-11e6-9c4b-ddca1961fe65.PNG)

- Select your Model Class : albums(MusicStore.Models)

![image](https://cloud.githubusercontent.com/assets/2546640/23244907/80064204-f956-11e6-8b25-1b84e6227c98.PNG)

- Select Data Context class: ApplicationDbContext(MusicStore.Data)

![image](https://cloud.githubusercontent.com/assets/2546640/23244967/ea4259a0-f956-11e6-83f2-5457b0cd248a.PNG)

- Now, you should that this has scaffolded a new AlbumsControllers and 5 new Views

*Albums Controller*

![image](https://cloud.githubusercontent.com/assets/2546640/23245055/b878ec6c-f957-11e6-889a-372ea2e4bdf3.PNG)

*Albums Views*

![image](https://cloud.githubusercontent.com/assets/2546640/23245066/bff368c8-f957-11e6-97d7-c5fead3e9b78.PNG)

- Run the application and navigate to /albums

*Should see the errror below*

![image](https://cloud.githubusercontent.com/assets/2546640/23279240/72048792-f9e2-11e6-89de-0e02b16e1a78.PNG)

**Add Database**

*Option 1: Use Visual Studio*

In Visual Studio, use the Package Manager Console to scaffold a new migration for these changes and apply them to the database:

>PM> Add-Migration [migration name]

>PM> Update-Database 

*Option 2: Use dotnet CLI*
> dotnet ef migrations add [migration name] 

> dotnet ef database update 

- Run your application again and go to /albums

![image](https://cloud.githubusercontent.com/assets/2546640/23284042/4e6c78de-f9f7-11e6-97ed-67e2fc0b6431.PNG)

 - Click Create New 

 ![image](https://cloud.githubusercontent.com/assets/2546640/23285222/d7667f26-f9fd-11e6-878d-84278f40ecca.PNG)
