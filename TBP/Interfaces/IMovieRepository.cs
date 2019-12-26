using System.Threading.Tasks;
using TBP.Entities;

namespace TBP.Interfaces
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<bool> IMDBIdExists(int imdbId);
    }
}
