namespace Personal.Movie.Database.Model.General
{
    public enum ResponseStatusEnum
    {
        Success = 1,
        DatabaseError = 2,
        WCFServerError = 3,
        AuthenticationFail = 4,
        AuthorizationFail = 5,
        APIError = 6,
        BadRequest = 7
    }
}
