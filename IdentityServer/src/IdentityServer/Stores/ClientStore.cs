using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServerDBConnection;
using System.Security.Claims;

namespace IdentityServer.Stores
{
    public class ClientStore : IClientStore
    {
        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            try
            {
                // Get Client By Client ID.
                var clientsFromDB = await IdentityServerDBHelper.GetClientByClientID(
                    Startup.Configuration.GetSection("ConnectionString").Value, Convert.ToInt32(clientId));
                if (clientsFromDB != null && clientsFromDB.Count != 0)
                {
                    Client client = new Client();
                    client.ClientId = clientsFromDB[0].clientID.ToString();
                    client.ClientName = clientsFromDB[0].clientName;
                    string clientSecrets = clientsFromDB[0].clientSecrets;
                    client.ClientSecrets = new List<Secret>{
                        new Secret(clientSecrets.Sha256())
                    };
                    string allowedGrantTypes = clientsFromDB[0].allowedGrantTypes;
                    switch (allowedGrantTypes)
                    {
                        // Resource Owner Password Grant Type
                        case "Resource Owner Password":
                            {
                                client.AllowedGrantTypes = GrantTypes.ResourceOwnerPassword;
                                break;
                            }
                        // Client Credential Grant Type
                        case "Client Credential":
                            {
                                client.AllowedGrantTypes = GrantTypes.ClientCredentials;
                                var rolesFromDB = await IdentityServerDBHelper.GetRolesByClientID(
                                    Startup.Configuration.GetSection("ConnectionString").Value,
                                    Convert.ToInt32(clientId));
                                List<Claim> claimList = new List<Claim>();
                                if (rolesFromDB != null && rolesFromDB.Count != 0)
                                {
                                    for (int i = 0; i < rolesFromDB.Count; i++)
                                    {
                                        claimList.Add(new Claim("role", rolesFromDB[i].roleName));
                                    }
                                }
                                client.Claims = claimList;
                                client.PrefixClientClaims = false;
                                break;
                            }
                    }
                    client.AccessTokenLifetime = clientsFromDB[0].accessTokenLifeTime;
                    string accessTokenType = clientsFromDB[0].accessTokenType;
                    if (accessTokenType.Equals("JWT"))
                    {
                        client.AccessTokenType = AccessTokenType.Jwt;
                    }
                    client.Enabled = clientsFromDB[0].enabled;
                    // Get Allowed Scopes By Client ID.
                    var allowedScopesFromDB = await IdentityServerDBHelper.GetAllowedScopeByClientID(
                        Startup.Configuration.GetSection("ConnectionString").Value, Convert.ToInt32(clientId));
                    if (allowedScopesFromDB != null && allowedScopesFromDB.Count != 0)
                    {
                        List<string> allowedScopes = new List<string>();
                        for (int i = 0; i < allowedScopesFromDB.Count; i++)
                        {
                            allowedScopes.Add(allowedScopesFromDB[i].scopeName);
                        }
                        client.AllowedScopes = allowedScopes;
                    }
                    return client;
                }
                else {
                    return new Client();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return new Client();
            }
        }
    }
}
