using ApiBank.DAL;
using ApiBank.Models;
using ApiBank.Service.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Internal;

namespace ApiBank.Service.Implementation
{
    public class AccountService : IAccountService
    {
        private ApiBankDbContext _dbContext;
        public AccountService(ApiBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account Authenticate(string AccountNumber, string Pin)
        {
            //lets make authentic
            //does account exist for that number
            var account = _dbContext.Accounts.Where(x => x.AccountNumberGenerated == AccountNumber).SingleOrDefault();
            if(account == null)
                return null;
            //ok so we have a match
            //verify pinHash 
            if(!VerifyPinHash(Pin,account.PinHash, account.PinSalt))
                return null;


            //ok so Authentication is passed
            return account;
      
        }

        private static bool VerifyPinHash(string Pin, byte[] pinHash, byte[] pinSalt)
        {
            if (string.IsNullOrWhiteSpace(Pin)) throw new ArgumentException("Pin ");
            //now let's verify  pin 
            using (var hmac = new System.Security.Cryptography.HMACSHA512(pinSalt))
            {
                var computedPinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Pin));
                for (int i = 0; i < computedPinHash.Length; i++)
                {
                    if (computedPinHash[i] != pinHash[i]) return false;
                }
            }
            return true;
        }

        public Account Create(Account account, string Pin, string ConfirmPin)
        {
            // this is to create a new account 
            if (_dbContext.Accounts.Any(x => x.Email == account.Email)) throw new ApplicationException("An account already exists with this" +
                " email");
            //validate pin 
            if (!Pin.Equals(ConfirmPin)) throw new ApplicationException("Pins do not match");

            //now all validation passes, lets create user account,
            //we are hasshing /encrypting pin first
            byte[] pinHash, pinSalt;
            CreatePinHash(Pin, out pinHash, out pinSalt); //lets create crypto method


            account.PinHash = pinHash;
            account.PinSalt = pinSalt;

            //all good add new account to db
            _dbContext.Accounts.Add(account);
            _dbContext.SaveChanges();

            return account;
        }

        private static void CreatePinHash(string pin, out byte[] pinHash, out byte[] pinSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pinSalt = hmac.Key;
                pinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pin));
            }
        }

        public void Delete(int Id)
        {
            var account = _dbContext.Accounts.Find(Id);
            if(account != null)
            {
                _dbContext.Accounts.Remove(account);
                _dbContext.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAllAccount()
        {
           return _dbContext.Accounts.ToList();
        }

        public Account GetByAccountNumber(string AccountNumber)
        {
            var account = _dbContext.Accounts.Where(x => x.AccountNumberGenerated == AccountNumber).FirstOrDefault();
            if (account == null) return null;

            return account;
        }

        public Account GetById(int Id)
        {
            var account = _dbContext.Accounts.Where(x=> x.Id ==Id).FirstOrDefault();
            if (account == null) return null;

            return account;
            
        }

        public void Update(Account account, string Pin = null)
        {
            //update is more tricky
            

            var accountToBeUpdated = _dbContext.Accounts.Where(x => x.Email == account.Email).SingleOrDefault();
            if (accountToBeUpdated == null) throw new ApplicationException("Account does not exist");
            //if it exists, let's listen for  user wanting to change any of his/her properties
            //acutually we want to allow the user to be able to chnage only the Email
            if (!string.IsNullOrWhiteSpace(account.Email))
            {
                //this means the user wishes to chnage his/her email
                //check if the one he/she is changing is not already taken
                if(_dbContext.Accounts.Any(x=> x.Email == account.Email)) throw new ApplicationException("This Email" + account.Email + "already exists");
                //else chnage email

                accountToBeUpdated.Email = account.Email;

            }

            //acutually we want to allow the user to be able to chnage only the PhoneNumber
            if (!string.IsNullOrWhiteSpace(account.PhoneNumber))
            {
                //this means the user wishes to chnage his/her email
                //check if the one he/she is changing is not already taken
                if (_dbContext.Accounts.Any(x => x.PhoneNumber == account.PhoneNumber)) throw new ApplicationException("This PhoneNumber" + account.PhoneNumber + "already exists");
                //else chnage email

                accountToBeUpdated.PhoneNumber = account.PhoneNumber;

            }
            //acutually we want to allow the user to be able to chnage only the pin
            if (!string.IsNullOrWhiteSpace(Pin))
            {
                //this means the user wishes to chnage his/her pin

                byte[] pinHash, pinSalt;
                CreatePinHash(Pin,out pinHash, out pinSalt);
                
                accountToBeUpdated.PinHash = pinHash;
                accountToBeUpdated.PinSalt = pinSalt;

            }
            accountToBeUpdated.DateLastUpdated = DateTime.Now;



            ///now persist this update to db
            _dbContext.Accounts.Update(accountToBeUpdated);
            _dbContext.SaveChanges();


        }

        
    }
}
