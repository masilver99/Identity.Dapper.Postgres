# Identity.Dapper.Postgres

This is a fork of the Daarto repo. My intention is to create a solid library and sample app from which to create future web applications from.  It also serves as a great example of how to use a different data store for Identity.

It uses Dapper and Postgres (a real database :-) instead of SQLServer, although it could be modyfied to use any database that dapper supports.  It uses standard Postgres table and field names (underscores instead of camel case).   Thankfully dapper converts bweteen underscored names and camel case class names.

I also removed the editor config file as I don't use the java style of curly bracing.  I also renamed the sample project and added a .NET Core 3.0 sample, which will be the focus of my attention giong forward.  Use this project, especially the Startup.cs file to determine how to setup the library.

Much appreciation to giorgos07, as his library was the best code I found for using Dapper with MS Identity.

In the db/scripts directory are the database scripts to build the postgres database.  NOTE: If you change table or field names, you'll need to update the queries.  You can run this in any instance of Postgres.  

To make setting up a dev database easy, there is a vagrant script in the db dir.  Run this with the 'vagrant up' command and it will install a VM with Ubuntu and Postgres and build the database.  The connection string in the sample .NET 3.0 project contains the connection string to connect to it.  You will need VirtualBox and Vagrant installed to use this. 

Next, I'll work on a NuGet package to make installation easier.

Original Readme from Daarto:

# Daarto

The main purpose of the project is to demonstrate a custom implementation of [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) by using SQL Server and [Dapper](https://github.com/StackExchange/Dapper), in case you do not want to use the default implementation provided by Entity Framework.
The application uses ASP.NET Core 2.1 and is built by using [Visual Studio 2017 Community Edition](https://www.visualstudio.com/vs/community/) and SQL Local DB.

## Getting Started

In order to successfully run the application, the only thing you have to do is re-create the database used, in your local machine. Inside **misc/scripts** folder you will find a file called [create_database_schema.sql](https://github.com/giorgos07/Daarto/blob/master/misc/scripts/create_database_schema.sql).
Execute this `T-SQL` script in SQL Server Management Studio Query Window and all the required tables will be created automatically in a database called **DaartoDb**. The schema is the default one, which is also used by ASP.NET Core Identity. Then run the following scripts in the order displayed below, to generate some demo data (they are random and created by using a very handy online tool called [mockaroo](https://www.mockaroo.com/)).

* [generate_roles_table_data.sql](https://github.com/giorgos07/Daarto/blob/master/misc/scripts/generate_roles_table_data.sql)
* [generate_users_table_data.sql](https://github.com/giorgos07/Daarto/blob/master/misc/scripts/generate_users_table_data.sql)
* [generate_users_roles_table_data.sql](https://github.com/giorgos07/Daarto/blob/master/misc/scripts/generate_users_roles_table_data.sql)

One last step is to run a `Gulp` task in order to copy some `npm` packages in the *wwwroot/lib*. The task we need to run is called `copy:libs`

> In Visual Studio navigate to View -> Other Windows -> Task Runner Explorer to be able to run this task easily.

When running the application use the following credentials to use an administrator's account

* **email:** admin@daarto.com
* **password:** abc!37KW!

Logging in with the administrator role will give you the ability to access the admin dashboard.

> I will try to add more features as i develop the application. If you like my work, feel free to use this project as a starting point for your own applications.

## Use as Nuget package

I have published the **AspNetCore.Identity.Dapper** project as a [NuGet package](https://www.nuget.org/packages/AspNetCore.Identity.DapperOrm/2.1.0). In order to install the package use the following command.

```powershell
PM> Install-Package AspNetCore.Identity.DapperOrm
```

In order to register the [IUserStore](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.iuserstore-1) and [IRoleStore](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.irolestore-1) store implementations, you will need to use the `AddDapperStores` extension method when configuring identity in your `ConfigureServices` method.

```csharp
// Get the connection string from appsettings.json file.
var connectionString = Configuration.GetConnectionString("DefaultConnection");
// Add and configure the default identity system that will be used in the application.
services.AddIdentity<ApplicationUser, ApplicationRole>()
        .AddUserManager<UserManager<ApplicationUser>>()
        .AddRoleManager<RoleManager<ApplicationRole>>()
        .AddSignInManager<SignInManager<ApplicationUser>>()
        .AddDapperStores(connectionString)
        .AddDefaultTokenProviders();
```
