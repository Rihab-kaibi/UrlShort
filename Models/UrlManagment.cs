namespace UrlShort.Models
{
    public class UrlManagment
    {
        public int Id { get; set; }
        public int Visits { get; set; }

        public string Url { get; set; } = "";
        public string ShortUrl { get; set; } = "";
        public string IpAddress { get; set; } = "";
        public string Country { get; set; } = "";
        // Navigation property for IpInfo
       
      
        public ICollection<IpInfo> IpInfo { get; set; }

        public ICollection<Stat> Stat { get; set; }


    }
}
