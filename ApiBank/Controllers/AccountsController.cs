using ApiBank.Models;
using ApiBank.Service.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ApiBank.Controllers
{
    [ApiController]
    [Route("api/v3/[controller]")]
    public class AccountsController : ControllerBase
    {
        //let's inject the AccountService
        private IAccountService _accountService;
        //let's also bring Automapper
        IMapper _mapper;
        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        //registerNewAccount
        [HttpPost]
        [Route("Register_new_account")]
        public IActionResult RegisterNewAccount([FromBody] RegisterNewAccountModel newAccount)
        {
            if (!ModelState.IsValid) return BadRequest(newAccount);

            //map to account

            var account = _mapper.Map<Account>(newAccount);
            return Ok(_accountService.Create(account, newAccount.Pin, newAccount.ConfirmPin));
        }

        [HttpGet]
        [Route("get_all_accounts")]
        public IActionResult GetAllAccounts()
        {
            //we want to map to GetAccountModel as defined in my

            var accounts = _accountService.GetAllAccount();
            var cleanedAccounts = _mapper.Map<IList<GetAccountModel>>(accounts);
            return Ok(cleanedAccounts);
        }

        [HttpPost]
        [Route("Authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            if(!ModelState.IsValid) return BadRequest(model);

            //now let's map
            return Ok(_accountService.Authenticate(model.AccountNumber, model.Pin));
            //it returns an account ....lets see whenwe run before we know whter to map or not
        }

        [HttpGet]
        [Route("get_by_account_number")]
        public IActionResult GetByAccountNumber(string AccountNumber)
        {
            if (!Regex.IsMatch(AccountNumber, @"^{0} [1-9]\d{9}$|^{1-9}$")) return BadRequest("Account Number must be 10-digit");

            var account = _accountService.GetByAccountNumber(AccountNumber);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpGet]
        [Route("get_by_account_By_Id")]
        public IActionResult GetByAccountById(int Id)
        {
            var account = _accountService.GetById(Id);
            var cleanedAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(cleanedAccount);
        }

        [HttpPut]
        [Route("update_account")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountModel model)
        {
            if(!ModelState.IsValid) return BadRequest(model);

            var account = _mapper.Map<Account>(model);

            _accountService.Update(account,model.Pin);
            return Ok(account);

            
        }




    }
}
