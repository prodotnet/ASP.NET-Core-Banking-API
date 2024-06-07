using BankingApi.Controllers.V1;
using BankingApi.Data;
using BankingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Xunit;


namespace BankingApi.Tests.Controllers
{
    public class TransactionsControllerTests
    {
        private TransactionsController GetControllerWithAuthenticatedUser(ApplicationDbContext context)
        {
            var controller = new TransactionsController(context);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "username"),
                new Claim(ClaimTypes.NameIdentifier, "1")
            }, "mock"));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            return controller;
        }
        
        
        [Fact]
        public async Task CreateWithdrawal_ValidRequest_ReturnsOk()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BankAPI_DB")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                // Populate the database with test data
                // ...

                var controller = GetControllerWithAuthenticatedUser(context);

                var withdrawalRequest = new WithdrawalRequest
                {
                    AccountNumber = "1234567890", // This should match the pre-populated account number
                    Amount = 500
                };

                // Act
                var result = await controller.CreateWithdrawal(withdrawalRequest);

                // Assert
                var okResult = Assert.IsType<OkObjectResult>(result);
                Assert.Equal("Withdrawal successful", okResult.Value);

                // Verify that the account balance is updated
                var updatedAccount = await context.ClientBankDetails.FirstOrDefaultAsync(a => a.AccountNumber == "1234567890");
                Assert.Equal(500, updatedAccount.Balance);
            }
        }

        [Fact]
        public async Task CreateWithdrawal_InsufficientBalance_ReturnsBadRequest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BankAPI_DB")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var controller = GetControllerWithAuthenticatedUser(context);

                var withdrawalRequest = new WithdrawalRequest
                {
                    AccountNumber = "9876543210", 
                    Amount = 600 
                };

                // Act
                var result = await controller.CreateWithdrawal(withdrawalRequest);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Insufficient balance", badRequestResult.Value);

                var updatedAccount = await context.ClientBankDetails.FirstOrDefaultAsync(a => a.AccountNumber == "9876543210");
                Assert.Equal(500, updatedAccount.Balance); 
            }
        }

        [Fact]
        public async Task CreateWithdrawal_InactiveAccount_ReturnsBadRequest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BankAPI_DB")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var inactiveAccount = new ClientBankDetail
                {
                    Id = 4,
                    AccountNumber = "1691307855",
                    Accounttype = AccountType.Savings,
                    Name = "Inactive Account",
                    Status = AccountStatus.Inactive,
                    Balance = 1000,
                    ClientDetailsId = 1
                };

                await context.ClientBankDetails.AddAsync(inactiveAccount);
                await context.SaveChangesAsync();

                var controller = GetControllerWithAuthenticatedUser(context);

                var withdrawalRequest = new WithdrawalRequest
                {
                    AccountNumber = "1691307855", // Inactive account number
                    Amount = 500
                };

                // Act
                var result = await controller.CreateWithdrawal(withdrawalRequest);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Withdrawals are not allowed on inactive accounts", badRequestResult.Value);

                var updatedAccount = await context.ClientBankDetails.FirstOrDefaultAsync(a => a.AccountNumber == "1691307855");
                Assert.Equal(1000, updatedAccount.Balance); // Balance should remain unchanged
            }
        }

        [Fact]
        public async Task CreateWithdrawal_FixedDepositPartialWithdrawal_ReturnsBadRequest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "BankAPI_DB")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var controller = GetControllerWithAuthenticatedUser(context);

                var withdrawalRequest = new WithdrawalRequest
                {
                    AccountNumber = "1376543210", 
                    Amount = 100 // Partial withdrawal not allowed for Fixed Deposit
                };

                // Act
                var result = await controller.CreateWithdrawal(withdrawalRequest);

                // Assert
                var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
                Assert.Equal("Only 100% withdrawal is allowed on Fixed Deposit accounts", badRequestResult.Value);

                var updatedAccount = await context.ClientBankDetails.FirstOrDefaultAsync(a => a.AccountNumber == "1376543210");
                Assert.Equal(200, updatedAccount.Balance); // Balance should remain unchanged
            }
        }
    }
}
