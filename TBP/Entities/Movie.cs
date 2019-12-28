using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace TBP.Entities
{
    public class Movie : Entity
    {
        public string PosterPath { get; set; }
        public string BackDropPath { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public int IMDBId { get; set; }
        public double IMDBRating { get; set; }
        public string Overview { get; set; }
        public DateTime ReleaseDate { get; set; }

        [BsonIgnore]
        public List<Character> Characters { get; set; }
        [BsonIgnore]
        public List<Genre> Genres { get; set; }
    }
}
