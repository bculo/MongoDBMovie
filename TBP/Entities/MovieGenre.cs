using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TBP.Entities
{
    public class MovieGenre : Entity
    {
        public ObjectId MovieId { get; set; }
        public ObjectId GenreId { get; set; }

        [BsonIgnore]
        public Movie Movie { get; set; }
        [BsonIgnore]
        public Genre Genre { get; set; }
    }
}
