﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanApplication.Controllers
{
    public class AccountControllerIAccountRepository accountRepository, ICustomerRepository customerRepository,
        ISequenceRepository sequenceRepository, IAccountTypeRepository accounttypeRepository, ILocationRepository locationRepository) : ControllerBase
    {
        public readonly IAccountRepository _accountRepository = accountRepository;
        public readonly ICustomerRepository _customerRepository = customerRepository;
        public readonly ISequenceRepository _sequenceRepository = sequenceRepository;
        public readonly ILocationRepository _locationRepository = locationRepository;
        public readonly IAccountTypeRepository _accounttypeRepository = accounttypeRepository;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get()
        {
            var account = _accountRepository.Query().Select(a => new Account
            {
                AccountId = a.AccountId,
                Number = a.Number,
                AccountTypeId = a.AccountTypeId,
                Purpose = a.Purpose,
                Alert = a.Alert,
                Balance = a.Balance,
                CustomerId = a.CustomerId,
                Days = a.Days,
                Status = a.Status,
                AccountType = _accounttypeRepository.Query().FirstOrDefault(t => t.AccountTypeId == a.AccountTypeId),
                Customer = _customerRepository.Query().FirstOrDefault(t => t.CustomerId == a.CustomerId)
            });

            return Ok(account);
        }

        // GET api/Account
        [HttpGet("List")]
        [AllowAnonymous]
        public async Task<IActionResult> GetList()
        {
            var account = _accountRepository.Query();

            return Ok(account);
        }
        // GET api/Account
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var account = _accountRepository.GetAll(id).FirstOrDefault();

            if (account != null)
            {
                account.BaseType = account.AccountType.BaseType;
                //_accounttypeRepository.Query().Where(a => a.AccountTypeId == account.AccountTypeId).Select(b => b.BaseType).FirstOrDefault();
                return Ok(account);
            }
            else
                return BadRequest();
        }
        // GET api/Account
        [AllowAnonymous]
        [HttpGet("Number/{number}")]
        public async Task<IActionResult> GetByAccountNo([FromRoute] string number)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var account = _accountRepository.GetAll().Where(s => s.Number == number).OrderByDescending(d => d.Date).FirstOrDefault();

            if (account != null)
            {
                account.BaseType = account.AccountType.BaseType;
                return Ok(account);
            }
            else
                return BadRequest();
        }

        // GET api/Account
        [HttpGet("Status/{status}")]
        public async Task<IActionResult> Get([FromRoute] string status)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var account = _accountRepository.GetAll().Where(s => s.Status == status);

            if (account != null)
            {
                return Ok(account);
            }
            else
                return BadRequest();
        }

        [HttpGet("customer/{id}")]
        public async Task<IActionResult> GetCustomerAccountById([FromRoute] string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var account = _customerRepository.GetAll().Where(c => c.CustomerId.ToString() == id || c.Code == id).FirstOrDefault();

            if (account != null)
            {
                return Ok(account);
            }
            else
                return BadRequest();
        }
        [HttpGet("transaction/{id}")]
        public async Task<IActionResult> GetAccountTransaction([FromRoute] string id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var account = _accountRepository.Query().Where(c => c.Number == id).Include(t => t.Transactions
                        .Where(s => s.Source != "Nominal").OrderByDescending(d => d.Date)).FirstOrDefault();

            if (account != null)
            {
                return Ok(account.Transactions);
            }
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Account value)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var type = await _accounttypeRepository.GetAsync(value.AccountTypeId);
            if (type == null) return BadRequest(new { Message = "Select a Valid Account Type" });
            value.Number = await _sequenceRepository.GetCode(type.SequenceId);

            await _accountRepository.InsertAsync(value);

            return Created($"account/{value.AccountId}", value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Account value, [FromRoute] int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != value.AccountId) return BadRequest();
            await _accountRepository.UpdateAsync(value);

            return Ok(value);
        }
    
}
