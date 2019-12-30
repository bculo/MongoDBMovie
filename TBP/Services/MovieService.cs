using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Options;
using TBP.Services.Result;

namespace TBP.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movierepo;
        private readonly IGenreRepository _genrerepo;
        private readonly IRepository<MovieGenre> _mgrepo;
        private readonly IRepository<Character> _characterrepo;
        private readonly PaginationOptions _options;
        private readonly IMovieClient _client;

        [ActivatorUtilitiesConstructor]
        public MovieService(IMovieRepository movierepo, 
            IGenreRepository genrerepo, 
            IRepository<MovieGenre> mgrepo,
            IRepository<Character> characterrepo,
            IOptions<PaginationOptions> options,
            IMovieClient client)
        {
            _movierepo = movierepo;
            _genrerepo = genrerepo;
            _mgrepo = mgrepo;
            _options = options.Value;
            _client = client;
            _characterrepo = characterrepo;
        }

        public MovieService(IMovieRepository movierepo, IGenreRepository genrerepo, IRepository<MovieGenre> mgrepo)
        {
            _movierepo = movierepo;
            _genrerepo = genrerepo;
            _mgrepo = mgrepo;
        }

        public async Task<ServiceResult> AddGenreToMovie(string movieId, Genre genre)
        {
            var result = new ServiceResult();

            if (!await _genrerepo.IMDBIdExists(genre.IMDBId))
            {
                if (!await _genrerepo.Add(genre))
                {
                    result.SetErrorMessage("Error");
                    return result;
                }
            }

            Genre mongoGenre = await _genrerepo.GetByIMDBId(genre.IMDBId);

            MovieGenre instance = new MovieGenre
            {
                GenreId = mongoGenre.Id,
                MovieId = GetObjectId(movieId),
            };

            if (!await _mgrepo.Add(instance))
            {
                result.SetErrorMessage("Error");
                return result;
            }
            return result;
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

        public async Task<List<Genre>> GetAllGenres()
        {
            return await _genrerepo.GetAll();
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _movierepo.GetAll();
        }

        public async Task<List<Movie>> GetMovies(int page)
        {
            return await _movierepo.GetPaginatedResult(page, _options.PageSize);
        }

        public async Task<List<Movie>> SearchMovie(string title, int page)
        {
            title = title.Trim().ToLower();
            return await _movierepo.MovieThatContaints(title, page, _options.PageSize);
        }

        public async Task<List<Character>> GetMovieCharacters(string movieID)
        {
            var movie = await _movierepo.GetById(GetObjectId(movieID));
            if (movie == null)
                return new List<Character>();

            if (await _movierepo.GetNumberOfCharactesInMovie(GetObjectId(movieID)) > 0)
                return await _movierepo.GetMovieCharactes(GetObjectId(movieID));

            var charactes = await _client.GetMovieCrew(movie);
            await _characterrepo.AddRange(charactes);
            return await _movierepo.GetMovieCharactes(GetObjectId(movieID));
        }

        public async Task<List<Genre>> GetMovieGenres(string movieId)
        {
            var movie = await _movierepo.GetById(GetObjectId(movieId));
            if (movie == null)
                return new List<Genre>();

            var result = await _movierepo.GetMovieGenres(movie.Id);
            return result;
        }

        private ObjectId GetObjectId(string id)
        {
            return ObjectId.Parse(id);
        }

        public async Task<List<Movie>> GetAllMoviesForGenre(string genreId)
        {
            var movie = await _genrerepo.GetById(GetObjectId(genreId));
            if (movie == null)
                return new List<Movie>();

            return await _movierepo.GetAllMoviesForCategory(GetObjectId(genreId));
        }
    }
}
