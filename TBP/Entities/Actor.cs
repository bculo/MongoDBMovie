namespace TBP.Entities
{
    public class Actor : Entity
    {
        public string Character { get; set; }
        public int IMDBId { get; set; }
        public string Name { get; set; }
        public string ProfilePath { get; set; }
    }
}
