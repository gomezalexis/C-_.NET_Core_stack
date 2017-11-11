using System.ComponentModel.DataAnnotations;

namespace TheWall.Models{
    public class Loger{
        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [MinLength(8)]
        public string Password {get; set;}
    }
}