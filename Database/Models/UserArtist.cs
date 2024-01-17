using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class UserArtist
    {
        public int UserId { get; set; }
        public int ArtistId { get; set; }
        public User User { get; set; }
        public Artist Artist { get; set; }
    }
}
