namespace BankingApi.Models
{

    //The enum for getting the client gender
    public enum Gender
    {
        Male,
        Female,

    }

    public class ClientDetail
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string ClientID { get; set; }

        public Gender Gender { get; set; }


        public string Address { get; set; }


        public string EmailAddress { get; set; }


        public string MobileNumber { get; set; }


        public List<ClientBankDetail> ClientBankDetails { get; set; }
    }
}
