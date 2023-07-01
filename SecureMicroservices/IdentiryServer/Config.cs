using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentiryServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients => 
            new Client[] 
            {
                new Client
                {
                    ClientId = "movieClient",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {new Secret("secret".Sha512()) },
                    AllowedScopes = {"movieAPI" }
                },
                new Client
                {
                    ClientId = "movies_mvc_client",
                    ClientName = "Movies MVC Web App",
                    AllowedGrantTypes = GrantTypes.Code,
                    AllowRememberConsent = false,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:5002/signin-oidc" // this is the client app port
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:5002/signout-callback-oidc"
                    },
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("secret".Sha512())
                    },
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                }
            };
        public static IEnumerable<ApiScope> ApiScopes => 
            new ApiScope[] 
            {
                new ApiScope("movieAPI", "Movie API")
            };
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] { };
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] { };
        public static IEnumerable<TestUser> TestUsers => new TestUser[] { };
    }
}
