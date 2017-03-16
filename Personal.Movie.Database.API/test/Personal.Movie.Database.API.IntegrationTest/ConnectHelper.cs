using IdentityModel.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Movie.Database.API.IntegrationTest
{
    internal class ConnectHelper
    {
        internal static async Task<HttpResponseMessage> HttpGetHelper(HttpClient httpClient, string apiUrl)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var client = new DiscoveryClient("http://wyh-identityserver.azurewebsites.net/");
                client.Policy.RequireHttps = false;
                var tokenClient = new TokenClient(client.GetAsync().Result.TokenEndpoint, "2", "Swagger00..");
                //var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("PersonalMovieDBUI",
                //    "PersonalMovieDBUI00..", "ManageUser");
                var tokenResponse = await tokenClient.RequestClientCredentialsAsync("ManageUser");
                httpClient.SetBearerToken(tokenResponse.AccessToken);
                httpResponse = await httpClient.GetAsync("/api/" + apiUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                httpResponse.StatusCode = HttpStatusCode.InternalServerError;
            }
            return httpResponse;
        }

        internal static async Task<HttpResponseMessage> HttpPostHelper(HttpClient httpClient, string jsonString, string apiUrl)
        {
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            try
            {
                var client = new DiscoveryClient("http://wyh-identityserver.azurewebsites.net/");
                client.Policy.RequireHttps = false;
                var tokenClient = new TokenClient(client.GetAsync().Result.TokenEndpoint, "1", "UI00..");
                var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("PersonalMovieDBUI",
                    "PersonalMovieDBUI00..", "ManageUser");
                httpClient.SetBearerToken(tokenResponse.AccessToken);
                HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                httpResponse = await httpClient.PostAsync("/api/" + apiUrl, httpContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                httpResponse.StatusCode = HttpStatusCode.InternalServerError;
            }
            return httpResponse;
        }
    }
}
