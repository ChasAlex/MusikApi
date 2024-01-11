using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public int ArtistId { get; set; }
        public virtual Artist Artist { get; set; }
        public int UserId { get; set; }
        public virtual ICollection<User>? Users { get; set; }

    }
}
