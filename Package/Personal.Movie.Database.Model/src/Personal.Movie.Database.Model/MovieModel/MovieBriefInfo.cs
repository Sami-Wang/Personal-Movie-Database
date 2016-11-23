using System;
using System.Collections.Generic;

namespace Personal.Movie.Database.Model.MovieModel
{
    public class MovieBriefInfo
    {
        public string posterPath { get; set; }
        public bool? adult { get; set; }
        public string overview { get; set; }
        public DateTime? releaseDate { get; set; }
        public List<int?> genreIDs { get; set; }
        public int? id { get; set; }
        public string originalTitle { get; set; }
        public string originalLanguage { get; set; }
        public string title { get; set; }
        public string backdropPath { get; set; }
        public decimal? popularity { get; set; }
        public int? voteCount { get; set; }
        public bool? video { get; set; }
        public decimal? voteAverage { get; set; }
    }
}
