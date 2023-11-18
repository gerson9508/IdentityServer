using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace IdentityServer.Infraestructure.Repositories.Base
{
   public static class Config
   {
      public static X509Certificate2 GetCert(string path, string password) =>
         new(File.ReadAllBytes(path), password);

      public static IEnumerable<IdentityResource> GetIdentityResources() =>
          new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                  new IdentityResources.Email(),
                  new IdentityResource
                  {
                      Name = "role",
                      DisplayName="User Role",
                      Description="The application can see your role.",
                      UserClaims = new[]{JwtClaimTypes.Role,ClaimTypes.Role
                      },
                      ShowInDiscoveryDocument = true,
                      Required=true,
                      Emphasize = true
                  }
                  };

      public static IEnumerable<ApiScope> GetApiScopes() => new[]
      {
         new ApiScope("http://10.129.104.4:8081/"),
      };

      public static IEnumerable<ApiResource> GetApis() =>
          new List<ApiResource> { new ApiResource("http://localhost:8081/")
          {
                        Name = "API for APITest",
                        DisplayName = "TST_Name",
                        Description = "Checking endpoints",
                        ApiSecrets =
                        {
                           new Secret("secret_for_the_api_tst".Sha256()),
                        },
                        Enabled = true,
                        UserClaims =
                        {
                           JwtClaimTypes.Name,
                           JwtClaimTypes.Role,
                           JwtClaimTypes.Email
                        }
          }
          };

      public static IEnumerable<Client> GetClients()
      {
         return new List<Client>
         {
            new Client
            {
               ClientId = "clientid_for_the_tst",
               RequirePkce = true,
               Enabled = true,
               AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
               ClientSecrets =
               {
                  new Secret("secret_for_the_tst".Sha256()),
               },
               AccessTokenType = AccessTokenType.Jwt,
               AllowedScopes =
               {
                  "http://localhost:8081/",
                   IdentityServerConstants.StandardScopes.Address,
                   "roles"
               },
            }
         };
      }
   }
}
