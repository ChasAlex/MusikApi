using Database.Models;
using System.Text.Json;
using API.Models.DTOs.GetTopTracksByArtistDTO;
using API.Models.DTOs.GetTopTracksByGenreDTO;

namespace API.Services
{

    public interface IMusicServices
    {
        Task<Toptracks> getTopTrackByArtistAsync (string artist);
        Task<Tracks> getTopTracksByGenreAsync(string genre);
        Task<Models.DTOs.GetArtistInfoDTO.Artist> GetInfoArtistAsync(string artist);
    }
    
    
    
    public class MusicService:IMusicServices
    {
        private HttpClient _client;
        private string _apiKey = "9a0c3b501fb488574295f2402c2f8c05";

        //Konstruktor för att ta in client
        public MusicService(HttpClient client)
        {
            _client = client;
        }

        // Konstruktor för utan en client
        public MusicService():this (new HttpClient()){}


        // Hämtar Top låter för en artist
        public async Task<Toptracks> getTopTrackByArtistAsync(string artist)
        {
            var result = await _client.GetAsync($"https://ws.audioscrobbler.com/2.0/?method=artist.gettoptracks&artist={artist}&api_key={_apiKey}&format=json");
        
            result.EnsureSuccessStatusCode();

            Models.DTOs.GetTopTracksByArtistDTO.Root root = JsonSerializer.Deserialize<Models.DTOs.GetTopTracksByArtistDTO.Root>( await result.Content.ReadAsStringAsync() );

            return root.Toptracks;
        
        }

        //Hämtar top låtar för en tagg eller en genre
        public async Task<Tracks> getTopTracksByGenreAsync(string genre)
        {
            var result = await _client.GetAsync($"https://ws.audioscrobbler.com/2.0/?method=tag.gettoptracks&tag={genre}&api_key={_apiKey}&format=json");
            
            result.EnsureSuccessStatusCode();

            Models.DTOs.GetTopTracksByGenreDTO.Root root = JsonSerializer.Deserialize<Models.DTOs.GetTopTracksByGenreDTO.Root>( await result.Content.ReadAsStringAsync() ) ;

            return root.Tracks;
        }

        //Hämta info om en artist

        public async Task<Models.DTOs.GetArtistInfoDTO.Artist> GetInfoArtistAsync(string artist)
        {
            var result = await _client.GetAsync($"https://ws.audioscrobbler.com/2.0/?method=artist.getinfo&artist={artist}&api_key={_apiKey}&format=json");
            result.EnsureSuccessStatusCode();

            Models.DTOs.GetArtistInfoDTO.Root root = JsonSerializer.Deserialize<Models.DTOs.GetArtistInfoDTO.Root>(await result.Content.ReadAsStringAsync());

            return root.Artist;
        }




    }
}
