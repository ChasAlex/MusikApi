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

            //string connectionString = builder.Configuration.GetConnectionString("dbConnection");

            string hardcodedConnection = "Data Source=(localdb)\\.;Initial Catalog=MusicApiProjectDb;Integrated Security=True;Pooling=False;Trust Server Certificate=False";

            builder.Services.AddDbContext<MusicContext>(opt => opt.UseSqlServer(hardcodedConnection));

            var app = builder.Build();


            //Hämta alla personer i systemet
            app.Map("/users", DbHelper.ListAllUserAsync);



            app.Run();
        }
    }
}