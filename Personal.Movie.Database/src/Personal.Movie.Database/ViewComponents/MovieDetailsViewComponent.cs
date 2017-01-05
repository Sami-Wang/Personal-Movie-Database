using Microsoft.AspNetCore.Mvc;
using Personal.Movie.Database.Model.MovieModel;
using Personal.Movie.Database.TMDbAPI;
using System.Threading.Tasks;

namespace Personal.Movie.Database.ViewComponents
{
    public class MovieDetailsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int? movieID)
        {
            MovieDetailInfo movieDetailsComponent = await MovieEnquiry.GetMovieDetails(movieID);
            return View(movieDetailsComponent);
        }
    }
}
