using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;

namespace Forum.IdentityServer
{
    public class IdentityServerConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("ForumAPI", new[]
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Email
                })
            };


        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client_mvc",
                    ClientName = "MVC Client",
                    ClientUri = "https://localhost:44395",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RedirectUris = { "https://localhost:44395/signin-oidc" },
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    PostLogoutRedirectUris = { "https://localhost:44395/signout-callback-oidc" },
                    AllowedCorsOrigins = { "https://localhost:44395" },
                    AllowOfflineAccess = true,
                    AccessTokenLifetime = (int)TimeSpan.FromHours(5).TotalSeconds,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "ForumAPI"
                    },
                    RequireClientSecret = false,
                    RequireConsent = false
                }
            };
    }
}
