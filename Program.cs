using QRCoder;
using Microsoft.EntityFrameworkCore;
using UAParser;
using UrlShort.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Cors;
using System.Runtime;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using AutoMapper;
using Newtonsoft.Json;

using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using static QRCoder.PayloadGenerator;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Drawing.Printing;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connStr = builder.Configuration.GetConnectionString(name: "DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(optionsAction: options => options.UseSqlite(connStr));
builder.Services.AddCors(); //this line to enable CORS
                            // Configure HttpClient in the DI container
var app = builder.Build();




// this block to configure CORS globally
app.UseCors(builder =>
{
    builder.AllowAnyOrigin() //  Angular app's URL
           .AllowAnyHeader()
           .AllowAnyMethod();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

byte[] GenerateQrCodeImage(string data)
{

    using var qrGenerator = new QRCodeGenerator();
    var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);

    using var qrCode = new QRCode(qrCodeData); // Using QRCode class

    var qrCodeImage = qrCode.GetGraphic(20, Color.Black, Color.White, true);

    using var memoryStream = new MemoryStream();
    qrCodeImage.Save(memoryStream, ImageFormat.Png);

    return memoryStream.ToArray();
}

app.MapGet("/qrcodeimage/{shortUrl}", async (string shortUrl, ApiDbContext db, HttpContext ctx) =>
{
    var urlMatch = await db.Urls.FirstOrDefaultAsync(x =>
        x.ShortUrl.Trim() == shortUrl.Trim());

    if (urlMatch == null)
        return Results.NotFound();

    if (urlMatch.QrCodeImage.Length == 0)
    {
        var urlToQr = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{urlMatch.ShortUrl}";
        var qrCodeData = GenerateQrCodeImage(urlToQr);
        urlMatch.QrCodeImage = qrCodeData;
        await db.SaveChangesAsync();
    }

    ctx.Response.ContentType = "image/png";
    await ctx.Response.Body.WriteAsync(urlMatch.QrCodeImage);

    return Results.Ok();
});

app.MapPost("/shorturl", async (UrlDto url, ApiDbContext db, HttpContext ctx) =>
{
    if (!Uri.TryCreate(url.Url, UriKind.Absolute, out var inputUrl))
        return Results.BadRequest("Invalid url has been provided ");
    ScrapeInfo si = await DataScraping.Scrape(url.Url);

    //craeting a short version of the provided Url
    var random = new Random();
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@abcdefghijklmopqrtsuvwxuz";
    var randomStr = new string(Enumerable.Repeat(chars, 8)
        .Select(x => x[random.Next(x.Length)]).ToArray());

    var sUrl = new UrlManagment
    {
        Url = url.Url,
        ShortUrl = randomStr,
        Name = url.Name,
        Description = si.Description,
        Title = si.Title

    };
    var result = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{sUrl.ShortUrl}";

    var qrCodeData = GenerateQrCodeImage(result);

    sUrl.QrCodeImage = qrCodeData;

    db.Urls.Add(sUrl);

    await db.SaveChangesAsync();

    //construct url
    return Results.Ok(new UrlShortResponseDto()
    { Url = result });
});

app.MapPost("/bulkshorten", async (BulkUrlDto bulkUrls, ApiDbContext db, HttpContext ctx) =>
{
    var shortenedUrls = new List<UrlShortResponseDto>();

    foreach (var urlDto in bulkUrls.Urls)
    {
        if (!Uri.TryCreate(urlDto.Url, UriKind.Absolute, out var inputUrl))
        {
            // Invalid URL, skip and continue to the next URL
            continue;
        }

        ScrapeInfo si = await DataScraping.Scrape(urlDto.Url);

        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@abcdefghijklmopqrtsuvwxuz";
        var randomStr = new string(Enumerable.Repeat(chars, 8)
            .Select(x => x[random.Next(x.Length)]).ToArray());

        var sUrl = new UrlManagment
        {
            Url = urlDto.Url,
            ShortUrl = randomStr,
            Name = urlDto.Name,
            Description = si.Description,
            Title = si.Title
        };

        var result = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{sUrl.ShortUrl}";

        var qrCodeData = GenerateQrCodeImage(result);

        sUrl.QrCodeImage = qrCodeData;

        db.Urls.Add(sUrl);

        await db.SaveChangesAsync();

        shortenedUrls.Add(new UrlShortResponseDto { Url = result });
    }

    return Results.Ok(shortenedUrls);
});

app.MapGet("/api/allUseragentinfo", async (ApiDbContext db) =>
{
    var userAgentInfo = await db.Stat
        .GroupBy(s => s.DeviceFamily)
        .Select(g => new
        {
            DeviceFamily = g.Key,
            Count = g.Count()
        })
        .ToListAsync();

    var infoCounter = userAgentInfo.ToDictionary(
        u => u.DeviceFamily,  // Key: DeviceFamily
        u => u.Count         // Value: Count
    );

    return Results.Ok(infoCounter);
});

app.MapGet("/api/deviceinfo", async (ApiDbContext dbContext, string url) =>
{
    var userAgents = await dbContext.Stat
        .Where(s => s.ShortUrl.Url == url)
        .Select(s => s.DeviceFamily)
        .ToListAsync();

    int smartphoneCount = 0;
    int laptopCount = 0;
    int tabletCount = 0;
    int otherCount = 0;

    foreach (var deviceFamily in userAgents)
    {
        // Implement logic to categorize user agents into device categories
        // You can use more advanced categorization logic here.

        if (deviceFamily.Contains("Smartphone"))
        {
            smartphoneCount++;
        }
        else if (deviceFamily.Contains("Laptop"))
        {
            laptopCount++;
        }
        else if (deviceFamily.Contains("Tablet"))
        {
            tabletCount++;
        }
        else
        {
            otherCount++;
        }
    }

    var infoCounter = new InfoCounter
    {
        Smartphone = smartphoneCount,
        Laptop = laptopCount,
        Tablet = tabletCount,
        Other = otherCount
    };
    
    return Results.Ok(infoCounter);
});

app.MapPost("/customshorturl", async (UrlDto url, ApiDbContext db, HttpContext ctx) =>
{
    if (!Uri.TryCreate(url.Url, UriKind.Absolute, out var inputUrl))
        return Results.BadRequest("Invalid URL has been provided.");

    if (string.IsNullOrEmpty(url.CustomShortUrl))
        return Results.BadRequest("Custom short code is required for custom URL shortening.");

    if (url.CustomShortUrl.Length < 5)
        return Results.BadRequest("Custom short code must be at least 5 characters long!");

    var existingCustomShortUrl = await db.Urls.FirstOrDefaultAsync(u => u.ShortUrl == url.CustomShortUrl);
    if (existingCustomShortUrl != null)
    {
        return Results.BadRequest("Custom short code is already in use.");
    }
    ScrapeInfo si = await DataScraping.Scrape(url.Url);

    var sUrl = new UrlManagment
    {
        Url = url.Url,
        ShortUrl = url.CustomShortUrl,
        Name = url.Name,
        Description = si.Description,
        Title = si.Title

    };

    var result = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{sUrl.ShortUrl}";

    var qrCodeData = GenerateQrCodeImage(result);
    sUrl.QrCodeImage = qrCodeData;

    db.Urls.Add(sUrl);
    await db.SaveChangesAsync();


    return Results.Ok(new UrlShortResponseDto()
    {
        Url = result
    });
});

app.MapGet("/api/visitcounts", async (ApiDbContext db) =>
{
    var visitCounts = await db.Urls
       .GroupBy(s => new { Year = s.CreatedDate.Year, Month = s.CreatedDate.Month })
       .Select(g => new
       {
           Year = g.Key.Year,
           Month = g.Key.Month,
           Count = g.Count()
       })
       .OrderBy(g => g.Year)
       .ThenBy(g => g.Month)
       .ToListAsync();

    return Results.Ok(visitCounts);
}
);
app.MapGet("/api/urls", async (ApiDbContext db, int page = 1, int pageSize = 10, string orderBy = "latest") =>
{
    // Calculate the number of items to skip based on the page number and page size
    var skipAmount = (page - 1) * pageSize;

    // Define a query to retrieve URLs based on the orderBy parameter
    var query = orderBy switch
    {
        // "latest" => db.Urls.OrderByDescending(u => u.Id),
        "mostVisited" => db.Urls.OrderByDescending(u => u.Visits),
        _ => db.Urls.OrderByDescending(u => u.Visits) // Default to ordering by the latest if orderBy is invalid
    };

    // Query the database with pagination and ordering
    var urlData = await query
        .Skip(skipAmount)
        .Take(pageSize)
        .Select(u => new
        {
            u.Id,
            u.Visits,
            u.ShortUrl,
            u.Url
        })
        .ToListAsync();

    return Results.Ok(urlData);
});

app.MapGet("/visits/{shortUrl}", async (string shortUrl, ApiDbContext db) =>
{
    var urlMatch = await db.Urls.FirstOrDefaultAsync(x =>
        x.ShortUrl.Trim() == shortUrl.Trim());

    if (urlMatch == null)
        return Results.NotFound();
    var creationDate = urlMatch.CreatedDate;

    return Results.Ok(urlMatch.Visits);
});

app.MapGet("/api/details/{shortUrl}", async (string shortUrl, ApiDbContext db) =>
{
    var urlMatch = await db.Urls.FirstOrDefaultAsync(x =>
        x.ShortUrl.Trim() == shortUrl.Trim());

    if (urlMatch == null)
        return Results.NotFound();

    // You can construct an object containing the required details here
    var urlDetails = new
    {
        Title = urlMatch.Title,
        Description = urlMatch.Description,
        LongUrl = urlMatch.Url,
        ShortUrl = urlMatch.ShortUrl,
        VisitCount = urlMatch.Visits
    };

    return Results.Ok(urlDetails);
});

app.MapGet("/api/url/{shortUrlId}", async (int shortUrlId, ApiDbContext db) =>
{
    var urlMatch = await db.Urls.FirstOrDefaultAsync(x => x.Id == shortUrlId);

    if (urlMatch == null)
        return Results.NotFound();

    var urlDetails = new
    {
        Title = urlMatch.Title,
        Description = urlMatch.Description,
        LongUrl = urlMatch.Url,
        ShortUrl = urlMatch.ShortUrl,
        VisitCount = urlMatch.Visits
    };

    return Results.Ok(urlDetails);
});

app.MapGet("/api/info-counter/{shortUrlId}", async (int shortUrlId, ApiDbContext db, HttpContext httpContext) =>
{
    var infoCounters = await db.InfoCounters
        .Where(ic => ic.ShortUrlId == shortUrlId)
        .ToListAsync();

    if (infoCounters == null)
    {
        httpContext.Response.StatusCode = 404;
        return;
    }

    await httpContext.Response.WriteAsJsonAsync(infoCounters);
});
 
app.MapGet("/api/urls-with-geolocation", async (HttpContext context, ApiDbContext dbContext) =>
{
    var urlsWithGeolocation = await dbContext.Urls
        .Select(u => new
        {
            Id = u.Id,
            Url = u.Url,
            ShortUrl = u.ShortUrl,
            IpInfo = u.IpInfo.Select(ip => new
            {
                City = ip.City,
                Country = ip.Country,
                Continent = ip.Continent,
                Latitude=ip.Latitude,
                Longitude = ip.Longitude
            }).ToList()
         })
        .ToListAsync();

    await context.Response.WriteAsJsonAsync(urlsWithGeolocation);
});

app.MapGet("/api/country-visits-summary", async (HttpContext context, ApiDbContext dbContext) =>
{
    var countryVisitSummary = await dbContext.IpInfos
        .Where(ip => !string.IsNullOrEmpty(ip.Country))  
        .GroupBy(ip => ip.Country)
        .Select(group => new
        {
            Country = group.Key,
            TotalVisits = group.Sum(ip => ip.ShortUrl.Visits)  
        })
        .ToListAsync();

    await context.Response.WriteAsJsonAsync(countryVisitSummary);
});


app.MapFallback(async (ApiDbContext db, HttpContext ctx) =>
{
    var path = ctx.Request.Path.ToUriComponent().Trim('/');
    var urlMatch = await db.Urls.FirstOrDefaultAsync(x =>
    x.ShortUrl.Trim() == path.Trim());
    if (urlMatch == null)
        return Results.BadRequest(error: "Invalid Short Url");

    // Increment the visit count
    urlMatch.Visits++;

    db.Urls.Update(urlMatch); // Update the entity in the database

    // Parse user agent string
    var userAgentString = ctx.Request.Headers["User-Agent"].ToString();
    var uaParser = Parser.GetDefault();
    var clientInfo = uaParser.Parse(userAgentString);

    // Store visitor information including parsed user agent details in Stat table
    var stat = new Stat
    {
        ShortUrlId = urlMatch.Id,
        //IpAddress = ctx.Connection.RemoteIpAddress.ToString():
        //This sets the IpAddress property of the Stat entity to the remote IP address of the visitor.
        //The ctx.Connection.RemoteIpAddress represents the IP address of the client making the request.
        IpAddress = ctx.Connection.RemoteIpAddress.ToString(),
        UserAgent = userAgentString,
        DeviceFamily = clientInfo.Device.Family,
        BrowserName = clientInfo.UA.Family,
        BrowserVersion = clientInfo.UA.Major + "." + clientInfo.UA.Minor,
        OperatingSystem = clientInfo.OS.Family
    };
    db.Stat.Add(stat);
    InfoCounter infoCounter = await db.InfoCounters.FirstOrDefaultAsync(x => x.ShortUrl == urlMatch);
    if (infoCounter == null)
    {
        var ic = new InfoCounter
        {
            Id = new Guid(),
            ShortUrlId = urlMatch.Id
        };

        if (userAgentString.Contains("Android"))
        {
            if (userAgentString.Contains("Mobile"))
            {
                ic.Smartphone += 1;
            }
            else
                ic.Tablet += 1;
        }
        else if (userAgentString.Contains("iPhone"))
        {
            ic.Smartphone += 1;
        }
        else if (userAgentString.Contains("Windows"))
        {
            ic.Laptop += 1;

        }
        else
        {
            ic.Other += 1;
        }
        db.InfoCounters.Add(ic);

    }
    else
    {


        if (userAgentString.Contains("Android"))
        {
            if (userAgentString.Contains("Mobile"))
            {
                infoCounter.Smartphone += 1;
            }
            else
                infoCounter.Tablet += 1;
        }
        else if (userAgentString.Contains("iPhone"))
        {
            infoCounter.Smartphone += 1;
        }
        else if (userAgentString.Contains("Windows"))
        {
            infoCounter.Laptop += 1;

        }
        else
        {
            infoCounter.Other += 1;
        }
        db.InfoCounters.Update(infoCounter);
    }
    // Fetch IP information using api.findip.net
    FindIpResponse ipData = await Ipconfiguration.getIpInfo("197.240.38.150");
    IMapper mapper = Mapper.MapObject();
    IpInfo ipInfo = mapper.Map<IpInfo>(ipData);
    try
    {
        ipInfo.ShortUrlId = urlMatch.Id;
        db.IpInfos.Add(ipInfo);
    }
    catch (Exception ex)
    {
        throw new Exception(ex.InnerException.Message);
    }
    await db.SaveChangesAsync();

    return Results.Redirect(urlMatch.Url);

});







app.Run();
static class DataScraping
{
    public static async Task<ScrapeInfo> Scrape(string url)
    {
        ScrapeInfo scrapeInfo = new();
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(url);

            //GET Method
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {

                string htmlContent = await response.Content.ReadAsStringAsync();
                // Load the HTML into HtmlAgilityPack's HtmlDocument
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);
                // Extract the title
                var titleNode = doc.DocumentNode.SelectSingleNode("//title");
                string title = titleNode != null ? titleNode.InnerText : "No title found";

                // Extract the meta description
                var metaDescriptionNode = doc.DocumentNode.SelectSingleNode("//meta[@name='description']");
                string metaDescription = metaDescriptionNode != null ? metaDescriptionNode.GetAttributeValue("content", "") : "No meta description found";
                scrapeInfo.Title = title;
                scrapeInfo.Description = metaDescription;
            }
            else
            {
                Console.WriteLine("Internal server Error");
                scrapeInfo = null;
            }

        }
        return scrapeInfo;
    }

}

