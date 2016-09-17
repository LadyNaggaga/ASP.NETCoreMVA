# Module 8: Single Page Applications with Angular 2

#### *Modules goals*
-  Emphasis the importance of SPA 
- Create a new Angular 2 application
	-  using yo aspnet core -spa 
	- Spend time to explain what a hot module reloading
## Prerequisites
- Check you have npm 3+ installed (run `npm -v` to check).
-  Install Yeoman and the aspnetcore-spa generator globally: 

  ```
  npm install -g yo generator-aspnetcore-spa
  ```
-  Install Webpack globally:

  ```
  npm install -g webpack
  ```
## Creating a new Angular 2 application
- From a commandline, create and navigate to a new empty directory.
- Create a new Angular 2 application using the "aspnetcore-spa" scaffolder:

  ```
  yo aspnetcore-spa
  ```
- View the application code by typing `code .` to launch Visual Studio Code in the current directory.

## Running the Angular 2 application
- From the commandline, set the ASP.NET Core development mode environment variable:

  ```
  set ASPNETCORE_ENVIRONMENT=Development
  ```
> **Note**: On OSX this is done using `export ASPNETCORE_ENVIRONMENT=Development`
  
1. Run the application using the `dotnet watch` tool:

  ```
  dotnet watch run
  ```
- Navigate to `http://localhost:5000` to view the application.

> **Note**: Leave the application running and the browser window open for the remainder of the lab.

## Experiment with Hot Module Reloading (HMR)
- Navigate to the Counter page in running web application.
- In Visual Studio Code, edit the Counter template (\ClientApp\components\counter\counter.html) to change the H2 heading text.
- Edit the counter implementation ((\ClientApp\components\counter\counter.ts) to change the counter increment to 10:

  ```
  export class Counter {
    public currentCount = 0;

    public incrementCounter() {
        this.currentCount+=10;
    }
  }
  ```
- Observe that the application has refreshed with your changes. View the console output to see the debug messages printed out during the updates.