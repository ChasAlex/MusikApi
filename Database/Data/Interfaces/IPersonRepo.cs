using Database.Models;

namespace Database.Data.Interfaces
{
    public interface IPersonRepo
    {
        Task<User> GetUserByCredentials(string username, string password);
        Task CreateNewUser(string fullname, string username, string password);
        Task<IReadOnlyList<User>> GetAllUsers();
        Task<IReadOnlyList<Genre>> GetAllGenresByPersonId(int id);
        Task<IReadOnlyList<Artist>> GetAllArtistsByPersonId(int id);
        Task<IReadOnlyList<Artist>> GetAllArtistsNotConnectedByPersonId(int id);
        Task<IReadOnlyList<Song>> GetAllSongsByPersonId(int id);
        Task AddUserArtistAsync(UserArtist userArtist);
        Task<Artist> AddArtistbyNameAsync(string artistname);

        // I needed to change for the test to workIs tested and should not break anything. 
        //Task AddUserGenreAsync(UserGenre userGenre);
        Task<UserGenre> AddUserGenreAsync(UserGenre userGenre);
        Task AddUserSongAsync(UserSong userSong);
        Task SaveChanges();
    }
}
