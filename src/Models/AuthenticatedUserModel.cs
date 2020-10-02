using System;

namespace WebAppAuthentication.Models
{
    public class AuthenticatedUserModel
    {
        public string Token { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
