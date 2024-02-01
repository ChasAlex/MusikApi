using System.Text.Json.Serialization;

namespace Client.Models.Viewmodels
{
    public class GetTopTracksByGenreViewmodel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

    }
}
