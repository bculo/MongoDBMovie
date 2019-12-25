using System.Threading.Tasks;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Services.Result;

namespace TBP.Services
{
    public class UserService : IUserService
    {
        protected readonly IPassword _hasher;
        protected readonly ITokenManager _token;
        protected readonly IUserRepository _userrepo;

        public UserService(IPassword hasher, ITokenManager token, IUserRepository userrepo)
        {
            _hasher = hasher;
            _token = token;
            _userrepo = userrepo;
        }

        public virtual async Task<AuthLoginResult> Login(string userName, string plainPassword)
        {
            User fetchedUser = await _userrepo.GetUserByName(userName);

            var result = new AuthLoginResult();

            if (fetchedUser != null)
            {
                bool goodPassword = await _hasher.CheckPassword(fetchedUser.HashedPassword, plainPassword);
                if (!goodPassword)
                {
                    result.SetErrorMessage("Wrong password");
                    return result;
                }

                result.Token = _token.CreateJWTToken(fetchedUser);
                return result;
            }

            result.SetErrorMessage("User doesnt exist");
            return result;
        }

        public virtual async Task<ServiceResult> Register(string userName, string email, string plainPassword)
        {
            User fetchedUser = await _userrepo.GetUserByName(userName);

            var result = new ServiceResult();

            if (fetchedUser != null) 
            {
                if (fetchedUser.Email.ToLower() == email.ToLower())
                    result.SetErrorMessage($"Email {email} is alreday taken");

                result.SetErrorMessage($"Username {userName} is alreday taken");
                return result;
            }

            User newUser = new User()
            {
                UserName = userName,
                Email = email,
                HashedPassword = await _hasher.HashPassword(plainPassword)
            };

            if(!await _userrepo.Add(newUser))
            {
                result.SetErrorMessage($"Ups something went wrong :(");
                return result;
            }

            return result;
        }
    }
}
