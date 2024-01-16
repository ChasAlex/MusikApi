using Database.Data;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Handlers
{
    public static class DbHelper
    {

        public static async Task<IReadOnlyList<User>> ListAllUserAsync(MusicContext context)
        {
            var results = context.Users.ToListAsync();
            return await results;
        }

        //Gets all genres for user from database
        public static async Task<List<Genre>> GetGenresForUserAsync(MusicContext context, int userId)
        {
            List<Genre> genres = new List<Genre>();

            User user = await context.Users.Include(u => u.Genres).FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null)
            {
                genres = user.Genres?.ToList() ?? new List<Genre>();
            }

            return genres;
        }

        //Gets all artists for users from database
        public static async Task<List<Artist>> GetArtistsForUserAsync(MusicContext context, int userId)
        {
            List<Artist> artists = new List<Artist>();

            User user = await context.Users.Include(u => u.Artists).FirstOrDefaultAsync(u => u.Id == userId);

            if (user != null && user.Artists != null)
            {
                artists = user.Artists?.ToList() ?? new List<Artist>();
            }

            return artists;
        }





        //public static async Task<IReadOnlyList<Genre>> ListAllGenresAsync(MusicContext context, int id)
        //{
        //    var results = context.Users.ToListAsync();
        //    return await results;
        //}




        //public static async Task<IReadOnlyList<Artist>> ListAllArtistsAsync(MusicContext context, int id)
        //{
        //    var results = context.Users.ToListAsync();
        //    return await results;
        //}




        //public static async Task<IReadOnlyList<Song>> ListAllSongsAsync(MusicContext context, int id)
        //{
        //    var results = context.Users.ToListAsync();
        //    return await results;
        //}




        //// Koppla en person till en ny genre, artist och låt // Todo fix name

        //public static async Task<IReadOnlyList<User>> ConntectPerson(MusicContext context)
        //{
        //    var results = context.Users.ToListAsync();
        //    return await results;
        //}   










    }
}
