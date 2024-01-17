using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class UserSong
    {
        public int UserId { get; set; }
        public int SongId { get; set; }
        public User User { get; set; }
        public Song Song { get; set; }
    }
}
