using System;

namespace TBP.Clients.MovieModels
{
    public class MovieClientModel
    {
        public string poster_path { get; set; }
        public int id { get; set; }
        public string backdrop_path { get; set; }
        public string original_language { get; set; }
        public string title { get; set; }
        public double vote_average { get; set; }
        public string overview { get; set; }
        public DateTime release_date { get; set; }
    }
}
