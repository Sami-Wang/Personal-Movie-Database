namespace Personal.Movie.Database.TMDbAPI
{
    internal class APIConstant
    {
        internal const string APIKEY = "b50f91529a2996a98fc75f7cf84b029b";

        internal const string TMDBBASEURL = "https://api.themoviedb.org/3/search/movie";

        internal const string SEARCHMOVIEBYTITLEURL = "?api_key=" + APIKEY + "&query=";

        internal const string IMAGESERVERURL = "https://image.tmdb.org/t/p/original";
    }
}
