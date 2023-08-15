namespace UrlShort.Models
{
    public class IpInfo
    {
        public int IpInfoId { get; set; }
        public string IpAddress { get; set; } = "";
        public string Country { get; set; } = "";
        public string ISP { get; set; } = "";
        public string UserType { get; set; } = "";
        public string Continent { get; set; } = "";
        public int ShortUrlId { get; set; }
        public virtual UrlManagment ShortUrl { get; set; }
    }
}
