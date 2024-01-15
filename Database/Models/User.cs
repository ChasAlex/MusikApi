using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Fullname { get; set; }



        [ForeignKey(nameof(GenreId))]
        public int GenreId { get; set; }
        public virtual ICollection<Genre>? Genres { get; set; }


        [ForeignKey(nameof(SongId))]
        public int SongId { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }


        [ForeignKey(nameof(ArtistId))]
        public int ArtistId { get; set; }
        public virtual ICollection<Artist>? Artists { get; set; }

    }
}
