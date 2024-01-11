using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Fullname { get; set; }

        public int CredentialId { get; set; }
        public virtual Credential Credential { get; set; }

        public int GenreId { get; set; }
        public virtual ICollection<Genre>? Genres { get; set; }

        public int SongId { get; set; }
        public virtual ICollection<Song>? Songs { get; set; }

        public int ArtistId { get; set; }
        public virtual ICollection<Artist>? Artists { get; set; }

    }
}
