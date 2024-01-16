using API.Models.DTOs.GetArtistInfoDTO;
using API.Models.DTOs.GetTopTracksByArtistDTO;
using API.Models.DTOs.GetTopTracksByGenreDTO;
using API.Models.ViewModels;
using API.Services;
using Database.Data;
using Database.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddScoped<IMusicServices,MusicService>();

            string connectionString = builder.Configuration.GetConnectionString("dbConnection");

            builder.Services.AddDbContext<MusicContext>(opt => opt.UseSqlServer(connectionString));

            var app = builder.Build();


            //Hämtar top låtar från en specifik artist
            app.MapGet("/artist/{artist}", async(string artist, [FromServices] IMusicServices musicServices) =>
            {
            
                Toptracks tracks = await musicServices.getTopTrackByArtistAsync(artist);

                GetTopTrackByArtistViewmodel[]? result = tracks?.Track?.Select(track => new GetTopTrackByArtistViewmodel
                {
                    Name = track.Name,
                    Playcount = track.Playcount,
                }).ToArray();

                return Results.Json(result);

            });

            // Hämta toplåtar för en genre/Tag
            app.MapGet("/genre/{genre}", async (string genre, [FromServices] IMusicServices musicServices) =>
            {

                Tracks tracks = await musicServices.getTopTracksByGenreAsync(genre);

                GetTopTracksByGenreViewmodel[]? result = tracks?.Track?.Select(track => new GetTopTracksByGenreViewmodel
                {
                    Name = track.Name 
                    
                }).ToArray();

                return Results.Json(result);

            });

            //Hämta Bio,name och playcount for en artist
            app.MapGet("/artistinfo/{artist}", async (string artist, [FromServices] IMusicServices musicServices) =>
            {

                Models.DTOs.GetArtistInfoDTO.Artist artist1 = await musicServices.GetInfoArtistAsync(artist);

                GetInfoArtistViewmodel result = new GetInfoArtistViewmodel
                {
                    Name = artist1.Name,
                    Playcount = artist1.Stats.Playcount,
                    Summary = artist1.Bio.Summary
                };
                return Results.Json(result);

            });







            //Hämta alla personer i systemet
            app.MapGet("/users", DbHelper.ListAllUserAsync);


            app.Run();
        }
    }
}