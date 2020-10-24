using System;
using System.Collections.Generic;
using System.Text;

namespace Concierto.Common.Models
{
    public class UserResponse
    {
        public string Token { get; set; }
        public string Expiration { get; set; }
        public User User { get; set; }
    }
}
