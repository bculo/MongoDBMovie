using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Services.Result;

namespace TBP.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movierepo;

        public MovieService(IMovieRepository movierepo)
        {
            _movierepo = movierepo;
        }

        public async Task<ServiceResult> AddMovie(Movie movie)
        {
            var result = new ServiceResult();

            if (await _movierepo.IMDBIdExists(movie.IMDBId))
            {
                result.SetErrorMessage("Movie already exists");
                return result;
            }

            if(!await _movierepo.Add(movie))
            {
                result.SetErrorMessage("Error");
                return result;
            }

            return result;
        }

        public Task<ServiceResult> CommentMovie(Guid movieID, Guid userID, string comment)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _movierepo.GetAll();
        }

        public Task<ServiceResult> LikeMovie(Guid movieID, Guid userID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> SearchMovie(string title)
        {
            throw new NotImplementedException();
        }
    }
}
