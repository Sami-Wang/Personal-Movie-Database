using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Personal.Movie.Database.API.Models.ManageUser;
using Personal.Movie.Database.Model.General;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Personal.Movie.Database.API.IntegrationTest
{
    public class ManageUserControllerIntegrationTest
    {
        private readonly TestServer testServer;
        private readonly HttpClient httpClient;

        public ManageUserControllerIntegrationTest()
        {
            testServer = new TestServer(new WebHostBuilder().UseKestrel().UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration().UseStartup<Startup>());
            httpClient = testServer.CreateClient();
        }

        // Test http://localhost:62610/api/ManageUser/ValidateUserNameAndPassword
        [Fact]
        public async Task GetsFundNamesPassingTest()
        {
            ValidateUserNameAndPasswordInput validateUserNameAndPasswordInput = new ValidateUserNameAndPasswordInput()
            {
                userName = "test_f",
                userPasswordHash = "bb96c2fc40d2d54617d6f276febe571f623a8dadf0b734855299b0e107fda32cf6b69f2da32b36445d73690b93cbd0f7bfc20e0f7f28553d2a4428f23b716e90"
            };
            string validateUserNameAndPasswordInputJsonString = JsonConvert.SerializeObject(
                validateUserNameAndPasswordInput);
            HttpResponseMessage httpResponse = await ConnectHelper.HttpPostHelper(httpClient, 
                validateUserNameAndPasswordInputJsonString, "ManageUser/ValidateUserNameAndPassword");
            Assert.Equal(httpResponse.StatusCode, HttpStatusCode.OK);
            var httpResponseContent = JObject.Parse(await httpResponse.Content.ReadAsStringAsync());
            Assert.Equal(Convert.ToInt32(httpResponseContent.GetValue("responseCode").ToString()),
                (int)ResponseStatusEnum.Success);
        }
    }
}
