using System.Text.Json.Serialization;

namespace API.Models.ViewModels
{
    public class GetTopTracksByGenreViewmodel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

    }
}
