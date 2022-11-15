using ApiBank.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiBank.Service.Interfaces
{
    public interface ITransactionService
    {
        Response CreateNewTransaction(Transaction transaction); // we will create a response model just wait
        Response FindTransactionByDate(DateTime date);
        Response MakeDeposit(string AccountNumber, decimal Amount, int TransactionPin);
        Response MakeWithdrawal(string AccountNUmber, decimal Amount, int TransactionPin);
        Response MakeFundsTransfer(string AccountNumber, string ToAccount, decimal Amount, string TransactionPin);

    }
}
    