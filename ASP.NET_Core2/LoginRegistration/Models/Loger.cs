using System.ComponentModel.DataAnnotations;

namespace LoginRegistration.Models{
    public class Loger{
        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [MinLength(8)]
        public string Password {get; set;}
    }
}