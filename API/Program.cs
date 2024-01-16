using Database.Data;
using Database.Handlers;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string ConnectionString = builder.Configuration.GetConnectionString("MusicContext");
            builder.Services.AddDbContext<MusicContext>(options => options.UseSqlServer(ConnectionString));

            var app = builder.Build();

            app.MapGet("/", () => "");

            //Get all users
            app.MapGet("/users", DbHelper.ListAllUserAsync);

            //Get all genres for chosen user
            app.MapGet("/genres/{UserId}", async (MusicContext context, int UserId) =>
            {
                var genres = await DbHelper.GetGenresForUserAsync(context, UserId);

                if (genres == null)
                {
                    return Results.NotFound();
                }

                var genreData = genres.Select(genre => new { genre.Title });
                return Results.Json(genreData);
            });

            //Get all artists for chosen user
            app.MapGet("/artists/{UserId}", async (MusicContext context, int UserId) =>
            {
                var artists = await DbHelper.GetArtistsForUserAsync(context, UserId);

                if (artists == null)
                {
                    return Results.NotFound();
                }

                var artistData = artists.Select(artist => new { artist.Name, artist.Description, artist.Country });
                return Results.Json(artistData);
            });

            app.Run();
        }
    }
}