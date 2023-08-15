using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShort.Models

{
    public class Stat
    {
        public int Id { get; set; } 
  
        public string IpAddress { get; set; } = "";
        public string UserAgent { get; set; } = "";
        public string DeviceFamily { get; set; } = "";
        public string BrowserName { get; set; } = "";
        public string BrowserVersion { get; set; } = "";
        public string OperatingSystem { get; set; } = "";
        public string OperatingSystemVirsion { get; set; } = "";

        public int ShortUrlId { get; set; }
        public virtual UrlManagment ShortUrl { get; set; }

    }
}
