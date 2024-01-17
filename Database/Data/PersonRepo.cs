using Database.Data.Interfaces;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    public class PersonRepo : IPersonRepo
    {

        private readonly MusicContext _context;

        public PersonRepo(MusicContext context)
        {
            _context = context;
        }



        public async Task<IReadOnlyList<Artist>> GetAllArtistsByPersonId(int id)
        {
            var artists = await _context.Artists
                .AsNoTracking()
                .Where(a => a.Id == id)
                .ToListAsync();

            return artists;
        }

        public async Task<IReadOnlyList<Genre>> GetAllGenresByPersonId(int id)
        {
            var personGenres = await _context.Genres
                .AsNoTracking()
                .Where(p => p.UserId == id)
                .ToListAsync();

            return personGenres;
        }

        public async Task<IReadOnlyList<Song>> GetAllSongsByPersonId(int id)
        {
            var songs = await _context.Songs
                 .AsNoTracking()
                 .Where(p => p.UserId == id)
                 .ToListAsync();

            return songs;
        }

        public async Task<IReadOnlyList<User>> GetAllUsers()
        {
            var persons = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            return persons;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}