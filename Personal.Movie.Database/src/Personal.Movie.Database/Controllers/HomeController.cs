using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Personal.Movie.Database.Model.MovieModel;
using Personal.Movie.Database.TMDbAPI;

namespace Personal.Movie.Database.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> MovieDetailsInfo(int? id)
        {
            return View(id);
        }

        public async Task<IActionResult> SearchMovies(string movieTitle)
        {
            List<MovieBriefInfo> searchMovies = await MovieEnquiry.SearchMovieByTitle(movieTitle);
            return View(searchMovies);
        }
    }
}
