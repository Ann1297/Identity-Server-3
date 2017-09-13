using IdentityServer3.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityServer.Config
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "MVC Client",
                    ClientId = "mvc",
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "https://localhost:44352/"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44352/"
                    },

                    AllowAccessToAllScopes = true
                },

                new Client
                {
                    Enabled = true,
                    ClientId = "mvc_resourceowener",
                    ClientName = "MVC Client (Resource Owener Password Credentials)",
                    Flow = Flows.ResourceOwner,

                    AllowedScopes = new List<string>
                    {
                        "openid"
                    },

                    ClientSecrets = new List<Secret>()
                    {
                        new Secret("supersecret".Sha256())
                    },

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 3600,

                    // refresh token settings
                    //AbsoluteRefreshTokenLifetime = 86400,
                    //SlidingRefreshTokenLifetime = 43200,
                    //RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    //RefreshTokenExpiration = TokenExpiration.Sliding
                }
            };
        }
    }
}