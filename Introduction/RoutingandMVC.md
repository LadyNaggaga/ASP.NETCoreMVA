# Module 3: Introduction to Routing & MVC 

### Install the Routing Package 

- Create a new ASP.NET Core application 

#### Using Routing Middleware

- Open  project.json file and add the line below as dependency to install the Microsoft.AspNetCore.Routing Package
```sh
    "dependencies": {
  ...,
  "Microsoft.AspNetCore.Routing": "1.0.0"
}
```
- Open the Startup.cs file
- Add a routing services to ConfigureServices method in the Startup.cs
```sh
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }
```
*Option 1*
- In the `Configure` method, create a `RouteBuilder` with a handler for the root of the site and add it to the middleware pipeline:
  
  ``` c#
  using Microsoft.AspNetCore.Routing;
  ...
  public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
  {
      loggerFactory.AddConsole();

      if (env.IsDevelopment())
      {
          app.UseDeveloperExceptionPage();
      }

      var routeBuilder = new RouteBuilder(app);

      routeBuilder.MapGet("", context => context.Response.WriteAsync("Hello from Routing!"));

      app.UseRouter(routeBuilder.Build());
  }
  ```
  - Run the site and verify your middleware is hit via routing (Ctrl+F5)
  - Add another route that matches a sub-path:
    ``` c#
  routeBuilder.MapGet("sub", context => context.Response.WriteAsync("Hello from sub!"));
  ```
  
### Capture and Use Data 
- Add another route that captures the name of an item from the URL, e.g. "item/{itemName}", and displays it in the response:
  
  ``` c#
  routeBuilder.MapGet("item/{itemName}", context => context.Response.WriteAsync($"Item: {context.GetRouteValue("itemName")}"));
  ```
-  Run the site and verify that your new route works. Browsing to "/item/monkey" should display the message "Item: monkey".
- Modify the route to include a route constraint on the captured segmet, enforcing it to be a number:
  
  ``` c#
  routeBuilder.MapGet("item/{id:int}", context => context.Response.WriteAsync($"Item ID: {context.GetRouteValue("id")}"));
  ```
-  Run the site again and see that the route is only matched when the captured segment is a valid number. Browsing to "/item/5" will work, but browsing to "/item/monkey" will now show a missing page (HTTP 404) error.
-  Modify the router to include both versions of the route above (with and without the route constraint)
- Experiment with changing the order the routes are added and observe what affect that has on which route is matched for a given URL

## Introduction to MVC 

**[What is the MVC pattern?](https://docs.asp.net/en/latest/mvc/overview.html)**

![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/MVC.png)

*The illustration above is a simple explanation of the  MVC pattern.*

When a you the visits a website the following things happen 
- You request a to view a page by entering a URL. 
- The **Controller** recieves the page request. 
- **Controller** sends the request to the **Model** to retrieve all the requested data.
- The **Model** stores and packages the data to be presented to you in the **View**

![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/MVCPattern.png)

### Let's add MVC 
- Open `project.json` and add "Microsoft.AspNetCore.Mvc" to the `"dependencies"` section and save it:

  ``` JSON
  "dependencies": {
    ...,
    "Microsoft.AspNetCore.Mvc": "1.0.0"
  }
  ```
- Add a "Controllers" folder to your application
- Create a new class called "HomeController" in the new folder and add the following code:

``` c#
using Microsoft.AspNetCore.Mvc;

public class HomeController
{
  [HttpGet("/")]
  public string Index() => "Hello from MVC!";
}
```
- Replace the Routing middleware from the previous step with MVC services and middleware in `Startup.cs` as shown:

  ``` c#
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddMvc();
  }
  
  public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
  {
      loggerFactory.AddConsole();

      if (env.IsDevelopment())
      {
          app.UseDeveloperExceptionPage();
      }

      app.UseMvc();
  }
```

- Run the site and verify the message is returned from your MVC controller
- If you have time, try the following:
  - Change the controller to render a view instead of returning a string directly
  - Play with the `[HttpGet("/")]` attribute to change the route the action method will match





