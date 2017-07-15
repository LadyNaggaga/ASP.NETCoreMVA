# Module 5: APIs and MVC Core 

Module Goal: Build a simple API that returns JSON and XML.  

## Prerequisites

- Download [POSTman](https://www.getpostman.com/) OR [Fiddler](http://www.telerik.com/fiddler) 

## Setting up the MVC API project

1. In Visual Studio File-> New Project -> .NET Core -> ASP.NET Core Web Application -> Web API

![image](https://cloud.githubusercontent.com/assets/2546640/23528014/0cac4536-ff4d-11e6-8fb6-2af22ce4d3e7.png)

Or you can use the dotnet CLI 

`dotnet new webapi ` 


**Add a Model**

2. Create a folder called `Models` and create a class called `Products` in that folder:

  ```C#
  namespace Products.Models
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
}

  ```
**Add a Controller**
3. In the Controller folder create a class called `ProductsController`.
4. Add an attribute route `[Route("/api/[controller]")]` to the `ProductsController` class:

  ```C#
  [Route("/api/[controller]")]
  public class ProductsController
  {

  }
  ```
  
5. Add a `Get` method to `ProductsController` that returns a `string` "Hello API World" with an attribute route

  ```C#
  [Route("/api/[controller]")]
  public class ProductsController
  {
    [HttpGet]
    public string Get() => "Hello World";
  }
  ```

6. Run the application and navigate to `/api/products`, it should return the string "Hello World".

## Returning JSON from the controller

1. Add the `Microsoft.AspNetCore.Mvc.Formatters.Json` 

*To edit your csproj in Visual Studio: Right click on your application name and select edit csproj*

![image](https://cloud.githubusercontent.com/assets/2546640/23097477/d0004d9c-f602-11e6-89b3-a898ed01c931.PNG)

Option 1: Edit by hand 

  ```XML
  <ItemGroup>
  ......
    <PackageReference Include="Microsoft.AspNetCore.Mvc.Formatters.Json" Version="1.1.1" />
    ....
  </ItemGroup>
  ```
 Option 2: Use NuGet package manager: Search for `Microsoft.AspNetCore.Mvc.Formatters.Json` 
![image](https://cloud.githubusercontent.com/assets/2546640/23097484/f721881e-f602-11e6-8539-e4d6b9f1626f.PNG)

2. Configure MVC to use the JSON formatter by changing the `ConfigureServices` in `Startup.cs` to use the following:

  ```C#
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddMvcCore()
          .AddJsonFormatters();
  }
  ```

3. Add a static list of projects to the `ProductsController`:

  ```C#
  public class ProductsController : ControllerBase
  {
      private static List<Product> _products = new List<Product>(new[] {
          new Product() { Id = 1, Name = "Green Peppers" },
          new Product() { Id = 2, Name = "Tacos" },
          new Product() { Id = 3, Name = "Chipotle Sauce" },
      });
      ...
  }
  
  ```
4. Change the `Get` method in `ProductsController` to return `IEnumerable<Product>` and return the list of products.

  ```C#
  public IEnumerable<Product> Get()
  {
      return _products;
  }
  ```

5. Run the application and navigate to `/api/products`. You should see a JSON payload of with all of the products.
6. Make a POST request to `/api/products`, you should see a JSON payload with all products.
7. Restrict the `Get` method to respond to GET requests by adding an `[HttpGet]` attribute to the method:

  ```C#
  [HttpGet]
  public IEnumerable<Product> Get()
  {
      return _products;
  }
  ```

## Add a method to Get a single product

1. Add a `Get` method to the `ProductsController` that takes an `int id` parameter and returns `Product`.

  ```C#
  public Product Get(int id)
  {
      return _products.SingleOrDefault(p => p.Id == id);
  }
  ```

2. Add an `HttpGet` route specifying the `id` as a route parameter:

  ```C#
  [HttpGet("{id}")]
  public Product Get(int id)
  {
      return _products.SingleOrDefault(p => p.Id == id);
  }
  ```

3. Run the application, and navigate to `/api/products/1`, you should see a JSON response for the first product.
4. Navigate to `/api/products/25`, it should return a 204 status code.
5. Change the `Get` method in the `ProductsController` to return a 404 if the product search returns null.

  ```C#
  [HttpGet("{id}")]
  public IActionResult Get(int id)
  {
      var product = _products.SingleOrDefault(p => p.Id == id);

      if (product == null)
      {
          return NotFound();
      }

      return Ok(product);
  }
  ```
6. Run the application and navigate to `/api/products/40` and it should return a 404 status code.

## Adding to the list of products

1. Add a `Post` method to `ProductsController` the takes a `Product` as input and adds it to the list of products:

  ```C#
  public void Post(Product product)
  {
      _products.Add(product);
  }
  ```

1. Add an `[HttpPost]` attribute to the method to constrain it to the POST HTTP verb:

  ```C#
  [HttpPost]
  public void Post(Product product)
  {
      _products.Add(product);
  }
  ```
  
1. Add a `[FromBody]` to the `product` argument:

  ```C#
  [HttpPost]
  public void Post([FromBody]Product product)
  {
      _products.Add(product);
  }
  ```

1. Run the application and post a JSON payload with the `Content-Type` header `application/json` to `/api/products`:

  ```JSON
  {
     "Id" : "4",
     "Name": "4K Television"
  }
  ```

1. Make a GET request to `/api/products` and the new entity should show up in the list.
1. Change the `Post` method to return an `IActionResult` with a 201 status code and the added `Product`:

  ```C#
  [HttpPost]
  public IActionResult Post([FromBody]Product product)
  {
      _products.Add(product);
      return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
  }
  ```
  
1. Add another product to the list by posting to `/api/products`:

  ```JSON
  {
    "Id": "5",
    "Name": "Radio"
  }
  ```
  
1. It should return a 201 and the `Product` that was added as JSON.

## Add model validation

1. Add the `Microsoft.AspNetCore.Mvc.DataAnnotations` package to `csproj`:

  
1. In `Startup.cs` add a call to `AddDataAnnotations()` chained off the `AddMvcCore` method in `ConfigureServices`:

  ```C#
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddMvcCore()
          .AddJsonFormatters()
          .AddDataAnnotations();
  }
  ```
1. Modify the `Product.cs` file and add a `[Required]` attribute to the name property:

  ```C#
  public class Product
  {
      public int Id { get; set; }
  
      [Required]
      public string Name { get; set; }
  }
  ```

1. In `ProductsController.cs` modify the `Post` method and add a `ModelState.IsValid` check. If the model state is not valid, return a 400 response to the client:

  ```C#
  [HttpPost]
  public IActionResult Post([FromBody]Product product)
  {
      if (!ModelState.IsValid)
      {
          return BadRequest();
      }
      
      _products.Add(product);
      return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
  }
  ```

1. POST an empty JSON payload to `/api/products` and it should return a 400 response:

  ```JSON
  {}
  ```

1. Modify the `Post` method to return the validation errors to the client by passing the `ModelState` object to the `BadRequest` method:

  ```C#
  [HttpPost]
  public IActionResult Post([FromBody]Product product)
  {
      if (!ModelState.IsValid)
      {
          return BadRequest(ModelState);
      }
      
      _products.Add(product);
      return CreatedAtAction(nameof(Get), new { id = product.Id }, product);
  }
  ```
  
1. POST an empty JSON payload to `/api/products` and it should return a 400 response with the validation errors formatted as JSON.

## Adding XML support

1. Add the `Microsoft.AspNetCore.Mvc.Formatters.Xml` package to `csproj`:


2. In `Startup.cs` add a call to `AddXmlDataContractSerializerFormatters()` chained off the `AddMvcCore` method in `ConfigureServices`:

  ```C#
  public void ConfigureServices(IServiceCollection services)
  {
      services.AddMvcCore()
          .AddJsonFormatters()
          .AddXmlDataContractSerializerFormatters();
  }
  ```
3. Run the application and make a request to `/api/products` with the accept header `application/xml`. The response should be an XML payload of the products.

## Restrict the ProductsController to be JSON only

1. Add a `[Produces("application/json")]` attribute to the `ProductsController` class:

  ```C#
  [Produces("application/json")]
  [Route("/api/[controller]")]
  public class ProductsController : ControllerBase
  ```


## Extra
- Add model validation when the product has a missing Name (and return that back to the client)
- Make the JSON properties camel case
- Write a custom output formatter to prints the product name as plain text
- Replace the static list of products with entity framework in memory store
- Replace the static list with entity framework + sqlite
