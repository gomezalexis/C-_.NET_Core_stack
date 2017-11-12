using System.ComponentModel.DataAnnotations;
using System;

namespace LostInTheWoods.Models
{
    public class Trail {

        [Required]
        [MinLength(2)]
        [Display(Name= "Trail Name")]
        public string trailName {get; set;}

        [Required]
        [MinLength(10)]
        [Display(Name = "Description")]
        public string description {get; set;}

        [Required]
        [Display(Name= "Trail Length")]
        public double trailLength {get; set;}

        [Required]
        [Display(Name = "Elevation Change")]
        public double elevationChange {get; set;}

        public int id {get; set;}
        public DateTime created_at {get; set;}
    }
}