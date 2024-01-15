using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Song
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }


        [ForeignKey(nameof(GenreId))]
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }


        [ForeignKey(nameof(ArtistId))]
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }


        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public virtual ICollection<User>? Users { get; set; }

    }
}
