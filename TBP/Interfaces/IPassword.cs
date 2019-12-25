using System.Threading.Tasks;

namespace TBP.Interfaces
{
    public interface IPassword
    {
        Task<string> HashPassword(string plainPassword);
        Task<bool> CheckPassword(string userCredentials, string plainPassword);
    }
}
