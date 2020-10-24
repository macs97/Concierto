using Concierto.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concierto.Common.Responses
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
        public User User { get; set; }
    }
}
