using System;

namespace TBP.Contracts.Movie
{
    public class MovieResponseModel
    {
        public string Id { get; set; }
        public string PosterPath { get; set; }
        public string BackDropPath { get; set; }
        public string Title { get; set; }
        public int IMDBId { get; set; }
        public double IMDBRating { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
