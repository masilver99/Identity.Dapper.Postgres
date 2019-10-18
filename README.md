# Identity.Dapper.Postgres

This work is based on the [Daarto repo](https://github.com/giorgos07/Daarto). My intention is to create a solid library and sample app from which to create future web applications from using MS Identity, Dapper and Postgres.  It also serves as a great example of how to use a different data store for Identity.

It uses Dapper and Postgres, although it could be modyfied to use any database that dapper supports.  It uses standard Postgres table and field names (underscores instead of camel case).   Thankfully dapper converts bweteen underscored names and camel case class names.

I also removed the editor config file as I don't use the java style of curly bracing.  I also renamed the sample project and added a .NET Core 3.0 sample, which will be the focus of my attention going forward.  Use this project, especially the Startup.cs file to determine how to setup the library.

Much appreciation to giorgos07, as his library was the best code I found for using Dapper with MS Identity.

In the db/scripts directory are the database scripts to build the postgres database.  NOTE: If you change table or field names, you'll need to update the queries.  You can run this in any instance of Postgres.  

To make setting up a dev database easy, there is a vagrant script in the db dir.  Run this with the 'vagrant up' command and it will install a VM with Ubuntu and Postgres and build the database.  The connection string in the sample .NET 3.0 project contains the connection string to connect to it.  You will need VirtualBox and Vagrant installed to use this. 

Next, I'll work on a NuGet package to make installation easier.
