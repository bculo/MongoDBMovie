namespace TBP.Options
{
    public sealed class SecurityOptions
    {
        public int PasswordIterations { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int AccessExpiration { get; set; }
        public int SaltLength { get; set; }
        public int DerivedKeyLength { get; set; }
        public string PasswordDelimiter { get; set; }
    }
}
