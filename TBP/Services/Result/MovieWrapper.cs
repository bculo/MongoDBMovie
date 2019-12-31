using TBP.Entities;

namespace TBP.Services.Result
{
    public class MovieWrapper
    {
        public string PosterPath { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public int IMDBId { get; set; }
        public double IMDBRating { get; set; }
        public string Overview { get; set; }
        public bool ExistsInDb { set; get; } = false;

        public MovieWrapper(Movie movie)
        {
            PosterPath = movie.PosterPath;
            Title = movie.Title;
            Language = movie.Language;
            IMDBId = movie.IMDBId;
            IMDBRating = movie.IMDBRating;
            Overview = movie.Overview;
        }
    }
}
