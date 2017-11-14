using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace BankAccounts.Models
{
    public class Transaction : BaseEntity
    {
        public int transactionId {get; set;}

        [Required]
        [Range(-10000,10000)]
        [Display(Name = "Deposit/Withdraw")]
        public double amount {get; set;}
        public DateTime createdAt {get; set;}
        public int userId {get; set;}
        public User user {get; set;}

    }
}