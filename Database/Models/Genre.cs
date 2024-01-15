using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }


        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public virtual ICollection<User>? Users { get; set; }


        [ForeignKey(nameof(SongId))]
        public int SongId { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
