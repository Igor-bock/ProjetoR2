using IdentityServer4.Test;

namespace rei_identityserver;

public static class InMemoryConfiguracao
{
    public static IEnumerable<IdentityResource> CM_IdentityResources()
        => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Address(),
            new IdentityResource("roles", "Cargos dos usuários", new List<string> { "role" }),
            new IdentityResource("empresas", "Nome da empresa", new List<string>{ "empresa" })
        };

    public static List<TestUser> CM_Usuarios()
        => new List<TestUser>
        {
            new TestUser
            {
                SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                Username = "adm",
                Password = "lippert",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "admin"),
                    new Claim("family_name", "reitech"),
                    new Claim("address", "pedro cincinato borges, 761"),
                    new Claim("role", "Admin"),
                    new Claim("empresa", "vilux"),
                    new Claim("empresa", "altex"),
                    new Claim("empresa", "marine")
                }
            },
            new TestUser
            {
                SubjectId = "b9ea2f24-c481-502t-kjss-i558765419p3",
                Username = "vilux",
                Password = "py123",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "vidriocar"),
                    new Claim("family_name", "Vilux"),
                    new Claim("address", "Paraguay"),
                    new Claim("role", "Gerente"),
                    new Claim("empresa", "vilux")
                }
            },
            new TestUser
            {
                SubjectId = "v2ea2j76-n971-782y-asfg-r820659624y1",
                Username = "altex",
                Password = "rj123",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "altex"),
                    new Claim("family_name", "Altex"),
                    new Claim("address", "Rio de Janeiro"),
                    new Claim("role", "Gerente"),
                    new Claim("empresa", "altex")
                }
            },
            new TestUser
            {
                SubjectId = "r4we5j55-n963-852y-trui-r564196345t9",
                Username = "marine",
                Password = "sc123",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "marine"),
                    new Claim("family_name", "Vidro Forte"),
                    new Claim("address", "Santa Catarina"),
                    new Claim("role", "Gerente"),
                    new Claim("empresa", "marine")
                }
            }
        };

    public static IEnumerable<Client> CM_Clients()
        => new List<Client>
        {
            new Client
            {
                ClientId = "reitech",
                ClientSecrets = new[] { new Secret("tocoYmevoy".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "esperanto" }
            },
            new Client
            {
                ClientName = "Esperanto",
                ClientId = "rei_esperanto",
                AllowedGrantTypes = GrantTypes.Hybrid,
                RedirectUris = new List<string>{"https://localhost:6001/signin-oidc"},
                RequirePkce = false,
                AllowedScopes =
                { 
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Address,
                    "roles",
                    "empresas",
                    "esperanto"
                },
                ClientSecrets = { new Secret("rei_segredo".Sha512()) },
                PostLogoutRedirectUris = new List<string>{ "https://localhost:6001/signout-callback-oidc" },
                RequireConsent = true
            },
            new Client
            {
                ClientId = "rei_blazor",
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RequireClientSecret = false,
                AllowedCorsOrigins = { "https://localhost:7178" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "esperanto"
                },
                RedirectUris = { "https://localhost:7178/authentication/login-callback" },
                PostLogoutRedirectUris = { "https://localhost:7178/authentication/logout-callback" }
            },
            new Client
            {
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientId = "rei_api",
                ClientSecrets =
                {
                    new Secret("api_secret".Sha256())
                },
                AllowedScopes = { "esperanto" }
            }
        };

    public static IEnumerable<ApiScope> CM_ApiScopes()
        => new List<ApiScope>
        {
            new ApiScope("esperanto", "Esperanto / Reiglass API")
        };

    public static IEnumerable<ApiResource> CM_ApiResources()
        => new List<ApiResource>
        {
            new ApiResource("esperanto", "Esperanto / Reiglass API")
            {
                Scopes = { "esperanto" }
            }
        };
}
