using System.Threading.Tasks;
using TBP.Services.Result;

namespace TBP.Interfaces
{
    public interface IUserService
    {
        Task<AuthLoginResult> Login(string userName, string plainPassword);
        Task<ServiceResult> Register(string userName, string email, string plainPassword);
    }
}
