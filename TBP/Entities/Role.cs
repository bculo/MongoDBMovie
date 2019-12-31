namespace TBP.Entities
{
    public class Role : Entity
    {
        public string Name { get; set; }
        public string LowercaseName => Name?.ToLower() ?? "";
    }
}
