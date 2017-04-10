using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Personal.Movie.Database.HttpRequestHelper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Movie.Database.Helpers
{
    internal class PersonalMovieDatabaseAPIHandler
    {
        internal async static Task<string> PostReqestHandler(string postRequestURL, Dictionary<string, string> postRequestInput) {
            try {
                CredentialInfo credentialInfo = new CredentialInfo()
                {
                    identityServerURL = Startup.Configuration.GetSection("URL").GetSection("IdentitySeverURL").Value,
                    identityServerClientID = Startup.Configuration.GetSection("IdentityServerCredential").GetSection("IdentityServerClientID").Value,
                    identityServerClientSecrets = Startup.Configuration.GetSection("IdentityServerCredential").GetSection("IdentityServerClientSecrets").Value,
                    identityServerScopes = Startup.Configuration.GetSection("IdentityServerCredential").GetSection("IdentityServerScopes").Value,
                    identityServerUsername = Startup.Configuration.GetSection("IdentityServerCredential").GetSection("IdentityServerUsername").Value,
                    identityServerPassword = Startup.Configuration.GetSection("IdentityServerCredential").GetSection("IdentityServerPassword").Value
                };

                ManageRequest manageRequest = new ManageRequest(credentialInfo,
                    Startup.Configuration.GetSection("URL").GetSection("APIURL").Value);

                HttpContent postRequestInputContent = new StringContent(
                    JsonConvert.SerializeObject(postRequestInput), Encoding.UTF8, 
                    "application/json");

                HttpResponseMessage httpResponse = await manageRequest.HttpPost(postRequestURL,
                    postRequestInputContent);
                if (httpResponse.StatusCode == HttpStatusCode.OK)
                {
                    string responseJsonString = await httpResponse.Content.ReadAsStringAsync();
                    JObject responseJsonObject = JObject.Parse(responseJsonString);
                    string responseData = responseJsonObject.GetValue("responseResults").ToString();
                    return responseData;
                }
                else {
                    return null;
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
    }
}
