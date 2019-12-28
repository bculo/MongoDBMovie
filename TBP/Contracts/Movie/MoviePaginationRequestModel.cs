using System;

namespace TBP.Contracts.Movie
{
    public class MoviePaginationRequestModel
    {
        public string MovieId { get; set; }
        public int Page { get; set; }
    }
}
