namespace Personal.Movie.Database.TMDbAPI
{
    internal class APIConstant
    {
        internal const string APIKEY = "b50f91529a2996a98fc75f7cf84b029b";

        internal const string TMDBBASEURL = "https://api.themoviedb.org/3/";

        internal const string GETNOWPLAYINGMOVIESURL = "movie/now_playing?api_key=" + APIKEY + "&page=1";

        internal const string GETUPCOMINGMOVIESURL = "movie/upcoming?api_key=" + APIKEY + "&page=1";

        internal const string GETTOPRATEDMOVIESURL = "movie/top_rated?api_key=" + APIKEY + "&page=1";

        internal const string SEARCHMOVIEBYTITLEURL = "search/movie?api_key=" + APIKEY + "&query=";

        internal const string GETMOVIEDETAILSURL = "movie/" + "**********" + "?api_key=" + APIKEY;

        internal const string IMAGESERVERURL = "https://image.tmdb.org/t/p/original";
    }
}
