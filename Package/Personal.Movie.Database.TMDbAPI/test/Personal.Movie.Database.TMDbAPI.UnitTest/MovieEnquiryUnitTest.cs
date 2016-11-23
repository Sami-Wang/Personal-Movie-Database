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

        // Test MovieEnquiry.SearchMovieByTitle
        [Fact]
        public void SearchMovieByTitlePassingTest()
        {
            var movieSearchResults = MovieEnquiry.SearchMovieByTitle("Lord of the Rings").Result;
            Assert.NotNull(movieSearchResults);
        }
    }
}
