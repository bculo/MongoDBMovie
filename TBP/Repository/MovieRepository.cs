using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Options;

namespace TBP.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(IOptions<MovieDatabaseOptions> settings) : base(settings) { }
        public MovieRepository(MovieDatabaseOptions settings) : base(settings) { }

        public async Task<bool> IMDBIdExists(int imdbId)
        {
            try
            {
                var result = await _mongo.Find(item => item.IMDBId == imdbId).FirstOrDefaultAsync();
                if (result != null)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Movie>> MovieThatContaints(string content, int page, int pagesize)
        {
            try
            {
                return await _mongo.Find(item => item.Title.ToLower().Contains(content))
                    .SortByDescending(i => i.IMDBRating)
                    .Skip((page - 1) * pagesize)
                    .Limit(pagesize)
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Movie>();
            }
        }

        public async Task<List<Genre>> GetMovieGenres(ObjectId id)
        {
            try
            {
                var movieGenreCollection = _mongoDatabase.GetCollection<MovieGenre>(nameof(MovieGenre));

                return await movieGenreCollection.Aggregate()
                    .Match(i => i.MovieId == id)
                    .Lookup(nameof(Genre), "GenreId", "_id", "Genres")
                    .Lookup(nameof(Movie), "MovieId", "_id", "Movies")
                    .Project(i => new Genre
                    {
                        Id = (ObjectId)i["Genres"][0]["_id"],
                        Name = (string)i["Genres"][0]["Name"]
                    })
                    .ToListAsync();
            }
            catch (Exception)
            {
                return new List<Genre>();
            }
        }

        public async Task<List<Movie>> GetAllMoviesForCategory(ObjectId genreId)
        {
            try
            {
                var movieGenreCollection = _mongoDatabase.GetCollection<MovieGenre>(nameof(MovieGenre));

                var result = await movieGenreCollection.Aggregate()
                    .Match(i => i.GenreId == genreId)
                    .Lookup(nameof(Movie), "MovieId", "_id", "Movies")
                    .Project(i => new Movie
                    {
                        Id = (ObjectId)i["Movies"][0]["_id"],
                        Title = (string)i["Movies"][0]["Title"],
                        Language = (string)i["Movies"][0]["Language"],
                        Overview = (string)i["Movies"][0]["Overview"],
                        BackDropPath = (string)i["Movies"][0]["BackDropPath"],
                        PosterPath = (string)i["Movies"][0]["PosterPath"],
                        IMDBId = (int)i["Movies"][0]["IMDBId"],
                        IMDBRating = (int)i["Movies"][0]["IMDBRating"],
                        ReleaseDate = (DateTime)i["Movies"][0]["ReleaseDate"]
                    })
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                return new List<Movie>();
            }
        }

        public async Task<List<Character>> GetMovieCharactes(ObjectId objectId)
        {
            try
            {
                var characterCollection = _mongoDatabase.GetCollection<Character>(nameof(Character));

                var result = await characterCollection.Find(item => item.MovieId == objectId)
                    .ToListAsync();

                return result;
            }
            catch (Exception)
            {
                return new List<Character>();
            }
        }

        public async Task<int> GetNumberOfCharactesInMovie(ObjectId objectId)
        {
            try
            {
                var characterCollection = _mongoDatabase.GetCollection<Character>(nameof(Character));
                return (int)await characterCollection.Find(item => item.MovieId == objectId)
                    .CountDocumentsAsync();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public async Task<Movie> GetByImdbId(int imdbID)
        {
            try
            {
                return await _mongo.Find(item => item.IMDBId == imdbID).FirstOrDefaultAsync();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
