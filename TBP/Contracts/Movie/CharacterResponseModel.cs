using System;

namespace TBP.Contracts.Movie
{
    public class CharacterResponseModel
    {
        public string Id { get; set; }
        public string CharacterInMovie { get; set; }
        public string Name { get; set; }
        public string ProfilePath { get; set; }
    }
}
