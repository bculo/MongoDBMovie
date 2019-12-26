using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
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
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