static class Ipconfiguration
{
    public static async Task<FindIpResponse> getIpInfo(string ip)
    {
        FindIpResponse findIpResponse = new();
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri($"https://api.findip.net/{ip}/?token=0b85d68e4ab84e69ad31a2f0ef3f678c");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //GET Method
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            if (response.IsSuccessStatusCode)
            {
                findIpResponse = await response.Content.ReadAsAsync<FindIpResponse>();
            }
            else
            {
                Console.WriteLine("Internal server Error");
                findIpResponse = null;
            }

        }
        return findIpResponse;
    }

}

static class Mapper
{
    public static IMapper MapObject()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<FindIpResponse, IpInfo>()
                 .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Names.En))
                 .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Location.Latitude))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Location.Longitude))
                 .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.Country.Names.En))
                 .ForMember(dest => dest.UserType, opt => opt.MapFrom(src => src.Traits.UserType))
                 .ForMember(dest => dest.Isp, opt => opt.MapFrom(src => src.Traits.Isp))
                 .ForMember(dest => dest.Continent, opt => opt.MapFrom(src => src.Continent.Names.En));
        });

        IMapper mapper = config.CreateMapper();
        return mapper;
        // Map the data

    }


}

class ApiDbContext : DbContext
{
    public virtual DbSet<UrlManagment> Urls { get; set; }
    public virtual DbSet<Stat> Stat { get; set; }
    public DbSet<IpInfo> IpInfos { get; set; }
    //public virtual DbSet<FindIpResponse> FindIpResponse { get; set; }
    public DbSet<InfoCounter> InfoCounters { get; set; }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<InfoCounter>()
            .HasOne(s => s.ShortUrl)
            .WithMany(u => u.InfoCounter)
            .HasForeignKey(s => s.ShortUrlId);
        modelBuilder.Entity<Stat>()
            .HasOne(s => s.ShortUrl)
            .WithMany(u => u.Stat)
            .HasForeignKey(s => s.ShortUrlId);

        modelBuilder.Entity<IpInfo>()
          .HasOne(u => u.ShortUrl) // Define the relationship to IpInfo
          .WithMany(x => x.IpInfo)
          .HasForeignKey(u => u.ShortUrlId) // FK to IpInfo
          .OnDelete(DeleteBehavior.Restrict); // Optional: Set the delete behavior
    }
    public override int SaveChanges()
    {
        var entities = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added && e.Entity is BaseEntity)
            .ToList();

        foreach (var entityEntry in entities)
        {
            ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
        }

        return base.SaveChanges();
    }
}
