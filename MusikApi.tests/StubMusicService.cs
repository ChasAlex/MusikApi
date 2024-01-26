using API.Models.DTOs.GetTopTracksByArtistDTO;
using API.Models.DTOs.GetTopTracksByGenreDTO;
using API.Services;

namespace MusikApi.tests
{

    // TODO need to fix the using. 
    public class StubMusicService : IMusicServices
    {
        public async Task<API.Models.DTOs.GetArtistInfoDTO.Artist> GetInfoArtistAsync(string artist)
        {

            var artistInfo = new API.Models.DTOs.GetArtistInfoDTO.Artist
            {
                Name = "Markoolio",
                Stats = new API.Models.DTOs.GetArtistInfoDTO.Stats
                {
                    Playcount = "899076"
                },
                Bio = new API.Models.DTOs.GetArtistInfoDTO.Bio
                {
                    Summary = "Summary"
                }

            };


            return artistInfo;
        }


        public async Task<Toptracks> GetTopTrackByArtistAsync(string artist)
        {

            var toptracksByArtist = new List<API.Models.DTOs.GetTopTracksByArtistDTO.Track>
            {
                new API.Models.DTOs.GetTopTracksByArtistDTO.Track
                {
                    Name = "Dancing Queen",
                    Playcount = "11415284"
                },
                 new API.Models.DTOs.GetTopTracksByArtistDTO.Track
                {
                    Name = "Gimme! Gimme! Gimme! (A Man After Midnight)",
                    Playcount = "7327889"
                },
                  new API.Models.DTOs.GetTopTracksByArtistDTO.Track
                {
                    Name = "Mamma Mia",
                    Playcount = "5834820"
                },
                new API.Models.DTOs.GetTopTracksByArtistDTO.Track
                {
                    Name = "Lay All Your Love on Me",
                    Playcount = "5701600"
                },
            };


            return new Toptracks
            {
                Track = toptracksByArtist
            };

        }


        public async Task<Tracks> GetTopTracksByGenreAsync(string genre)
        {

            var TopTracksByGenre = new List<API.Models.DTOs.GetTopTracksByGenreDTO.Track>
            {
                new API.Models.DTOs.GetTopTracksByGenreDTO.Track
                {
                    Name = "Lush Life"
                },
                new API.Models.DTOs.GetTopTracksByGenreDTO.Track
                {
                    Name = "Blow Your Mind (Mwah)"
                },
                new API.Models.DTOs.GetTopTracksByGenreDTO.Track
                {
                    Name = "Bad At Love"
                },
                new API.Models.DTOs.GetTopTracksByGenreDTO.Track
                {
                    Name = "Hotter Than Hell"
                }

            };

            return new Tracks
            {
                Track = TopTracksByGenre
            };

        }


    }
}

