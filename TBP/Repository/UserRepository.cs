using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;
using TBP.Entities;
using TBP.Interfaces;
using TBP.Options;

namespace TBP.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IOptions<MovieDatabaseOptions> settings) : base(settings) { }

        public async Task<User> GetUserByName(string username)
        {
            try
            {
                return await _mongo.Find(item => item.UserName.ToLower() == username.ToLower())
                    .FirstOrDefaultAsync();
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
