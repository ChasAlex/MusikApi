using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Credential
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        // Foreign Key for one-to-one relationship with User
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
