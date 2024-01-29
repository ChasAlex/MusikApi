using Database.Models;

namespace Database.Data.Interfaces
{
    public interface IPersonRepo
    {
        Task<IReadOnlyList<User>> GetAllUsersAsync();
        Task<User> GetUserByCredentialsAsync(string username, string password);
        Task CreateNewUserAsync(string fullname, string username, string password);
        Task<IReadOnlyList<Artist>> GetAllArtistsByUserIdAsync(int id);
        Task<IReadOnlyList<Song>> GetAllSongsByUserIdAsync(int id);
        Task<IReadOnlyList<Genre>> GetAllGenresByUserIdAsync(int id);
        Task<IReadOnlyList<Genre>> GetAllGenresNotConnectedByUserIdAsync(int id);
        Task<IReadOnlyList<Artist>> GetAllArtistsNotConnectedByUserIdAsync(int id);
        Task<IReadOnlyList<Song>> GetAllSongsNotConnectedByUserIdAsync(int id);
        Task AddUserArtistAsync(UserArtist userArtist);

        // I needed to change for the test to workIs tested and should not break anything. 
        //Task AddUserGenreAsync(UserGenre userGenre);
        Task<UserGenre> AddUserGenreAsync(UserGenre userGenre);
        Task AddUserSongAsync(UserSong userSong);
        Task SaveChanges();
    }
}
