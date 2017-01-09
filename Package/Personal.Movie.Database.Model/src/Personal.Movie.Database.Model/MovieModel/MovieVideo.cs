namespace Personal.Movie.Database.Model.MovieModel
{
    public class MovieVideo
    {
        public string movieVideoID { get; set; }
        public string languageCode { get; set; }
        public string countryCode { get; set; }
        public string videoURL { get; set; }
        public string videoName { get; set; }
        public int? videoSize { get; set; }
        public string videoType { get; set; }
    }
}
