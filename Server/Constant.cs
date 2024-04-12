namespace Server
{
    public static class Constant
    {
        public const string Audience = "http://localhost:33742/";
        public const string Issuer = Audience;
        //public const string Audience = "http://localhost:33742/";
        public const string Secret = "not_too_short_secret_otherwise_it_might_error";

    }
}
