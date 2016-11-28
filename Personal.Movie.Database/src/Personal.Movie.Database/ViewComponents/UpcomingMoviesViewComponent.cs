using Microsoft.AspNetCore.Mvc;
using Personal.Movie.Database.Model.MovieModel;
using Personal.Movie.Database.TMDbAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personal.Movie.Database.ViewComponents
{
    public class UpcomingMoviesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MovieBriefInfo> upcomingMoviesComponent = await MovieEnquiry.GetUpcomingMovies();
            for (int i = 0; i < upcomingMoviesComponent.Count; i++)
            {
                if (upcomingMoviesComponent[i].posterPath == null)
                {
                    upcomingMoviesComponent.Remove(upcomingMoviesComponent[i]);
                }
            }
            return View(upcomingMoviesComponent);
        }
    }
}
