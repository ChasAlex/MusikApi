using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models
{
    public class Credential
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
