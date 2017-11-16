using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace WeddingPlanner.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int userId {get; set;}
        public string firstName {get; set;}
        public string lastName {get; set;}
        public string email {get; set;}
        public string password {get; set;}
        public List<Wedding> weddings {get; set;}
        public List<Guest> guests {get; set;}

        public User()
        {
            weddings = new List<Wedding>();
            guests = new List<Guest>();
        }

    }
    public class RegisterViewModel : BaseEntity{

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name = "First Name")]
        public string firstName {get; set;}

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Name can only contain letters")]
        [Display(Name = "Last Name")]
        public string lastName {get; set;}

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string email {get; set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password {get; set;}  

        [Required]
        [Compare("password")]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmation")]
        public string passwordConfirm {get; set;}
    }

    public class LoginViewModel : BaseEntity
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string loginEmail {get; set;}

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string loginPassword {get; set;}
    }

    //Lets do the Wrapper
    public class UserWrapper : BaseEntity
    {
        public RegisterViewModel registerVM {get; set;}
        public LoginViewModel loginVM {get; set;}
    }
}