# Creating a Form 
### Watch: [Creating a Form Module here](https://mva.microsoft.com/en-US/training-courses/introduction-to-asp-net-core-1-0-16841?l=eYlqdeE6C_1006218965)
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
## Create a Form 

Let's make a few changes to the site menu, layout and homepage

Open *Views/Shared/_Layout.cshtml* and make the changes below:
- Change MusicStore to Music Store 
- Add te following entries: Artist, Album, Label, Genre, and Twitter.

```html
@inject Microsoft.ApplicationInsights.AspNetCore.JavaScriptSnippet JavaScriptSnippet
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - Music Store</title>

    <environment names="Development">
        <link rel="stylesheet" href="~/lib/bootstrap/dist/css/bootstrap.css" />
        <link rel="stylesheet" href="~/css/site.css" />
    </environment>
    <environment names="Staging,Production">
        <link rel="stylesheet" href="https://ajax.aspnetcdn.com/ajax/bootstrap/3.3.7/css/bootstrap.min.css"
              asp-fallback-href="~/lib/bootstrap/dist/css/bootstrap.min.css"
              asp-fallback-test-class="sr-only" asp-fallback-test-property="position" asp-fallback-test-value="absolute" />
        <link rel="stylesheet" href="~/css/site.min.css" asp-append-version="true" />
    </environment>
    @Html.Raw(JavaScriptSnippet.FullScript)
</head>
<body>
    <nav class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="sr-only">Toggle navigation</span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <a asp-area="" asp-controller="Home" asp-action="Index" class="navbar-brand">Music Store</a>
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li><a asp-area="" asp-controller="Home" asp-action="Index">Home</a></li>
                    <li><a asp-area="" asp-controller="Home" asp-action="About">About</a></li>
                    <li><a asp-area="" asp-controller="Home" asp-action="Index">Artist</a></li>
                    <li><a asp-area="" asp-controller="Home" asp-action="Index">Album</a></li>
                    <li><a asp-area="" asp-controller="Home" asp-action="Index">Label</a></li>
                    <li><a asp-area="" asp-controller="Home" asp-action="Index">Genre</a></li>
                    <li><a asp-area="" asp-controller="Home" asp-action="Index">Twitter</a></li>
                </ul>
                @await Html.PartialAsync("_LoginPartial")
            </div>
        </div>
    </nav>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; 2017 - Music Store</p>
        </footer>
    </div>

    <environment names="Development">
        <script src="~/lib/jquery/dist/jquery.js"></script>
        <script src="~/lib/bootstrap/dist/js/bootstrap.js"></script>
        <script src="~/js/site.js" asp-append-version="true"></script>
    </environment>
    <environment names="Staging,Production">
        <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-2.2.0.min.js"
                asp-fallback-src="~/lib/jquery/dist/jquery.min.js"
                asp-fallback-test="window.jQuery"
                crossorigin="anonymous"
                integrity="sha384-K+ctZQ+LL8q6tP7I94W+qzQsfRV2a+AfHIi9k8z8l9ggpc8X+Ytst4yBo/hH+8Fk">
        </script>
        <script src="https://ajax.aspnetcdn.com/ajax/bootstrap/3.3.7/bootstrap.min.js"
                asp-fallback-src="~/lib/bootstrap/dist/js/bootstrap.min.js"
                asp-fallback-test="window.jQuery && window.jQuery.fn && window.jQuery.fn.modal"
                crossorigin="anonymous"
                integrity="sha384-Tc5IQib027qvyjSMfHjOMaLkfuWVxZxUPnCJA7l2mCWNIpG9mGCD8wGNIcPD7Txa">
        </script>
        <script src="~/js/site.min.js" asp-append-version="true"></script>
    </environment>

    @RenderSection("Scripts", required: false)
</body>
</html>
```