using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class Comment{

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string comment{get; set;}

        public int id{get; set;}
    }
}