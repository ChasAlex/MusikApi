using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Credential
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
