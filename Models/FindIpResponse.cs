namespace UrlShort.Models
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class FindIpResponse
    {
        [JsonProperty("city")]
        public City City { get; set; }

        [JsonProperty("continent")]
        public Continent Continent { get; set; }

        [JsonProperty("country")]
        public Country Country { get; set; }

        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("subdivisions")]
        public Subdivision[] Subdivisions { get; set; }

        [JsonProperty("traits")]
        public Traits Traits { get; set; }
    }

    public partial class City
    {
        [JsonProperty("geoname_id")]
        public long GeonameId { get; set; }

        [JsonProperty("names")]
        public CityNames Names { get; set; }
    }

    public partial class CityNames
    {
        [JsonProperty("de")]
        public string De { get; set; }

        [JsonProperty("en")]
        public string En { get; set; }

        [JsonProperty("es")]
        public string Es { get; set; }

        [JsonProperty("fa")]
        public string Fa { get; set; }

        [JsonProperty("fr")]
        public string Fr { get; set; }

        [JsonProperty("ja")]
        public string Ja { get; set; }

        [JsonProperty("ko")]
        public string Ko { get; set; }

        [JsonProperty("pt-BR")]
        public string PtBr { get; set; }

        [JsonProperty("ru")]
        public string Ru { get; set; }

        [JsonProperty("zh-CN")]
        public string ZhCn { get; set; }
    }

    public partial class Continent
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("geoname_id")]
        public long GeonameId { get; set; }

        [JsonProperty("names")]
        public CityNames Names { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("geoname_id")]
        public long GeonameId { get; set; }

        [JsonProperty("is_in_european_union")]
        public bool IsInEuropeanUnion { get; set; }

        [JsonProperty("iso_code")]
        public string IsoCode { get; set; }

        [JsonProperty("names")]
        public CityNames Names { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        [JsonProperty("weather_code")]
        public string WeatherCode { get; set; }
    }

    public partial class Subdivision
    {
        [JsonProperty("geoname_id")]
        public long GeonameId { get; set; }

        [JsonProperty("iso_code", NullValueHandling = NullValueHandling.Ignore)]
         public long? IsoCode { get; set; }

        [JsonProperty("names")]
        public SubdivisionNames Names { get; set; }
    }

    public partial class SubdivisionNames
    {
        [JsonProperty("en")]
        public string En { get; set; }

        [JsonProperty("fr", NullValueHandling = NullValueHandling.Ignore)]
        public string Fr { get; set; }
    }

    public partial class Traits
    {
        [JsonProperty("autonomous_system_number")]
        public long AutonomousSystemNumber { get; set; }

        [JsonProperty("autonomous_system_organization")]
        public string AutonomousSystemOrganization { get; set; }

        [JsonProperty("connection_type")]
        public string ConnectionType { get; set; }

        [JsonProperty("isp")]
        public string Isp { get; set; }

        [JsonProperty("user_type")]
        public string UserType { get; set; }
    }
}
