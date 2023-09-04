export interface Stat {
    Id: number; 
  
    IpAddress: string;
    UserAgent: string;
    DeviceFamily: string;
    BrowserName: string;
    BrowserVersion: string;
    OperatingSystem: string;
    OperatingSystemVirsion: string;

    ShortUrlId: number;
   // export virtual UrlManagment ShortUrl { get; set; }
    CreatedDate: Date | string;


}