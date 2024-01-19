using System.Text.Json.Serialization;

namespace API.Models.DTOs.GetArtistInfoDTO
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

        [JsonPropertyName("image")]
        public List<Image> Image { get; set; }

        [JsonPropertyName("streamable")]
        public string Streamable { get; set; }

        [JsonPropertyName("ontour")]
        public string Ontour { get; set; }

        [JsonPropertyName("stats")]
        public Stats Stats { get; set; }

        [JsonPropertyName("similar")]
        public Similar Similar { get; set; }

        [JsonPropertyName("tags")]
        public Tags Tags { get; set; }

        [JsonPropertyName("bio")]
        public Bio Bio { get; set; }
    }

    public class Bio
    {
        [JsonPropertyName("links")]
        public Links Links { get; set; }

        [JsonPropertyName("published")]
        public string Published { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("content")]
        public string Content { get; set; }
    }

    public class Image
    {
        [JsonPropertyName("#text")]
        public string Text { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }
    }

    public class Link
    {
        [JsonPropertyName("#text")]
        public string Text { get; set; }

        [JsonPropertyName("rel")]
        public string Rel { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }
    }

    public class Links
    {
        [JsonPropertyName("link")]
        public Link Link { get; set; }
    }

    public class Root
    {
        [JsonPropertyName("artist")]
        public Artist Artist { get; set; }
    }

    public class Similar
    {
        [JsonPropertyName("artist")]
        public List<Artist> Artist { get; set; }
    }

    public class Stats
    {
        [JsonPropertyName("listeners")]
        public string Listeners { get; set; }

        [JsonPropertyName("playcount")]
        public string Playcount { get; set; }
    }

    public class Tag
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Tags
    {
        [JsonPropertyName("tag")]
        public List<Tag> Tag { get; set; }
    }


}
