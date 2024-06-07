namespace BankingApi.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public string EntityName { get; set; }
        public DateTime Timestamp { get; set; }
        public string Changes { get; set; }
    }
}

