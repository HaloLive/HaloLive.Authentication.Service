# HaloLive.Authentication.Service

Repository of the ASP Core Web API application that provides OAuth2 authentication for the HaloLive backend. If you're looking for information or documentation consult the [Documentation Repo](https://github.com/HaloLive/Documentation) that contains design diagrams, endpoint and request/response documentation and information about much more.

To understand how to communicate with this service consult the section on the Authentication and Authorization Scheme specifically.

## Setup

To use this project you'll first need a couple of things:

* Visual Studio 2017
* Netcore 1.1
* Nuget Feed: https://www.myget.org/F/halolive/api/v3/index.json
* Nuget Feed: https://www.myget.org/F/aspnet-contrib/api/v3/index.json

## Build

To build the service you can run the Batch script called [build.bat](https://github.com/HaloLive/HaloLive.Authentication.Service/blob/master/build.bat) or manually publish it in visual studio.

Both will successfully build the application.

## Running

To run the application you can use the [run.bat](https://github.com/HaloLive/HaloLive.Authentication.Service/blob/master/run.bat), assuming you built it with [build.bat](https://github.com/HaloLive/HaloLive.Authentication.Service/blob/master/build.bat) and put it in the build directory, or you can run the following command in the console in the publish directory:

```
dotnet HaloLive.Authentication.Application.dll
```

### Optional Arguments

**--usehttps={certPath}**: Enables SSL/HTTPS on the web host. This is actually required by the OAuth2 library as it has been configured to reject requests not using HTTPS.

**--url={customUrl}**: Starts the listener on the specified url and port. For example http://localhost:5000.

## Configuration

A test certifcate is provided.

You should have a MySQL database setup with the provided scheme in the [sql directory](https://github.com/HaloLive/HaloLive.Authentication.Service/tree/master/sql). There is a configuration file, see the example [config file](https://github.com/HaloLive/HaloLive.Authentication.Service/blob/master/src/HaloLive.Authentication.Application/Config/authserverconfig.json). You must configure the location of the certificates as well as the MySQL database connection string. This should go into the generated Build/Config folder. It's recommended certificates go into Build/Certs too.

Another important configuration option is **AuthenticationControllerEndpoint** and it is expected to be */api/auth* according to the design documents and documentation so do not adjust this unless you have good reason to.

Example authserverconfig.json below:

```
{
	"AuthConfig": 
	{
		"AuthenticationControllerEndpoint": "/api/auth",
		"JwtSigningX509Certificate2Path": "Certs/TestCert.pfx",
		"AuthenticationDatabaseString": "Server=localhost;Database=halolive;Uid=root;Pwd=test;"
	}
}
```

Example command to run the server with everything enabled:

```
dotnet HaloLive.Authentication.Application.dll --usehttps=Certs/TestCert.pfx --url=https://localhost:81
```

## Builds

TODO

## Tests

TODO: The current appveyor tags are to HaloLive.Library

|    | Windows .NET Debug |
|:---|------------------:|
|**master**| [![Build status](https://ci.appveyor.com/api/projects/status/rinvn2tdxn0yinf4?svg=true)](https://ci.appveyor.com/project/HelloKitty/halolive-library) |
|**dev**| [![Build status](https://ci.appveyor.com/api/projects/status/rinvn2tdxn0yinf4/branch/dev?svg=true)](https://ci.appveyor.com/project/HelloKitty/halolive-library/branch/dev) |
