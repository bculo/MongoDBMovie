using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace TBP.Entities
{
    public class Character : Entity
    {
        public string CharacterInMovie { get; set; }
        public int IMDBId { get; set; }
        public string Name { get; set; }
        public string ProfilePath { get; set; }
        public ObjectId MovieId { get; set; }

        [BsonIgnore]
        public Movie Movie { get; set; }
    }
}
