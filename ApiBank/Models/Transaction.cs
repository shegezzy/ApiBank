using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Transactions;

namespace ApiBank.Models
{
    [Table ("Transaction")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        public string TransactionUniqueReference { get; set; } // this we will generate in every instance of this class
        public decimal TransactionAmount { get; set; }
        public TranStatus TransactionStatus { get; set; } // this is an enum let's create it
        public bool IsSuccessful => TransactionStatus.Equals(TranStatus.Success); // this guy depends on the value of TransactionStatus
        public string TransactionSourceAccount { get; set; }
        public string TransactionParticulars { get; set; }
        public TranType TransactionType { get; set; } // this is another enum
        public DateTime TransactionDate { get; set; }
         
        public Transaction()
        {
            TransactionUniqueReference = $"{Guid.NewGuid().ToString().Replace("-","").Substring(1, 27)}"; /// We will use guid to generate it
        }
    }

    public enum TranStatus
    {
        Failed,
        Success,
        Error,

    }

    public enum TranType
    {
        Deposit,
        Withdrawal,
        Transfer,
    }
}
