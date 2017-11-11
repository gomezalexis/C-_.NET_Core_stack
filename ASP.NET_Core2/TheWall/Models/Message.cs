using System.ComponentModel.DataAnnotations;

namespace TheWall.Models
{
    public class Message{

        [Required]
        [MinLength(2)]
        [MaxLength(255)]
        public string message{get; set;}

        public int id{get; set;}
    }
}