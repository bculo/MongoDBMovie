using System.Collections.Generic;

namespace TBP.Clients.ActorModels
{
    public class CreditsMovieClientResponse
    {
        public int id { get; set; }
        public List<ActorClientModel> cast { get; set; }
    }
}
