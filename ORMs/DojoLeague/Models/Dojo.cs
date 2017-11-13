using System.ComponentModel.DataAnnotations;
using System;

namespace DojoLeague.Models
{
    public class Dojo {

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [Display(Name = "Dojo Name")]
        public string dojoName {get; set;}

        [Required]
        [MinLength(2)]
        [MaxLength(30)]
        [Display(Name = "Dojo Location")]
        public string dojoLocation {get; set;}

        [Display(Name = "Description (optional)")]
        public string description {get; set;}

        public int id {get; set;}
        public DateTime created_at {get; set;}
    }
}