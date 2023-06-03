# Authorization Server using IdentityServer4

[![Build status][ci-badge]][ci-status]

Authorization Server using IdentityServer4 4.1.2 on ASP.NET Core 3.1.

This is my attempt at improving my understanding of modern authentication, that is, OpenID Connect & Oauth 2.0
and how these two protocols are implemented in IdentityServer4 4.1.2 on ASP.NET Core 3.1

## Prerequisites

- Visual Studio 2019 or greater
- ASP.NET Core 3.1

## Getting started

1. Clone the project.
1. Open the solution file `AuthorizationServer.sln` on Visual Studio.
1. Press F5 to start the build and run the application.

## License

[MIT License](LICENSE)

Copyright &copy; 2022 Felipe Romero

## Appendix

### How to manually set Kestrel's HTTPS configuration with a development certificate file

Export ASP.NET Core's development certificate (the one that gets automatically created when you install .NET)
to `%APPDATA%\ASP.NET\https\AuthorizationServer.pfx` (notice the filename is the same as your executing assembly).

```ps1
dotnet dev-certs https -ep "$Env:APPDATA\ASP.NET\https\AuthorizationServer.pfx" -p '12345'
```

Add the following setting to your `appsettings.json` file. 
The password must match the password used for the exported certificate in the previous step.

```json
"Kestrel": {
    "Certificates": {
        "Development": {
            "Password": "12345"
        }
    }
}
```

### Sources

I'm taking guidance from these sources to implement IdentityServer4 with ASP.NET Core Identity.

- [scottbrady91.com](https://www.scottbrady91.com/identity-server/getting-started-with-identityserver-4)
- [github.com/scottbrady91/IdentityServer4-Example](https://github.com/scottbrady91/IdentityServer4-Example/blob/master/IdentityProvider)
- [technet.microsoft.com](https://social.technet.microsoft.com/wiki/contents/articles/37169.net-core-secure-your-web-applications-using-identityserver-4.aspx)
- [damienbod/IdentityServer4AspNetCoreIdentityTemplate](https://github.com/damienbod/IdentityServer4AspNetCoreIdentityTemplate/tree/release_5_1_3)

[ci-status]: https://github.com/feliperomero3/AuthorizationServer/actions/workflows/AuthorizationServer-CI.yml
[ci-badge]: https://github.com/feliperomero3/AuthorizationServer/actions/workflows/AuthorizationServer-CI.yml/badge.svg