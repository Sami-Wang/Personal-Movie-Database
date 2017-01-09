using Microsoft.AspNetCore.Mvc;
using Personal.Movie.Database.Model.MovieModel;
using Personal.Movie.Database.TMDbAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personal.Movie.Database.ViewComponents
{
    public class MovieVideosViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? movieID)
        {
            List<MovieVideo> movieVideosComponent = await MovieEnquiry.GetMovieVideos(movieID);
            return View(movieVideosComponent);
        }
    }
}