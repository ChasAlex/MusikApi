using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Foreign Key for one-to-many relationship with Artist
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }

        // Foreign Key for one-to-many relationship with Artist
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }

        // Navigation Property for many-to-many relationship with Users
        public ICollection<UserSong> UserSongs { get; set; }
    }
}
