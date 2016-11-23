
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
                        movieBriefInfo.posterPath = APIConstant.IMAGESERVERURL + npm.GetValue("poster_path").ToString();
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
                        movieBriefInfo.backdropPath = APIConstant.IMAGESERVERURL + npm.GetValue("backdrop_path").ToString();
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
