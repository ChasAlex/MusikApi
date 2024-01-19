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
		public DbSet<UserArtist> UserArtists { get; set; }
		public DbSet<UserGenre> UserGenres { get; set; }
		public DbSet<UserSong> UserSongs { get; set; }

		

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<UserArtist>()
				.HasKey(ua => new { ua.UserId, ua.ArtistId });

			modelBuilder.Entity<UserGenre>()
				.HasKey(ug => new { ug.UserId, ug.GenreId });

			modelBuilder.Entity<UserSong>()
				.HasKey(us => new { us.UserId, us.SongId });

			base.OnModelCreating(modelBuilder);
		}
	}
}