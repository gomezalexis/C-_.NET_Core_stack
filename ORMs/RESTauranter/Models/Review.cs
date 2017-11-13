using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace RESTauranter.Models
{
    public class Review : BaseEntity
    {
        public int id {get; set;}

        [Required]
        [MinLength(2)]
        [Display(Name = "Reviewer Name")]
        public string reviewerName {get; set;}

        [Required]
        [MinLength(2)]
        [Display(Name = "Restaurant Name")]
        public string restaurantName {get; set;}

        [Required]
        [MinLength(10)]
        [Display(Name = "Review")]
        public string theReview {get; set;}

        [Required]
        [Display(Name = "Date of Visit")]
        public DateTime dateOfVisit {get; set;}

        [Required]
        [Range(1, 5)]
        [Display(Name = "Stars")]
        public int stars {get; set;}

    }
}