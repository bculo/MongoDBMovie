using System.Threading.Tasks;
using TBP.Entities;

namespace TBP.Interfaces
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task<bool> IMDBIdExists(int imdbId);
    }
}
