using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServerDemo;

public class IdentityConfiguration
{
    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("myApi")
            {
                Scopes = new List<string>{ "myApi.read","myApi.write" },
                ApiSecrets = new List<Secret>{ new Secret("supersecret".Sha256()) }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("myApi.read"),
            new ApiScope("myApi.write"),
        };

    //Faz busca de clientes em uma base de dados(appsettings) pois o método AddInMemoryClients só é chamado nesse momento
    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                //Uma aplicação client pode ter mais do que um ClientId
                ClientId = "cwm.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "myApi.write" }
            },
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static List<TestUser> TestUsers =>
        new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "1144",
                Username = "macoratti",
                Password = "numsey",
                Claims =
                {
                    new Claim(JwtClaimTypes.Name, "Macoratti Net"),
                    new Claim(JwtClaimTypes.GivenName, "Macoratti"),
                    new Claim(JwtClaimTypes.FamilyName, "Net"),
                    new Claim(JwtClaimTypes.WebSite, "http://macoratti.net"),
                }
            }
        };
}