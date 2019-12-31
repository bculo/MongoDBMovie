using Microsoft.Extensions.Options;
using MongoDB.Bson;
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
                var roleCollection = _mongoDatabase.GetCollection<Role>(nameof(Role));

                var result =  await _mongo.Aggregate()
                    .Match(item => item.UserName.ToLower() == username.ToLower())
                    .Lookup(nameof(Role), "RoleId", "_id", "Roles")
                    .Project(i => new User
                    {
                        Id = (ObjectId)i["_id"],
                        UserName = (string)i["UserName"],
                        HashedPassword = (string)i["HashedPassword"],
                        Role = new Role
                        {
                            Id = (ObjectId)i["Roles"][0]["_id"],
                            Name = (string)i["Roles"][0]["Name"]
                        }
                    })
                    .FirstOrDefaultAsync();

                return result;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
