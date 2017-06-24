# HaloLive.ServiceDiscovery.Service

Repository of the ASP Core Web API application that provides service discovery for the HaloLive backend.. If you're looking for information or documentation consult the [Documentation Repo](https://github.com/HaloLive/Documentation) that contains design diagrams, endpoint and request/response documentation and information about much more.

See the section on Service Discovery specifically.

## Setup

To use this project you'll first need a couple of things:

* Visual Studio 2017
* Netcore 1.1
* Nuget Feed: https://www.myget.org/F/halolive/api/v3/index.json

## Build

To build the service you can run the Batch script called [build.bat](https://github.com/HaloLive/HaloLive.ServiceDiscovery.Service/blob/master/build.bat) or manually publish it in visual studio.

Both will successfully build the application.

## Running

To run the application you can use the [run.bat](https://github.com/HaloLive/HaloLive.ServiceDiscovery.Service/blob/master/run.bat), assuming you built it with [build.bat](https://github.com/HaloLive/HaloLive.ServiceDiscovery.Service/blob/master/build.bat) and put it in the build directory, or you can run the following command in the console in the publish directory:

```
dotnet HaloLive.ServiceDiscovery.Application.dll
```

## Configuration

In the future endpoints entries may move to a MySQL database. Right now they're located in the file store serialized in JSON.
The current endpoint loading system expects all endpoints to be in a folder called **Endpoints** relative to the running application. The build script will create this folder for you.

It expects endpoint files to be named **Endpoints{Region}.json** where the {Region} is a region string like *US* or *CN*. Below is an example file for the EndpointsUS.json file.

```
{
	"Region": "US",

	"ServiceEndpoints":
	{
		"AuthenticationService" : { "EndpointAddress": "127.0.0.1", "EndpointPort": 80 }
	}
}
```

## Tests

TODO: The current appveyor tags are to HaloLive.Library

|    | Windows .NET Debug |
|:---|------------------:|
|**master**| [![Build status](https://ci.appveyor.com/api/projects/status/rinvn2tdxn0yinf4?svg=true)](https://ci.appveyor.com/project/HelloKitty/halolive-library) |
|**dev**| [![Build status](https://ci.appveyor.com/api/projects/status/rinvn2tdxn0yinf4/branch/dev?svg=true)](https://ci.appveyor.com/project/HelloKitty/halolive-library/branch/dev) |
