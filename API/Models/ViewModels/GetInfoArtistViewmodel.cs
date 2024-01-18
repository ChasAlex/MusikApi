using System.Text.Json.Serialization;

namespace API.Models.ViewModels
{
    public class GetInfoArtistViewmodel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("playcount")]
        public string Playcount { get; set; }

        [JsonPropertyName("summary")]
        public string Summary { get; set; }
    }
}
