using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Personal.Movie.Database.HttpRequestHelper.UnitTest
{
    public class ManageRequestUnitTest
    {
        // Test ManageRequest.HttpPost
        [Fact]
        public void HttpPostPassingTest()
        {
            CredentialInfo credentialInfo = new CredentialInfo()
            {
                identityServerURL = ConnectionConst.IDENTITYSERVERURL,
                identityServerClientID = ConnectionConst.IDENTITYSERVERCLIENTID,
                identityServerClientSecrets = ConnectionConst.IDENTITYSERVERCLIENTSECRETS,
                identityServerScopes = ConnectionConst.IDENTITYSERVERSCOPES,
                identityServerUsername = ConnectionConst.IDENTITYSERVERUSERNAME,
                identityServerPassword = ConnectionConst.IDENTITYSERVERPASSWORD
            };

            ManageRequest manageRequest = new ManageRequest(credentialInfo, ConnectionConst.APISERVERURL);

            Dictionary<string, string> ValidateUserNameAndPasswordInput = new Dictionary<string, string>() {
                { "userName", "test_f"},
                { "userPasswordHash", "bb96c2fc40d2d54617d6f276febe571f623a8dadf0b734855299b0e107fda32cf6b69f2da32b36445d73690b93cbd0f7bfc20e0f7f28553d2a4428f23b716e90"}
            };

            HttpContent ValidateUserNameAndPasswordInputContent = new StringContent(
                JsonConvert.SerializeObject(ValidateUserNameAndPasswordInput), Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = manageRequest.HttpPost(ConnectionConst.POSTREQUESTURL, 
                ValidateUserNameAndPasswordInputContent).Result;

            Assert.Equal(httpResponse.StatusCode, HttpStatusCode.OK);
        }
    }
}
