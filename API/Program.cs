using API.EndPoints;
using API.Models.DTOs.GetTopTracksByArtistDTO;
using API.Models.DTOs.GetTopTracksByGenreDTO;
using API.Models.ViewModels;
using API.Services;
using Database.Data;
using Database.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string connectionString = builder.Configuration.GetConnectionString("MusicDbCon");
            builder.Services.AddScoped<IMusicServices, MusicService>();

            builder.Services.AddDbContext<MusicContext>(opt => opt.UseSqlServer(connectionString));
            builder.Services.AddScoped<IPersonRepo, PersonRepo>();

            var app = builder.Build();

            app.ExternalApiMusic();
            app.MusicApiExtensions();



            app.Run();
        }
    }
}