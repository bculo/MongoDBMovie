using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;

namespace TBP.Interfaces
{
    public interface IMovieClient
    {
        Task<List<Movie>> GetPopularMovies(int pageNumber);
        Task<List<Actor>> GetMovieCrew(int movieID);
    }
}
