using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;

namespace TBP.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<bool> IMDBIdExists(int imdbId);
        Task<Movie> GetByImdbId(int imdbID);
        Task<List<Movie>> MovieThatContaints(string content, int page, int pagesize);
        Task<List<Genre>> GetMovieGenres(ObjectId id);
        Task<List<Movie>> GetAllMoviesForCategory(ObjectId genreId);
        Task<int> GetNumberOfCharactesInMovie(ObjectId objectId);
        Task<List<Character>> GetMovieCharactes(ObjectId objectId);
    }
}
