using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankingApi.Data;
using BankingApi.Models;
using Microsoft.AspNetCore.Authorization;
using Azure.Core;

namespace BankingApi.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all transactions.
        /// </summary>
        /// <returns>A list of transactions</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

    


        /// <summary>
        /// Creates a withdrawal transaction.
        /// </summary>
        /// <param name="request">The withdrawal request</param>
        /// <returns>A success message</returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateWithdrawal([FromBody] WithdrawalRequest withdrawal)
        {
            var account = await _context.ClientBankDetails
                .FirstOrDefaultAsync(a => a.AccountNumber == withdrawal.AccountNumber);

            if (account == null)
            {
                return NotFound("Account not found");
            }

            if (account.Status != AccountStatus.Active)
            {
                return BadRequest("Withdrawals are not allowed on inactive accounts");
            }

            if (withdrawal.Amount <= 0)
            {
                return BadRequest("Withdrawal amount must be greater than 0");
            }

            if (account.Accounttype == AccountType.FixedDeposit && withdrawal.Amount != account.Balance)
            {
                return BadRequest("Only 100% withdrawal is allowed on Fixed Deposit accounts");
            }

            if (withdrawal.Amount > account.Balance)
            {
                return BadRequest("Insufficient balance");
            }

            account.Balance -= withdrawal.Amount;
            await _context.SaveChangesAsync();

            // Add audit trail entry
            _context.Transactions.Add(new Transaction
            {
                Action = "Withdrawal",
                EntityName = "BankAccounts",
                Timestamp = DateTime.Now,
                Changes = $"Account {account.AccountNumber} withdrawn {withdrawal.Amount}"
            });

            await _context.SaveChangesAsync();

            return Ok("Withdrawal successful");
        }
    }


    public class WithdrawalRequest
    {
        public required string AccountNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
