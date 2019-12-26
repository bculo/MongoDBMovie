using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBP.Clients.ActorModels
{
    public class CreditsMovieClientResponse
    {
        public int id { get; set; }
        public List<ActorClientModel> cast { get; set; }
    }
}
