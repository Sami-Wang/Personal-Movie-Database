using Microsoft.AspNetCore.Mvc;
using Personal.Movie.Database.Model.MovieModel;
using Personal.Movie.Database.TMDbAPI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Personal.Movie.Database.ViewComponents
{
    public class NowPlayingMoviesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MovieBriefInfo> nowPlayingMoviesComponent = await MovieEnquiry.GetNowPlayingMovies();
            for (int i = 0; i < nowPlayingMoviesComponent.Count; i++) {
                if (nowPlayingMoviesComponent[i].posterPath == null) {
                    nowPlayingMoviesComponent.Remove(nowPlayingMoviesComponent[i]);
                }
            }
            return View(nowPlayingMoviesComponent);
        }        
    }
}
