using TBP.Entities;

namespace TBP.Interfaces
{
    public interface ITokenManager
    {
        string CreateJWTToken(User user);
    }
}
