# Chayns Backend API [![NuGet](https://img.shields.io/nuget/v/Chayns.Backend.Api.svg)](https://www.nuget.org/packages/Chayns.Backend.Api)  ![NuGet](https://img.shields.io/nuget/dt/Chayns.Backend.Api.svg) 
The Chayns.Backend.Api-package is dedicated to support you using the [backend api](https://github.com/TobitSoftware/chayns-backend) itself. <br>
It shortens and simplifies many features of the api and makes the usage much more cleaned up.

## Requirements
To use this project you will need the following prerequisites
+ Visual Studio
+ .NET Framework 4.5.2 or greater or
+ .Net Core 1.0 or greater

## Installation
You can install the library by using the [VisualStudio nuget-plugin](https://marketplace.visualstudio.com/items?itemName=NuGetTeam.NuGetPackageManager).
The plugin will install all dependencies.

If you already have NuGet installed you can install the package running the following command inside your project directory.

```
nuget install Chayns.Backend.Api
```

## How to use

To authenticate your request you will need your secret and TappId (you can find more information in the [chayns backend wiki](https://github.com/TobitSoftware/chayns-backend/wiki/Authorization)).<br>
You can find the secret in the tapp administration in the TSPN and the TappId can be found in the JavaScript Frontend API.

## Here are some examples:

All examples require the following variables

```CSHARP 
private const string Secret = "{your Secret}";
private const int LocationId = 1234;
private const int TappId = 1234;
```

In the first example we want to load some user-informations:

```CSHARP
UserResult GetUser(int userId)
{
    var location = new LocationIdentifier(LocationId);
    var cred = new SecretCredentials(Secret, TappId);

    var userRep = new UserRepository(cred);
    var result = userRep.GetUser(userId, location);
    
    return result.Data;
}
```

In the our second Example we want to load all our female users

```CSHARP
IEnumerable<UserResult> GetFemaleUsers()
{
    var cred = new SecretCredentials(TappSecret, TappId);
    var userGet = new UserDataGet(LocationId)
    {
        Gender = "female"
    };

    var userRep = new UserRepository(cred);
    Result<UserResult> result = userRep.GetUser(userGet);

    return result.Data;
}
```

And in our last example we generate a PageAccessToken that could be used in our frontend

```CSHARP
string GetPageAccessToken()
{
    var cred = new SecretCredentials(Secret, TappId);
    var tokenGet = new PageAccessTokenDataGet(LocationId)
    {
        Permissions = new List<string>
        {
            "PublicInfo",
            "UserInfo",
            "SeeUAC",
            "EditUAC",
            "DeviceInfo",
            "Push",
            "Intercom",
            "Email"
        }
    };
    var accesstokenRep = new AccessTokenRepository();
    
    var result = accesstokenRep.GetPageAccessToken(cred, tokenGet);
    
    return result?.Data?.PageAccessToken;
}
```