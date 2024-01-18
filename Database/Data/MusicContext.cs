using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Data
{
    public class MusicContext : DbContext
    {

        public MusicContext() { }
        public MusicContext(DbContextOptions<MusicContext> options) : base(options) { }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Credential> Credentials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\.;Initial Catalog=MusikAPI;Integrated Security=True;Pooling=False");
        }
    }
}


// Alex connectionstring = "Data Source=(localdb)\\.;Initial Catalog=MusikAPI;Integrated Security=True;Pooling=False"

// Andreas connectionstring = "Data Source=(localdb)\\.;Initial Catalog=MusicApiProjectDb;Integrated Security=True;Pooling=False;Trust Server Certificate=False"

