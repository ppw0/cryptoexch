namespace Cryptoexch.Models
{
    public partial class OrderView_Result
    {
        public int ID { get; set; }
        public string CurrencyPair { get; set; }
        public string Beginning { get; set; }
        public string Type { get; set; }
        public double? Amount { get; set; }
        public double? Limit { get; set; }
        public double? Total { get; set; }
        public string Expiration { get; set; }
    }
}