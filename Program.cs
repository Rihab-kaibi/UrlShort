using Microsoft.EntityFrameworkCore;
using UAParser;
using UrlShort.Models;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Cors;
using System.Runtime;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connStr = builder.Configuration.GetConnectionString(name: "DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(optionsAction: options => options.UseSqlite(connStr));
builder.Services.AddCors(); //this line to enable CORS
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

//app.MapPost(pattern: "/shorturl", handler: async(UrlDto url, ApiDbContext db, HttpContext ctx) =>
//{
//    if (!Uri.TryCreate(url.Url, UriKind.Absolute, out var inputUrl))
//        return Results.BadRequest("Invalid url has been provided ");
app.MapPost("/shorturl", async (UrlDto url, ApiDbContext db, HttpContext ctx) =>
{
    if (!Uri.TryCreate(url.Url, UriKind.Absolute, out var inputUrl))
        return Results.BadRequest("Invalid url has been provided ");

    //craeting a short version of the provided Url
    var random = new Random();
    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890@abcdefghijklmopqrtsuvwxuz";
    var randomStr = new string(Enumerable.Repeat(chars, 8)
        .Select(x => x[random.Next(x.Length)]).ToArray());

    // Fetch IP information using api.findip.net
    using var httpClient = new HttpClient();
    var ipResponse = await httpClient.GetAsync("https://api.findip.net/ip/1.json");
   var ipData = await ipResponse.Content.ReadFromJsonAsync<IpInfo>();
   
    //var ipData = await ipResponse.Content.ReadAsAsync<FindIpResponse>();
    //var findIpResponse = await ipResponse.Content.ReadFromJsonAsync<FindIpResponse>();
    //var ipData = findIpResponse.IpInfo;
    
    //jdiida
    // Fetch IP information using api.findip.net
    ////var httpClient = httpClientFactory.CreateClient();
    ////var ipResponse = await httpClient.GetAsync("https://api.findip.net/ip/1.json");
    ////var ipData = await ipResponse.Content.ReadAsAsync<FindIpResponse>();
    ////maping a shortuRL WITH THE LONG URL 
    //var sUrl = new UrlManagment()
    //{
    //    Url = url.Url,
    //    ShortUrl = randomStr,
    //};
    // Save URL mapping in the database
    var sUrl = new UrlManagment
    {
        Url = url.Url,
        ShortUrl = randomStr,
        IpInfo = new IpInfo
        {
            IpAddress = ipData.IpAddress,
            Country = ipData.Country,
            ISP = ipData.ISP,
            UserType = ipData.UserType,
            Continent = ipData.Continent
        }
    };
    

    //saving the maping in the database 
    db.Urls.Add(sUrl);
    await db.SaveChangesAsync();
    //construct url
    var result = $"{ctx.Request.Scheme}://{ctx.Request.Host}/{sUrl.ShortUrl}";
    return Results.Ok(new UrlShortResponseDto()
    { Url = result });
});


app.MapFallback(async (ApiDbContext db, HttpContext ctx) =>
{
    var path = ctx.Request.Path.ToUriComponent().Trim('/');
    var urlMatch = await db.Urls.FirstOrDefaultAsync  (x=> 
    x.ShortUrl.Trim() == path.Trim());
    if (urlMatch == null)
        return Results.BadRequest(error: "Invalid Short Url");
    // else , var visits = urlMatch.Visits ; visits += 1 db.urls.update(urlmatch) db.savechangesasync()
    else
    {
        urlMatch.Visits++; // Increment the visit count
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
        await db.SaveChangesAsync(); // Save changes asynchronously
        return Results.Redirect(urlMatch.Url);

    }
});
app.Run();



class ApiDbContext : DbContext
{
    public virtual DbSet<UrlManagment> Urls { get; set; }
    public virtual DbSet<Stat> Stat { get; set; }
    public DbSet<IpInfo> IpInfos { get; set; }


    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Stat>()
            .HasOne(s => s.ShortUrl)
            .WithMany(u => u.Stat)
            .HasForeignKey(s => s.ShortUrlId);

        modelBuilder.Entity<UrlManagment>()
          .HasOne(u => u.IpInfo) // Define the relationship to IpInfo
          .WithMany(x => x.UrlManagment)
          .HasForeignKey(u => u.IpInfoId) // FK to IpInfo
          .OnDelete(DeleteBehavior.Restrict); // Optional: Set the delete behavior
    }

}
