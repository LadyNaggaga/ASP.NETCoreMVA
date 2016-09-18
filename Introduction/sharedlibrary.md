#Module 2.1: Multi project solutions/ Share Library 

*Notes*
- Make the folder structure like this:
	- Global.json
	- Src\Foo.Web
	-  Src\Foo
-  Make a new class library in Src\Foo
-  Add a "Reference" in project.json to "Foo": "1.0.0"
-  Add a function to a class in Foo and call it from Foo.Web
-  Dotnet pack - make a nuget
-  Make a local folder for nugets
-  Reference Maria.Library from there, not the source.
