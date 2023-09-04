namespace UrlShort.Models
{

    public class InfoCounter
    {
        public Guid Id { get; set; }
        public int Smartphone { get; set; }
        public int Laptop { get; set; }
        public int Tablet { get; set; }
        public int Other { get; set; }
        public int ShortUrlId { get; set; }
        public virtual UrlManagment ShortUrl { get; set; }
    }
}

