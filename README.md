# cryptoexch

a classroom example of a web site using ASP.NET and EF to showcase adding, removing and retrieving data from a database with SQL and LINQ

## requirements
- VS2019 (+ ASP.NET and web development workload)
- SQL Server Management Studio
- you may also need to upgrade the MSSQLLocalDB instance that comes with VS2019.

## setting up
1. create an `App_Data` folder on `C:`
2. run `RecreateDB.sql` in SSMS or equivalent
3. move the `App_Data` folder to the project folder
4. run `Update-Package -Reinstall` in the Nuget Package Manager Console
5. compile and run

## logging in
To log in as adminstrator, use `Admin` and `Pa$$word`. To log in as a pre-created user, use `billg` and `password12345!`. You can also create a new account and log in with that.
