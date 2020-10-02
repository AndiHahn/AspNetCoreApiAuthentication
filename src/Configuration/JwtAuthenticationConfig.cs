﻿namespace WebAppAuthentication.Configuration
{
    public class JwtAuthenticationConfig
    {
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }
}
