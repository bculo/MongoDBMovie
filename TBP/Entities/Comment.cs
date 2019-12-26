using System;

namespace TBP.Entities
{
    public class Comment
    {
        public string Content { get; set; }
        public int MovieID { get; set; }
        public string UserID { get; set; }
        public DateTime Created { get; set; }
    }
}
