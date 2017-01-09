using Xunit;

namespace Personal.Movie.Database.TMDbAPI.UnitTest
{
    public class MovieEnquiryUnitTest
    {
        // Test MovieEnquiry.GetNowPlayingMovies
        [Fact]
        public void GetNowPlayingMoviesPassingTest()
        {
            var nowPlayingMovies = MovieEnquiry.GetNowPlayingMovies().Result;
            Assert.NotNull(nowPlayingMovies);
        }

        // Test MovieEnquiry.GetUpcomingMovies
        [Fact]
        public void GetUpcomingMoviesPassingTest()
        {
            var upcomingMovies = MovieEnquiry.GetUpcomingMovies().Result;
            Assert.NotNull(upcomingMovies);
        }

        // Test MovieEnquiry.GetTopRatedMovies
        [Fact]
        public void GetTopRatedMoviesPassingTest()
        {
            var topRatedMovies = MovieEnquiry.GetTopRatedMovies().Result;
            Assert.NotNull(topRatedMovies);
        }

        // Test MovieEnquiry.SearchMovieByTitle
        [Fact]
        public void SearchMovieByTitlePassingTest()
        {
            var movieSearchResults = MovieEnquiry.SearchMovieByTitle("Lord of the Rings").Result;
            Assert.NotNull(movieSearchResults);
        }

        // Test MovieEnquiry.GetMovieDetails
        [Fact]
        public void GetMovieDetailsPassingTest()
        {
            var movieDetails = MovieEnquiry.GetMovieDetails(120).Result;
            Assert.NotNull(movieDetails);
        }

        // Test MovieEnquiry.GetMovieVideos
        [Fact]
        public void GetMovieVideosPassingTest()
        {
            var movieVideos = MovieEnquiry.GetMovieVideos(120).Result;
            Assert.NotNull(movieVideos);
        }
    }
}
