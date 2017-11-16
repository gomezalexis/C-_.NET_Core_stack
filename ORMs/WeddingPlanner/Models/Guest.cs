using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace WeddingPlanner.Models
{
    public class Guest : BaseEntity
    {
        [Key]
        public int guestId {get; set;}

        public int userId {get; set;}
        public User user {get; set;}

        public int weddingId {get; set;}
        public Wedding wedding {get; set;}
    }
}