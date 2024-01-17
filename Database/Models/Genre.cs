using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }

        // Navigation Property for one-to-many relationship with Songs
        public ICollection<Song> Songs { get; set; }

        // Navigation Property for many-to-many relationship with Users
        public ICollection<UserGenre> UserGenres { get; set; }
    }
}
