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

        // public static async Task ConnectUserToSong(MusicContext context, string id, CreateSongDTO dto)
        // {


        //}










    }
}
