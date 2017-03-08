namespace Personal.Movie.Database.API.Models.ManageUser
{
    public class ValidateUserNameAndPasswordInput
    {
        public string userName { get; set; }
        public string userPasswordHash { get; set; }
    }
}
