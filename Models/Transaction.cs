namespace restAPI.Models
{
    public class Transaction
    {
        public required string Payer { get; set; }
        public int Points { get; set; } 
        public DateTime Timestamp { get; set; }
    }
}
