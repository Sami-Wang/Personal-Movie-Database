using System.Collections.Generic;

namespace Personal.Movie.Database.Model.General
{
    public class ResponseData<T>
    {
        public int? responseCode { get; set; }
        public string responseStatusDescription { get; set; }
        public List<T> responseResults { get; set; }
    }
}
