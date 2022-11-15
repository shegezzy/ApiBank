using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace ApiBank.Models
{
    [Table("Accounts")]

    public class Account
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

        public byte[] PinHash { get; set; }
        public byte[] PinSalt { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateLastUpdated { get; set; }


        //now to generate an accountNumber, Lets do that in the constructor
        //first lets create a Random obj
        Random rand = new Random();

        public Account()
        {
            AccountNumberGenerated = Convert.ToString((long)rand.NextDouble() * 9_000_000_000L + 1_000_000_000L); //we did 9_000_000 so we could get a 10-digit number
            AccountName = $"{FirstName} {LastName}"; //e.g Segun Akinnola

        }

    }

    public enum AccountType
    {
        savings,//1
        Current,//2
        Corporate,//3
        Government//4
    }

    
    

}