using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TBP.Contracts.Authentication
{
    public class AuthRegistrationRequestModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
