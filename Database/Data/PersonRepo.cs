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

        //Gets all registered users
        public async Task<IReadOnlyList<User>> GetAllUsersAsync()
        {
            var persons = await _context.Users
                .AsNoTracking()
                .ToListAsync();

            return persons;
        }

        //User login: Checks matching credentials and returns the connected user info
        public async Task<User> GetUserByCredentialsAsync(string username, string password)
        {
            var user = await _context.Credentials
                .Where(u => u.Username == username && u.Password == password)
                .Join(_context.Users, credential => credential.UserId, user => user.Id, (credential, user) => user)
                .SingleOrDefaultAsync();

            return user;
        }

        //User signup: Checks if username is taken, then creates new user and credentials
        public async Task CreateNewUserAsync(string fullname, string username, string password)
        {
            try
            {
                var userExists = await _context.Credentials.AnyAsync(c => c.Username == username);
                if (userExists)
                {
                    throw new InvalidOperationException("Username already exists.");
                }
                else
                {
                    var newUser = new User { Fullname = fullname };
                    _context.Users.AddAsync(newUser);
                    await _context.SaveChangesAsync();

                    var newCredential = new Credential { UserId = newUser.Id, Username = username, Password = password };
                    _context.Credentials.Add(newCredential);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync($"Error: {ex.Message}");
                throw;
            }
        }

        //Gets all users favorite artists
        public async Task<IReadOnlyList<Artist>> GetAllArtistsByUserIdAsync(int id)
        {
            var artists = await _context.UserArtists
                .AsNoTracking()
                .Where(u => u.UserId == id)
                .Select(u => u.Artist)
                .ToListAsync();

            return artists;
        }

        //Gets all users favorite songs
        public async Task<IReadOnlyList<Song>> GetAllSongsByUserIdAsync(int id)
        {
            var songs = await _context.UserSongs
                .AsNoTracking()
                .Where(u => u.UserId == id)
                .Select(u => u.Song)
                .ToListAsync();

            return songs;
        }

        //Gets all users favorite genres
        public async Task<IReadOnlyList<Genre>> GetAllGenresByUserIdAsync(int id)
        {
            var personGenres = await _context.UserGenres
                .AsNoTracking()
                .Where(u => u.UserId == id)
                .Select(u => u.Genre)
                .ToListAsync();

            return personGenres;
        }

        //Gets all artists not favorited by user (yet)
        public async Task<IReadOnlyList<Artist>> GetAllArtistsNotConnectedByUserIdAsync(int id)
        {
            var notConnectedArtists = await _context.Artists
            .AsNoTracking()
            .Where(a => !_context.UserArtists
                        .Any(u => u.UserId == id && u.ArtistId == a.Id))
        .ToListAsync();

            return notConnectedArtists;
        }

        //Gets all songs not favorited by user (yet)
        public async Task<IReadOnlyList<Song>> GetAllSongsNotConnectedByUserIdAsync(int id)
        {
            var notConnectedSongs = await _context.Songs
            .AsNoTracking()
            .Where(a => !_context.UserSongs
                        .Any(u => u.UserId == id && u.SongId == a.Id))
        .ToListAsync();

            return notConnectedSongs;
        }

        //Gets all genres not favorited by user (yet)
        public async Task<IReadOnlyList<Genre>> GetAllGenresNotConnectedByUserIdAsync(int id)
        {
            var notConnectedGenres = await _context.Genres
            .AsNoTracking()
            .Where(a => !_context.UserGenres
                        .Any(u => u.UserId == id && u.GenreId == a.Id))
        .ToListAsync();

            return notConnectedGenres;
        }

        //Adds new connection between user and artist
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

        //Adds new connection between user and song
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

        //Adds new connection between user and genre
        public async Task<UserGenre> AddUserGenreAsync(UserGenre userGenre)
        {
            var user = await _context.Users.FindAsync(userGenre.UserId);
            var genre = await _context.Genres.FindAsync(userGenre.GenreId);
            if (user == null || genre == null)
            {
                return null;
            }

            var existingConnection = await _context.UserGenres
                .Where(u => u.UserId == userGenre.UserId && u.GenreId == userGenre.GenreId)
                .FirstOrDefaultAsync();
            if (existingConnection != null)
            {
                return null;
            }

            var newUserGenre = new UserGenre
            {
                UserId = userGenre.UserId,
                GenreId = userGenre.GenreId
            };

            _context.UserGenres.Add(newUserGenre);
            await _context.SaveChangesAsync();

            return newUserGenre;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}