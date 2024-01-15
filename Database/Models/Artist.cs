using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }


        [ForeignKey(nameof(SongId))]
        public int SongId { get; set; }
        public virtual ICollection<Song> Songs { get; set; }


        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
