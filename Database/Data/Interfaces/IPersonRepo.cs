using Database.Models;

namespace Database.Data.Interfaces
{
    public interface IPersonRepo
    {
        Task<User> GetUserByCredentials(string username, string password);
        Task<IReadOnlyList<User>> GetAllUsers();
        Task<IReadOnlyList<Genre>> GetAllGenresByPersonId(int id);
        Task<IReadOnlyList<Artist>> GetAllArtistsByPersonId(int id);
        Task<IReadOnlyList<Song>> GetAllSongsByPersonId(int id);
        Task AddUserArtistAsync(UserArtist userArtist);
        Task AddUserGenreAsync(UserGenre userGenre);
        Task AddUserSongAsync(UserSong userSong);
        Task SaveChanges();
    }
}
