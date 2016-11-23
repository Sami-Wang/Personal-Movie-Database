using Xunit;

namespace Personal.Movie.Database.TMDbAPI.UnitTest
{
    public class MovieEnquiryUnitTest
    {
        // Test MovieEnquiry.SearchMovieByTitle
        [Fact]
        public void SearchMovieByTitlePassingTest()
        {
            // Test Account Number
            var movieSearchResults = MovieEnquiry.SearchMovieByTitle("Lord of the Rings").Result;
            Assert.NotNull(movieSearchResults);
        }
    }
}
