using System.ComponentModel.DataAnnotations;

namespace BankingApi.Models
{

    /// <summary>
    /// enum for client Acount type
    /// </summary>
    public enum AccountType
    {
        Cheque,
        Savings,
        FixedDeposit
    }

    /// <summary>
    /// enum for client Account status
    /// </summary>
    public enum AccountStatus
    {
        Active,
        Inactive
    }

    public class ClientBankDetail
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public AccountType Accounttype { get; set; }
        public string Name { get; set; }
        public AccountStatus Status { get; set; }
        public decimal Balance { get; set; }

        [Required]
        public int ClientDetailsId { get; set; }
        public ClientDetail ClientDetails { get; set; }
    }

}
