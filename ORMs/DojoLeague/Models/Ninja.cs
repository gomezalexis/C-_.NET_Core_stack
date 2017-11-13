using System.ComponentModel.DataAnnotations;
using System;

namespace DojoLeague.Models
{
    public class Ninja 
    {
        [Required]
        [MinLength(2)]
        [Display(Name= "Ninja Name")]
        public string name {get; set;}

        [Required]
        [Range(1,10)]
        [Display(Name = "Level")]
        public int level {get; set;}

        [Display(Name = "Description (optional)")]
        public string description {get; set;}

        [Display(Name = "Dojo")]
        public int dojo_id {get; set;}
        public int id {get; set;}
        public DateTime created_at {get; set;}
    }
}