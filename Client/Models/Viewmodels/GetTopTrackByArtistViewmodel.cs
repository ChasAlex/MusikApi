
using System.Text.Json.Serialization;

namespace Client.Models.Viewmodels
{
    public class GetTopTrackByArtistViewmodel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("playcount")]
        public string Playcount { get; set; }
    }
}
