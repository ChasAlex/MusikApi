using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }

        // Navigation Property for one-to-many relationship with Songs
        public ICollection<Song> Songs { get; set; }

        // Navigation Property for many-to-many relationship with Users
        public ICollection<UserArtist> UserArtists { get; set; }
    }
}
