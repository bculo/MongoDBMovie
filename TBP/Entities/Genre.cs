using System.Collections.Generic;

namespace TBP.Entities
{
    public class Genre : Entity
    {
        public string Name { get; set; }
        public int IMDBId { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
