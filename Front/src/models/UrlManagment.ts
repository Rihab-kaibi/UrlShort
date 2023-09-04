export interface UrlManagment   {
    Id: number;
    Visits: number;
    Name: string;
    Url: string;
    ShortUrl: string;
    CustomShortUrl: string;

    IpAddress: string;
    Country: string;
    QrCodeImage: string;
    // Navigation property for IpInfo


   // IpInfo: IpInfo[];

    //Stat: Stat[];

    CreatedDate: Date | string;
}