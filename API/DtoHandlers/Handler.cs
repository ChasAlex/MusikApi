using Database.Dtos;
using Database.Models;

namespace API.DtoHandlers
{
    public static class Handler
    {

        public static IEnumerable<GenreDto> CreateGenreDto(IReadOnlyList<Genre> genres)
        {
            return genres.Select(g => new GenreDto
            {
                Id = g.Id,
                Title = g.Title

            });
        }


        public static IEnumerable<ArtistDto> CreateArtistDto(IReadOnlyList<Artist> artists)
        {
            return artists.Select(a => new ArtistDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Country = a.Country

            });
        }


        public static IEnumerable<SongDto> CreateSongDto(IReadOnlyList<Song> songs)
        {
            return songs.Select(s => new SongDto
            {
                Id = s.Id,
                Name = s.Name,

            });
        }


        public static IEnumerable<UserDto> CreateUserDto(IReadOnlyList<User> persons)
        {
            return persons.Select(p => new UserDto
            {
                Id = p.Id,
                Fullname = p.Fullname

            });
        }

    }
}
