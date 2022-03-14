# Authorization Server using IdentityServer4

[![Build status][ci-badge]][ci-status]

Authorization Server using IdentityServer4 4.1.2 on ASP.NET Core 3.1.

This is my attempt at improving my understanding of modern authentication, that is, OpenID Connect & Oauth 2.0
and how these two protocols are implemented in IdentityServer4 4.1.2 on ASP.NET Core 3.1

## Prerequisites

- Visual Studio 2019
- ASP.NET Core 3.1

## License

[MIT License](LICENSE)

Copyright &copy; 2021 Felipe Romero

## Appendix

### How to manually set Kestrel's HTTPS configuration with a development certificate file

1. Export ASP.NET Core's development certificate (that gets automatically created when you install .NET)
to `%APPDATA%\ASP.NET\https\AuthorizationServer.pfx` (notice the filename is the same as your executing assembly).
Choose the option to also export the private key using the PFX file format and take note of the password.
1. Add the following setting to your `appsettings.json` file. The password must match the password used for the certificate in the previous step.

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

I'm taking guidance from these two sources to implement IdentityServer4 with ASP.NET Core Identity.

- [scottbrady91.com](https://www.scottbrady91.com/identity-server/getting-started-with-identityserver-4)
- [github.com/scottbrady91](https://github.com/scottbrady91/IdentityServer4-Example/blob/master/IdentityProvider)
- [technet.microsoft.com](https://social.technet.microsoft.com/wiki/contents/articles/37169.net-core-secure-your-web-applications-using-identityserver-4.aspx)

[ci-status]: https://github.com/feliperomero3/AuthorizationServer/actions/workflows/AuthorizationServer-CI.yml
[ci-badge]: https://github.com/feliperomero3/AuthorizationServer/actions/workflows/AuthorizationServer-CI.yml/badge.svg