using BankingApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace BankingApi.Data
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {


        }


        //This function will populate the clients and Bank information
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            //populatin the clients information
            modelBuilder.Entity<ClientDetail>().HasData(
           new ClientDetail { Id = 1, Name = "Sphe", Surname = "Ngidi", ClientID = "0123456789012", Gender = Gender.Male, Address = "1 First st Johannesburg", EmailAddress = "sphe@gmail.com", MobileNumber = "07123456789" },
           new ClientDetail { Id=2 ,Name="Pro",Surname="Dot" , ClientID ="2123456789012", Gender = Gender.Female, Address ="2 Second st Johannesburg" , EmailAddress="ProD@gmail.com", MobileNumber="07234567890"},
          new ClientDetail { Id = 3, Name = "Sphesihle", Surname = "Smith", ClientID = "3123456789012", Gender = Gender.Male, Address = "23 Second st Johannesburg", EmailAddress = "sphesihle1@gmail.com", MobileNumber = "0734567890" }
          );



            //populatin the clients Bank information
            modelBuilder.Entity<ClientBankDetail>().HasData(
             new ClientBankDetail { Id = 1, AccountNumber = "1234567890", Accounttype = AccountType.Cheque, Name = "Sphe Ngidi Cheque", Status = AccountStatus.Active, Balance = 1000, ClientDetailsId = 1 },
             new ClientBankDetail { Id = 2, AccountNumber = "9876543210", Accounttype = AccountType.Savings, Name = "Pro Dot Savings", Status = AccountStatus.Active, Balance = 500, ClientDetailsId = 2 },
             new ClientBankDetail { Id = 3, AccountNumber = "1376543210", Accounttype = AccountType.FixedDeposit, Name = "Sphesihle Smith FixedDeposit", Status = AccountStatus.Active, Balance = 200, ClientDetailsId = 3 }
            );




        }


        public DbSet<ClientDetail> ClientDetails { get; set; }
        public DbSet<ClientBankDetail> ClientBankDetails { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

    }
}
