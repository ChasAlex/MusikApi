using Database.Models;

namespace Database.Data.Interfaces
{
    public interface IPersonRepo
    {

        Task<IReadOnlyList<User>> GetAllUsers();
        Task<IReadOnlyList<Genre>> GetAllGenresByPersonId(int id);
        Task<IReadOnlyList<Artist>> GetAllArtistsByPersonId(int id);
        Task<IReadOnlyList<Song>> GetAllSongsByPersonId(int id);
        Task SaveChanges();


        // need to add later = Koppla en person till en ny genre, artist och låt



    }
}
