namespace UrlShort.Models
{
    public class IpInfo
    {
        public int IpInfoId { get; set; }
        public string IpAddress { get; set; } = "";
        public string Country { get; set; } = "";
        public string City { get; set; } = "";
        public string Isp { get; set; } = "";
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string UserType { get; set; } = "";
        public string Continent { get; set; } = "";     
        public int ShortUrlId { get; set; }
        public virtual UrlManagment ShortUrl { get; set; }
    }
}
