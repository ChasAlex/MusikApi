using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Country { get; set; }
        public int SongId { get; set; }
        public virtual ICollection<Song> Songs { get; set; }

        public int UserId { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
