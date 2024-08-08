namespace NotaryOfficeProject.Helpers
{
    public class JWT
    {
        public String? Key { get; set; }
        public String? Issuer { get; set; }
        public String? Audience { get; set; }
        public double DurationInDays { get; set; }
    }
}
