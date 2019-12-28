using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Options;

namespace TBP.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(IOptions<MovieDatabaseOptions> settings) : base(settings) { }
        public GenreRepository(MovieDatabaseOptions settings) : base(settings) { }

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
    }
}
