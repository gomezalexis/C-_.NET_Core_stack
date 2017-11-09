using System.ComponentModel.DataAnnotations;

namespace LoginRegistration.Models{
    public class User{

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name = "First Name")]
        public string FirstName {get; set;}

        [Required]
        [MinLength(2)]
        [Display(Name = "Last Name")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        public string LastName {get; set;}

        [Required]
        [EmailAddress]
        public string Email {get; set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmation")]
        public string PasswordConfirm {get; set;}
    }
}