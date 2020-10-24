using Concierto.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concierto.Common.Responses
{
    public class TokenResponse
    {
        public string Token { get; set; }

        public User User { get; set; }

        public DateTime Expiration { get; set; }

        public DateTime ExpirationLocal => Expiration.ToLocalTime();
    }

}
