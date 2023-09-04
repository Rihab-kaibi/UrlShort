using System.ComponentModel.DataAnnotations;

namespace UrlShort.Models
{
    public class UrlManagment : BaseEntity
    {
        public int Id { get; set; }
        public int Visits { get; set; }
        public string Name { get; set; }
        public string Url { get; set; } = "";
        public string ShortUrl { get; set; } = "";
         public string CustomShortUrl { get; set; } = "";
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public string IpAddress { get; set; } = "";
        public string Country { get; set; } = "";
        public byte[] QrCodeImage { get; set; } = new byte[0];
         //public DateTime Date { get; set; }
        public ICollection<InfoCounter> InfoCounter { get; set; }
        public ICollection<IpInfo> IpInfo { get; set; }

        public ICollection<Stat> Stat { get; set; }

       // [Required]
      //  public DateTime CreatedDate { get; set; }
    }
}
