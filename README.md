# AlbumApp-NG-Net452
This is a demo online Music Store named Album Music.
It is a SOA project built with Visual Studio 2015.
It uses .NET 4.5.2, WCF, MVC, WebApi, WPF, AngularJS and UI-Grid, Entity Framework and SQL Server.
It includes Unit tests using Moq and xUnit.

There are two UI’s:
 - A customer facing Website built with MVC and using both AngularJS SPA and Razor views.    
 - A backend Desktop Administration app built with WPF.
 
## Setup 
#### Security 
Create a User Group ‘AlbumAppAdmin’ and add your desktop user to it.

Create a User ‘AlbumAppUser’, if you host the AlbumApp.Web in IIS, use ‘AlbumAppUser’
as the AppPool identity for the website.

#### Build
Build the Solution.  (The AlbumApp.Web.Tests build fails, just ignore).
 
Right click on the Solution and Set the Startup Projects to Multiple.
 - Set the AlbumApp.ServiceHost.Console to Start.
 - Set the AlbumApp.Web and the AlbumApp.Admin projects to Start as desired.

## Database
Update the ‘Data Source’ for your SQL Server setup in the .config files for these projects.
 - AlbumApp.Tests
 - AlbumApp.ServiceHost.Console
 - AlbumApp.Data
 - AlbumApp.Web
       connectionString="Data Source='Your SQL Server name here'”

Open the ‘Package Manager Console’ and set ‘Default Project’ to ‘AlbumApp.Data’.

Run the following command, this will create the Database and populate it with test data.

     update-database –verbose
	 
## Testing
For Testing run the AlbumApp.ServiceHost.Console.exe from a Command Prompt.

If Test Explorer can’t find the tests delete this file:

     C:\Users\(yourusername)\AppData\Local\Temp\VisualStudioTestExplorerExtensions\xunit.runner.visualstudio.2.3.1
     
A few tests may fail on the 'Run All' (If get build error, stop the ServiceHost, Rebuild the Solution and then Restart the ServiceHost).  Run just the failed tests (might need a couple runs) and they should pass.

Stop the ServiceHost before starting the application.

## Login
Password for all Web users is

     P@$$w0rd

Users are:
 - jsbach@gmail.com
 - jrmorton@gmail.com
 - cschuman@gmail.com
 - bbritten@gmail.com
 - eelias@gmail.com.

## Miscellaneous
In the Administration Console in the Maintain Albums tab to test the delete function
add an album and then delete it.  CRUD functionality for tracks and cart items has not been implemented.

There are screenshots available in the Screenshots folder in the root of the Solution.
