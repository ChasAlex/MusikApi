using Microsoft.EntityFrameworkCore;
using Database.Data;
using Database.Handlers;


namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            string connectionString = builder.Configuration.GetConnectionString("MusicContext");
            builder.Services.AddDbContext<MusicContext>(opt => opt.UseSqlServer(connectionString));

            var app = builder.Build();
            

            //Hämta alla personer i systemet
            app.Map("/users",DbHelper.ListAllUserAsync);


            
            app.Run();
        }
    }
}