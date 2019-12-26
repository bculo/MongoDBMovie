using System.Collections.Generic;

namespace TBP.Clients.MovieModels
{
    public class PopularMovieClientResponse
    {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<MovieClientModel> results { get; set; }
    }
}
