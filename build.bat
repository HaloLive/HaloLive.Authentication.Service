dotnet restore HaloLive.Authentication.Service.sln
dotnet publish src/HaloLive.Authentication.Application/HaloLive.Authentication.Application.csproj -c release

if not exist "build" mkdir build
xcopy src\HaloLive.Authentication.Application\bin\Release\netcoreapp1.1\publish build /s /y

if not exist "build\Certs" mkdir build\Certs
if not exist "build\Config" mkdir build\Config

PAUSE