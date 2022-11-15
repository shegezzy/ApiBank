using System.ComponentModel.DataAnnotations;
using System;

namespace ApiBank.Models
{
    public class GetAccountModel
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; } // This will be an Enum to show if the account to show if its Savings or Current
        public string AccountNumberGenerated { get; set; } // We shall generate accountnumber here!

        //we'll also store the hash and salt of the Account Transaction
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
