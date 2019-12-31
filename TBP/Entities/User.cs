using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TBP.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
        public ObjectId RoleId { get; set; }

        [BsonIgnore]
        public Role Role { get; set; }
     }
}
