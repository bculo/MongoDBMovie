using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;
using TBP.Services.Result;

namespace TBP.Interfaces
{
    public interface IMovieService
    {
        Task<ServiceResult> AddMovie(Movie movie);
        Task<List<Movie>> SearchMovie(string title);
        Task<List<Movie>> GetAllMovies();
        Task<ServiceResult> LikeMovie(Guid movieID, Guid userID);
        Task<ServiceResult> CommentMovie(Guid movieID, Guid userID, string comment);
    }
}
