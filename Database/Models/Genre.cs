using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<User>? Users { get; set; }
        public int SongId { get; set; }
        public virtual ICollection<Song> Songs { get; set; }
    }
}
