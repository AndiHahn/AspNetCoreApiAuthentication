namespace WebAppAuthentication
{
    public static class Constants
    {
        public static class ApiKeySection
        {
            public const string AUTHENTICATION_SCHEME = "ApiKeyAuthenticationScheme";
            public const string API_KEY = "123456789";
        }

        public static class BasicAuthSection
        {
            public const string AUTHENTICATION_SCHEME = "BasicAuthenticationScheme";
        }

        public static class JwtAuthSection
        {
            public const string AUTHENTICATION_SCHEME = "JwtBearerScheme";

            public static class UserRoles
            {
                public const string Admin = "Admin";
                public const string User = "User";
            }
        }
    }
}
