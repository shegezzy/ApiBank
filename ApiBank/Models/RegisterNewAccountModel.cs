using System.ComponentModel.DataAnnotations;
using System;

namespace ApiBank.Models
{
    public class RegisterNewAccountModel
    {
        //basically it will have everything Accounthas execpt some fields
        
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        //public decimal CurrentAccountBalance { get; set; }
        public AccountType AccountType { get; set; } // This will be an Enum to show if the account to show if its Savings or Current
        //public string AccountNumberGenerated { get; set; } // We shall generate accountnumber here!

        //we'll also store the hash and salt of the Account Transaction
        //public byte[] PinHash { get; set; }
        //public byte[] PinSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }
        //Let's add regular expression
        [Required]
        [RegularExpression(@"^[0-9]\d{4}$" ,ErrorMessage = "Pin must not be more than 4")] // it should be a 4-digit number string
        public string Pin { get; set; }
        [Required]

        [Compare("Pin", ErrorMessage = "Pins do not match")]
        public string ConfirmPin { get; set; } //we want to compare both of them ....
    }
}
