
using Newtonsoft.Json.Linq;
using Personal.Movie.Database.Model.MovieModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Personal.Movie.Database.TMDbAPI
{
    public class MovieEnquiry
    {
        /// <summary>
        /// Get Now Playing Movies.
        /// </summary>
        /// <returns></returns>
        public async static Task<List<MovieBriefInfo>> GetNowPlayingMovies()
        {
            List<MovieBriefInfo> nowPlayingMovies = new List<MovieBriefInfo>();
            // Request TMDb API To Get Now Playing Movies
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(APIConstant.TMDBBASEURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(APIConstant.GETNOWPLAYINGMOVIESURL);
                    // Parse Json
                    string nowPlayingMoviesJsonString = await response.Content.ReadAsStringAsync();
                    JObject nowPlayingMoviesJsonObject = JObject.Parse(nowPlayingMoviesJsonString);
                    JArray nowPlayingMoviesJsonArray = (JArray)nowPlayingMoviesJsonObject.GetValue("results");
                    foreach (JObject npm in nowPlayingMoviesJsonArray)
                    {
                        MovieBriefInfo movieBriefInfo = new MovieBriefInfo();
                        if (npm.GetValue("poster_path").ToString().Trim().Equals(""))
                        {
                            movieBriefInfo.posterPath = null; 
                        }
                        else {
                            movieBriefInfo.posterPath = APIConstant.IMAGESERVERURL + npm.GetValue("poster_path").ToString();
                        }
                        movieBriefInfo.adult = Convert.ToBoolean(npm.GetValue("adult").ToString());
                        movieBriefInfo.overview = npm.GetValue("overview").ToString();
                        movieBriefInfo.releaseDate = Convert.ToDateTime(npm.GetValue("release_date").ToString());
                        List<int?> genreIDs = new List<int?>();
                        foreach (string gi in (JArray)npm.GetValue("genre_ids"))
                        {
                            genreIDs.Add(Convert.ToInt32(gi));
                        }
                        movieBriefInfo.genreIDs = genreIDs;
                        movieBriefInfo.id = Convert.ToInt32(npm.GetValue("id").ToString());
                        movieBriefInfo.originalTitle = npm.GetValue("original_title").ToString();
                        movieBriefInfo.originalLanguage = npm.GetValue("original_language").ToString();
                        movieBriefInfo.title = npm.GetValue("title").ToString();
                        if (npm.GetValue("backdrop_path").ToString().Trim().Equals("")) {
                            movieBriefInfo.backdropPath = null;
                        }
                        else {
                            movieBriefInfo.backdropPath = APIConstant.IMAGESERVERURL + npm.GetValue("backdrop_path").ToString();
                        }
                        movieBriefInfo.popularity = Convert.ToDecimal(npm.GetValue("popularity").ToString());
                        movieBriefInfo.voteCount = Convert.ToInt32(npm.GetValue("vote_count").ToString());
                        movieBriefInfo.video = Convert.ToBoolean(npm.GetValue("video").ToString());
                        movieBriefInfo.voteAverage = Convert.ToDecimal(npm.GetValue("vote_average").ToString());
                        nowPlayingMovies.Add(movieBriefInfo);
                    }
                    return nowPlayingMovies;
                }
                catch (Exception ex) {
                    return nowPlayingMovies;
                }
            }
        }

        /// <summary>
        /// Get Upcoming Movies.
        /// </summary>
        /// <returns></returns>
        public async static Task<List<MovieBriefInfo>> GetUpcomingMovies()
        {
            List<MovieBriefInfo> upcomingMovies = new List<MovieBriefInfo>();
            // Request TMDb API To Get Now Playing Movies
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(APIConstant.TMDBBASEURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(APIConstant.GETUPCOMINGMOVIESURL);
                    // Parse Json
                    string upcomingMoviesJsonString = await response.Content.ReadAsStringAsync();
                    JObject upcomingMoviesJsonObject = JObject.Parse(upcomingMoviesJsonString);
                    JArray upcomingMoviesJsonArray = (JArray)upcomingMoviesJsonObject.GetValue("results");
                    foreach (JObject um in upcomingMoviesJsonArray)
                    {
                        MovieBriefInfo movieBriefInfo = new MovieBriefInfo();
                        if (um.GetValue("poster_path").ToString().Trim().Equals(""))
                        {
                            movieBriefInfo.posterPath = null;
                        }
                        else
                        {
                            movieBriefInfo.posterPath = APIConstant.IMAGESERVERURL + um.GetValue("poster_path").ToString();
                        }
                        movieBriefInfo.adult = Convert.ToBoolean(um.GetValue("adult").ToString());
                        movieBriefInfo.overview = um.GetValue("overview").ToString();
                        movieBriefInfo.releaseDate = Convert.ToDateTime(um.GetValue("release_date").ToString());
                        List<int?> genreIDs = new List<int?>();
                        foreach (string gi in (JArray)um.GetValue("genre_ids"))
                        {
                            genreIDs.Add(Convert.ToInt32(gi));
                        }
                        movieBriefInfo.genreIDs = genreIDs;
                        movieBriefInfo.id = Convert.ToInt32(um.GetValue("id").ToString());
                        movieBriefInfo.originalTitle = um.GetValue("original_title").ToString();
                        movieBriefInfo.originalLanguage = um.GetValue("original_language").ToString();
                        movieBriefInfo.title = um.GetValue("title").ToString();
                        if (um.GetValue("backdrop_path").ToString().Trim().Equals(""))
                        {
                            movieBriefInfo.backdropPath = null;
                        }
                        else
                        {
                            movieBriefInfo.backdropPath = APIConstant.IMAGESERVERURL + um.GetValue("backdrop_path").ToString();
                        }
                        movieBriefInfo.popularity = Convert.ToDecimal(um.GetValue("popularity").ToString());
                        movieBriefInfo.voteCount = Convert.ToInt32(um.GetValue("vote_count").ToString());
                        movieBriefInfo.video = Convert.ToBoolean(um.GetValue("video").ToString());
                        movieBriefInfo.voteAverage = Convert.ToDecimal(um.GetValue("vote_average").ToString());
                        upcomingMovies.Add(movieBriefInfo);
                    }
                    return upcomingMovies;
                }
                catch (Exception ex)
                {
                    return upcomingMovies;
                }
            }
        }

        /// <summary>
        /// Get Top Rated Movies.
        /// </summary>
        /// <returns></returns>
        public async static Task<List<MovieBriefInfo>> GetTopRatedMovies()
        {
            List<MovieBriefInfo> topRatedMovies = new List<MovieBriefInfo>();
            // Request TMDb API To Get Now Playing Movies
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = new Uri(APIConstant.TMDBBASEURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(APIConstant.GETTOPRATEDMOVIESURL);
                    // Parse Json
                    string topRatedMoviesJsonString = await response.Content.ReadAsStringAsync();
                    JObject topRatedMoviesJsonObject = JObject.Parse(topRatedMoviesJsonString);
                    JArray topRatedMoviesJsonArray = (JArray)topRatedMoviesJsonObject.GetValue("results");
                    foreach (JObject trm in topRatedMoviesJsonArray)
                    {
                        MovieBriefInfo movieBriefInfo = new MovieBriefInfo();
                        if (trm.GetValue("poster_path").ToString().Trim().Equals(""))
                        {
                            movieBriefInfo.posterPath = null;
                        }
                        else
                        {
                            movieBriefInfo.posterPath = APIConstant.IMAGESERVERURL + trm.GetValue("poster_path").ToString();
                        }
                        movieBriefInfo.adult = Convert.ToBoolean(trm.GetValue("adult").ToString());
                        movieBriefInfo.overview = trm.GetValue("overview").ToString();
                        movieBriefInfo.releaseDate = Convert.ToDateTime(trm.GetValue("release_date").ToString());
                        List<int?> genreIDs = new List<int?>();
                        foreach (string gi in (JArray)trm.GetValue("genre_ids"))
                        {
                            genreIDs.Add(Convert.ToInt32(gi));
                        }
                        movieBriefInfo.genreIDs = genreIDs;
                        movieBriefInfo.id = Convert.ToInt32(trm.GetValue("id").ToString());
                        movieBriefInfo.originalTitle = trm.GetValue("original_title").ToString();
                        movieBriefInfo.originalLanguage = trm.GetValue("original_language").ToString();
                        movieBriefInfo.title = trm.GetValue("title").ToString();
                        if (trm.GetValue("backdrop_path").ToString().Trim().Equals(""))
                        {
                            movieBriefInfo.backdropPath = null;
                        }
                        else
                        {
                            movieBriefInfo.backdropPath = APIConstant.IMAGESERVERURL + trm.GetValue("backdrop_path").ToString();
                        }
                        movieBriefInfo.popularity = Convert.ToDecimal(trm.GetValue("popularity").ToString());
                        movieBriefInfo.voteCount = Convert.ToInt32(trm.GetValue("vote_count").ToString());
                        movieBriefInfo.video = Convert.ToBoolean(trm.GetValue("video").ToString());
                        movieBriefInfo.voteAverage = Convert.ToDecimal(trm.GetValue("vote_average").ToString());
                        topRatedMovies.Add(movieBriefInfo);
                    }
                    return topRatedMovies;
                }
                catch (Exception ex)
                {
                    return topRatedMovies;
                }
            }
        }

        /// <summary>
        /// Search Movie By Title.
        /// </summary>
        /// <param name="movieTitle"></param>
        /// <returns></returns>
        public async static Task<List<MovieBriefInfo>> SearchMovieByTitle(string movieTitle) {
            List<MovieBriefInfo> movieSearchResults = new List<MovieBriefInfo>();
            // Request TMDb API To Search Movie By Title        
            using (HttpClient client = new HttpClient())
            {               
                try
                {
                    client.BaseAddress = new Uri(APIConstant.TMDBBASEURL);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(APIConstant.SEARCHMOVIEBYTITLEURL +
                        movieTitle.Replace(" ", "%20"));
                    // Parse Json
                    string movieSearchResultsJsonString = await response.Content.ReadAsStringAsync();
                    JObject movieSearchResultsJsonObject = JObject.Parse(movieSearchResultsJsonString);
                    JArray movieSearchResultsJsonArray = (JArray)movieSearchResultsJsonObject.GetValue("results");
                    foreach (JObject msrja in movieSearchResultsJsonArray) {
                        MovieBriefInfo movieBriefInfo = new MovieBriefInfo();
                        movieBriefInfo.posterPath = APIConstant.IMAGESERVERURL + msrja.GetValue("poster_path").ToString();
                        movieBriefInfo.adult = Convert.ToBoolean(msrja.GetValue("adult").ToString());
                        movieBriefInfo.overview = msrja.GetValue("overview").ToString();
                        movieBriefInfo.releaseDate = Convert.ToDateTime(msrja.GetValue("release_date").ToString());
                        List<int?> genreIDs = new List<int?>();
                        foreach (string gi in (JArray)msrja.GetValue("genre_ids")) {                          
                            genreIDs.Add(Convert.ToInt32(gi));
                        }
                        movieBriefInfo.genreIDs = genreIDs;
                        movieBriefInfo.id = Convert.ToInt32(msrja.GetValue("id").ToString());
                        movieBriefInfo.originalTitle = msrja.GetValue("original_title").ToString();
                        movieBriefInfo.originalLanguage = msrja.GetValue("original_language").ToString();
                        movieBriefInfo.title = msrja.GetValue("title").ToString();
                        movieBriefInfo.backdropPath = APIConstant.IMAGESERVERURL + msrja.GetValue("backdrop_path").ToString();
                        movieBriefInfo.popularity = Convert.ToDecimal(msrja.GetValue("popularity").ToString());
                        movieBriefInfo.voteCount = Convert.ToInt32(msrja.GetValue("vote_count").ToString());
                        movieBriefInfo.video = Convert.ToBoolean(msrja.GetValue("video").ToString());
                        movieBriefInfo.voteAverage = Convert.ToDecimal(msrja.GetValue("vote_average").ToString());
                        movieSearchResults.Add(movieBriefInfo);
                    }
                    return movieSearchResults;
                }
                catch (Exception ex)
                {
                    return movieSearchResults;
                }
            }                
        }
    }
}
