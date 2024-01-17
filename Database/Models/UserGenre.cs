using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class UserGenre
    {
        public int UserId { get; set; }
        public int GenreId { get; set; }
        public User User { get; set; }
        public Genre Genre { get; set; }
    }
}
