namespace Cryptoexch.Models
{
    public partial class UserView_Result
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string BankAccount { get; set; }
        public bool Verified { get; set; }
        public string Doc1URI { get; set; }
        public string Doc2URI { get; set; }
        public bool TwoFactorEnabled { get; set; }
    }
}