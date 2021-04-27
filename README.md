# episerver-team-01
Practice EPIServer



Installation
------------

1.  Configure Visual Studio to add this package source: http://nuget.episerver.com/feed/packages.svc/. This allows missing packages to be downloaded, when the solution is built.
2.  Open solution and build to download nuget package dependencies.
3.  Search the solution for "ChangeThis" and review/update as described.
4.  Import cms.bacpac & commerce.bacpac or Run Setup\SetupDatabases.cmd to create the databases *. In the unlucky event of errors please check the logs.  
5.  Start the site (Debug-Start from Visual studio) and browse to http://localhost:50244 to finish installation. Login with admin@example.com/store.

*By default SetupDatabases.cmd use the default SQL Server instance. Change this line `set sql=sqlcmd -S . -E` by replacing `.` with the instance name to use different instance.

Note: SQL scripts are executed using Windows authentication so make sure your user has sufficient permissions.

Styling
-------

The styling of the site is done in [less](http://lesscss.org/). In order to be able to recompile the less files to css you will need to
install [nodejs](https://nodejs.org/). If you have nodejs the less files will be recompiled into css on every build. From the command line
you can also execute the following command in folder "Sources\EPiServer.Reference.Commerce.Site\":

```
msbuild -t:BuildLessFiles
```

Compiling the razor views
-------------------------

If you want to build the views to validate their correctness you can set the MvcBuildViews parameter to true.

```
msbuild -p:MvcBuildViews=true
```


SQL Server authentication
-------------------------

If you don't have mixed mode authentication enabled you can edit this line in SetupDatabases.cmd and provide username and password.

```
set sql=sqlcmd -S . -U username -P password
```