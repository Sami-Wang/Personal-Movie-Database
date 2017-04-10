using IdentityModel.Client;
using System;
using System.Threading.Tasks;

namespace Personal.Movie.Database.HttpRequestHelper
{
    internal class ManageToken
    {
        internal static async Task<TokenResponse> GetAccessToken(CredentialInfo credentialInfo) {
            try
            {
                var discoveryClient = new DiscoveryClient(credentialInfo.identityServerURL);
                discoveryClient.Policy.RequireHttps = false;
                TokenClient tokenClient = new TokenClient(discoveryClient.GetAsync().Result.TokenEndpoint,
                    credentialInfo.identityServerClientID, credentialInfo.identityServerClientSecrets);
                TokenResponse tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync(
                    credentialInfo.identityServerUsername, credentialInfo.identityServerPassword,
                    credentialInfo.identityServerScopes);
                return tokenResponse;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
    }
}
