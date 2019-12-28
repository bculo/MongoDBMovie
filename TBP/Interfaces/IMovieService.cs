using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;
using TBP.Services.Result;

namespace TBP.Interfaces
{
    public interface IMovieService
    {
        Task<ServiceResult> AddMovie(Movie movie);
        Task<ServiceResult> AddGenreToMovie(string movieId, Genre genre);
        Task<List<Movie>> SearchMovie(string title, int page);
        Task<List<Genre>> GetAllGenres();
        Task<List<Movie>> GetAllMovies();
        Task<List<Movie>> GetMovies(int page);
        Task<ServiceResult> LikeMovie(string movieID, string userID);
        Task<ServiceResult> CommentMovie(string movieID, string userID, string comment);
        Task<List<Character>> GetMovieCharacters(string movieID, int page);
        Task<List<Genre>> GetMovieGenres(string movieId);
        Task<List<Movie>> GetAllMoviesForGenre(string genreId);
    }
}
