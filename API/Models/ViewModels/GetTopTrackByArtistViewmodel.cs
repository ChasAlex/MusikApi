using API.Models.DTOs.GetTopTracksByArtistDTO;
using System.Text.Json.Serialization;

namespace API.Models.ViewModels
{
    public class GetTopTrackByArtistViewmodel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("playcount")]
        public string Playcount { get; set; }
    }
}
