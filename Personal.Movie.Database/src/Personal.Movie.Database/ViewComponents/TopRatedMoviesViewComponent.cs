using Microsoft.AspNetCore.Mvc;
using Personal.Movie.Database.Model.MovieModel;
using Personal.Movie.Database.TMDbAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personal.Movie.Database.ViewComponents
{
    public class TopRatedMoviesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MovieBriefInfo> topRatedMoviesComponent = await MovieEnquiry.GetTopRatedMovies();
            for (int i = 0; i < topRatedMoviesComponent.Count; i++)
            {
                if (topRatedMoviesComponent[i].posterPath == null)
                {
                    topRatedMoviesComponent.Remove(topRatedMoviesComponent[i]);
                }
            }
            return View(topRatedMoviesComponent);
        }
    }
}
