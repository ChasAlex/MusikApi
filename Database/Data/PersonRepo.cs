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

        //Add new connection in UserArtist table
        public async Task AddUserArtistAsync(UserArtist userArtist)
        {
            var user = await _context.Users.FindAsync(userArtist.UserId);
            var artist = await _context.Artists.FindAsync(userArtist.ArtistId);
            if (user == null || artist == null)
            {
                return;
            }

            var existingConnection = await _context.UserArtists
                .Where(u => u.UserId == userArtist.UserId && u.ArtistId == userArtist.ArtistId)
                .FirstOrDefaultAsync();
            if (existingConnection != null)
            {
                return;
            }

            var newUserArtist = new UserArtist
            {
                UserId = userArtist.UserId,
                ArtistId = userArtist.ArtistId
            };

            _context.UserArtists.Add(newUserArtist);
            await _context.SaveChangesAsync();
        }

        //Add new connection in UserGenre table
        public async Task AddUserGenreAsync(UserGenre userGenre)
        {
            var user = await _context.Users.FindAsync(userGenre.UserId);
            var genre = await _context.Genres.FindAsync(userGenre.GenreId);
            if (user == null || genre == null)
            {
                return;
            }

            var existingConnection = await _context.UserGenres
                .Where(u => u.UserId == userGenre.UserId && u.GenreId == userGenre.GenreId)
                .FirstOrDefaultAsync();
            if (existingConnection != null)
            {
                return;
            }

            var newUserGenre = new UserGenre
            {
                UserId = userGenre.UserId,
                GenreId = userGenre.GenreId
            };

            _context.UserGenres.Add(newUserGenre);
            await _context.SaveChangesAsync();
        }

        //Add new connection in UserSong table
        public async Task AddUserSongAsync(UserSong userSong)
        {
            var user = await _context.Users.FindAsync(userSong.UserId);
            var song = await _context.Songs.FindAsync(userSong.SongId);
            if (user == null || song == null)
            {
                return;
            }

            var existingConnection = await _context.UserSongs
                .Where(u => u.UserId == userSong.UserId && u.SongId == userSong.SongId)
                .FirstOrDefaultAsync();
            if (existingConnection != null)
            {
                return;
            }

            var newUserSong = new UserSong
            {
                UserId = userSong.UserId,
                SongId = userSong.SongId
            };

            _context.UserSongs.Add(newUserSong);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}