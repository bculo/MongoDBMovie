namespace TBP.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}
