using System.Threading.Tasks;
using TBP.Entities;

namespace TBP.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByName(string username);
    }
}
