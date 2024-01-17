using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Fullname { get; set; }

        // Navigation Property for one-to-one relationship with Credentials
        public Credential Credential { get; set; }

        // Navigation Properties for many-to-many relationship with Genres, Artists, and Songs
        public ICollection<UserGenre>? UserGenres { get; set; }
        public ICollection<UserArtist>? UserArtists { get; set; }
        public ICollection<UserSong>? UserSongs { get; set; }

    }
}
