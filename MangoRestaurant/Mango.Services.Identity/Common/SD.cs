﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Mango.Services.Identity.Common
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };


        public static IEnumerable<ApiScope> ApiScope =>
            new List<ApiScope>
            {
                    new ApiScope("Mango", "Mango Server"),
                    new ApiScope( name: "read", displayName: "Read your data"),
                    new ApiScope( name: "write", displayName: "Write your data"),
                    new ApiScope( name: "delete", displayName: "Delete your data")
            };

        public static IEnumerable<Client> client =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = {"read","write","profile"}
                },

                new Client
                {
                    ClientId = "mango",
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "http://localhost:40390/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:40390/signout-callback-oidc" },
                    AllowedScopes =  new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                         IdentityServerConstants.StandardScopes.Email,
                         "mango"
                    }
                },

            };
    }
}



