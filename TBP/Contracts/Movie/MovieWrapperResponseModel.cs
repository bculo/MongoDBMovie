namespace TBP.Contracts.Movie
{
    public class MovieWrapperResponseModel
    {
        public string PosterPath { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public int IMDBId { get; set; }
        public double IMDBRating { get; set; }
        public string Overview { get; set; }
        public bool ExistsInDb { set; get; } = false;
    }
}
