using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;

namespace TBP.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<bool> IMDBIdExists(int imdbId);
        Task<List<Movie>> MovieThatContaints(string content, int page, int pagesize);
        Task<List<Character>> GetMovieCharactes(ObjectId movieId, int page, int pagesize);
        Task<List<Genre>> GetMovieGenres(ObjectId id);
        Task<List<Movie>> GetAllMoviesForCategory(ObjectId genreId);
    }
}
