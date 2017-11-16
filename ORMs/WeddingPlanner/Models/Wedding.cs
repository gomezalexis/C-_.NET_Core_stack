using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace WeddingPlanner.Models
{
    public class Wedding : BaseEntity
    {
        [Key]
        public int weddingId {get; set;}

        [Required]
        [Display(Name = "Wedding Date")]
        public DateTime weddingDate {get; set;}

        [Required]
        [Display(Name = "Wedder One")]
        public string wedderOne {get; set;}

        [Required]
        [Display(Name = "Wedder Two")]
        public string wedderTwo {get; set;}

        [Required]
        [MinLength(10)]
        [Display(Name = "Wedding Address")]
        public string address {get; set;}

        public int userId {get; set;}
        public User user {get; set;}

        public List<Guest> guests {get; set;}
        public Wedding()
        {
            guests = new List<Guest>();
        }

    }
}