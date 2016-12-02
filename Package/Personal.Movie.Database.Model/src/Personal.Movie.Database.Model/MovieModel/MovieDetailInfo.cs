using System;
using System.Collections.Generic;

namespace Personal.Movie.Database.Model.MovieModel
{
    public class MovieDetailInfo
    {
        public bool? adult { get; set; }
        public string backdropPath { get; set; }
        public MovieCollection movieCollection { get; set; }
        public decimal? budget { get; set; }
        public List<Genre> genres { get; set; }
        public string homepage { get; set; }
        public int? id { get; set; }
        public string imdbID { get; set; }
        public string originalLanguage { get; set; }
        public string originalTitle { get; set; }
        public string overView { get; set; }
        public decimal? popularity { get; set; }
        public string posterPath { get; set; }
        public List<ProductionCompany> productionCompanies { get; set; }
        public List<ProductionCountry> productionCountries { get; set; }
        public DateTime? releaseDate { get; set; }
        public decimal? revenue { get; set; }
        public int? runtime { get; set; }
        public List<SpokenLanguage> spokenLanguages { get; set; }
        public string status { get; set; }
        public string tagline { get; set; }
        public string title { get; set; }
        public bool? video { get; set; }
        public decimal? voteAverage { get; set; }
        public int? voteCount { get; set; }
    }

    public class MovieCollection {
        public int? id { get; set; }
        public string name { get; set; }
        public string posterPath { get; set; }
        public string backDropPath { get; set; }
    }

    public class Genre {
        public int? id { get; set; }
        public string name { get; set; }
    }

    public class ProductionCompany {
        public int? id { get; set; }
        public string name { get; set; }
    }

    public class ProductionCountry {
        public string countryCode { get; set; }
        public string countryName { get; set; }
    }

    public class SpokenLanguage {
        public string languageCode { get; set; }
        public string languageName { get; set; }
    }
}
