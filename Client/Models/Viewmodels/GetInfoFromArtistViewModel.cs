using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client.Models.Viewmodels
{
    public class GetInfoFromArtistViewModel
    {
        
        
            [JsonPropertyName("name")]
            public string Name { get; set; }


            [JsonPropertyName("playcount")]
            public string? Playcount { get; set; }

            [JsonPropertyName("summary")]
            public string? Summary { get; set; }
        
    }
}
