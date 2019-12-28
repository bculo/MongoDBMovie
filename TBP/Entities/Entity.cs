using MongoDB.Bson;

namespace TBP.Entities
{
    public abstract class Entity
    {
        public ObjectId Id { get; set; }
    }
}
