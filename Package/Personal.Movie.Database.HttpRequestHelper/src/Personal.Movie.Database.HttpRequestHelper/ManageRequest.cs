using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Personal.Movie.Database.HttpRequestHelper
{
    public class ManageRequest
    {
        public CredentialInfo credentialInfo { get; set; }
        public string apiBaseURL { get; set; }

        public ManageRequest(CredentialInfo credentialInfoNew, string apiBaseURLNew) {
            credentialInfo = credentialInfoNew;
            apiBaseURL = apiBaseURLNew;
        }

        /// <summary>
        /// Http Get Request.
        /// </summary>
        /// <param name="apiURL"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> HttpGet(string apiURL)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    client.BaseAddress = new Uri(apiBaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    TokenResponse tokenResponse = await ManageToken.GetAccessToken(credentialInfo);
                    client.SetBearerToken(tokenResponse.AccessToken);
                    response = await client.GetAsync(apiURL);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
                return response;
            }
        }

        /// <summary>
        /// Http Post Request.
        /// </summary>
        /// <param name="apiURL"></param>
        /// <param name="postContent"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> HttpPost(string apiURL, HttpContent postContent)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = new HttpResponseMessage();
                try
                {
                    client.BaseAddress = new Uri(apiBaseURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    TokenResponse tokenResponse = await ManageToken.GetAccessToken(credentialInfo);
                    client.SetBearerToken(tokenResponse.AccessToken);
                    response = await client.PostAsync(apiURL, postContent);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    response.StatusCode = System.Net.HttpStatusCode.NotFound;
                }
                return response;
            }
        }
    }
}
