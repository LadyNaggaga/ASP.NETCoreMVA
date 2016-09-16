# Module 3: Introduction to Routing & MVC 

### Install the Routing Package 

- Install the Microsoft.AspNetCore.Routing Package
- Open a new ASP.NET Core application  project.json file and add the line below as dependency 
```sh
    "dependencies": {
  ...,
  "Microsoft.AspNetCore.Routing": "1.0.0"
}
```

### Capture and Use Data 

## Introduction to MVC 

**[What is the MVC pattern?](https://docs.asp.net/en/latest/mvc/overview.html)**

![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/MVC.png)

*The illustration above is a simple explanation of the  MVC pattern.*

When a you the visits a website the following things happen 
- You request a to view a page by entering a URL. 
- The **Controller** recieves the page request. 
- **Controller** sends the request to the **Model** to retrieve all the requested data.
- The **Model** stores and packages the data to be presented to you in the **View**
![Alt Text](https://github.com/LadyNaggaga/ASP.NETCoreMVA/blob/master/Images/Pattern.png)




