using System.Text.Json.Serialization;

namespace API.Models.DTOs.GetTopTracksByArtistDTO
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class Artist
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("mbid")]
        public string Mbid { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Attr
    {
        [JsonPropertyName("rank")]
        public string Rank { get; set; }

        [JsonPropertyName("artist")]
        public string Artist { get; set; }

        [JsonPropertyName("page")]
        public string Page { get; set; }

        [JsonPropertyName("perPage")]
        public string PerPage { get; set; }

        [JsonPropertyName("totalPages")]
        public string TotalPages { get; set; }

        [JsonPropertyName("total")]
        public string Total { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("#text")]
        public string Text { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("toptracks")]
        public Toptracks Toptracks { get; set; }
    }

    public class Toptracks
    {
        [JsonPropertyName("track")]
        public List<Track> Track { get; set; }

        [JsonPropertyName("@attr")]
        public Attr Attr { get; set; }
    }

    public class Track
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("playcount")]
        public string Playcount { get; set; }

        [JsonPropertyName("listeners")]
        public string Listeners { get; set; }

        [JsonPropertyName("mbid")]
        public string Mbid { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("streamable")]
        public string Streamable { get; set; }

        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }

        [JsonPropertyName("image")]
        public List<Image> Image { get; set; }

        [JsonPropertyName("@attr")]
        public Attr Attr { get; set; }
    }




}
