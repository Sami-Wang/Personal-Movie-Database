namespace Personal.Movie.Database.HttpRequestHelper.UnitTest
{
    public class ConnectionConst
    {
        internal const string IDENTITYSERVERURL = "http://wyh-identityserver.azurewebsites.net/";

        internal const string IDENTITYSERVERCLIENTID = "1";

        internal const string IDENTITYSERVERCLIENTSECRETS = "UI00..";

        internal const string IDENTITYSERVERSCOPES = "ManageUser";

        internal const string IDENTITYSERVERUSERNAME = "PersonalMovieDBUI";

        internal const string IDENTITYSERVERPASSWORD = "PersonalMovieDBUI00..";

        internal const string APISERVERURL = "http://personalmoviedatabaseapi.azurewebsites.net/api/";

        internal const string POSTREQUESTURL = "ManageUser/ValidateUserNameAndPassword";
    }
}
