using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;

namespace TBP.Interfaces
{
    public interface IMovieClient
    {
        Task<List<Genre>> GetMovieGenres(int imdbMovieId);
        Task<List<Movie>> GetPopularMovies(int pageNumber);
        Task<List<Character>> GetMovieCrew(Movie movie);
    }
}
