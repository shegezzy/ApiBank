using ApiBank.Models;
using System.Collections;
using System.Collections.Generic;

namespace ApiBank.Service.Interfaces
{
    public interface IAccountService
    {
        Account Authenticate(string AccountNumber, string Pin);
        IEnumerable<Account> GetAllAccount();
        Account Create(Account account, string Pin, string ConfirmPin);
        void Update(Account account, string Pin = null);
        void Delete(int Id);
        Account GetById(int Id);
        Account GetByAccountNumber(string AccountNumber);
    }
}
