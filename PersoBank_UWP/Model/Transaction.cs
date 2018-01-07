using System;

namespace PersoBank_UWP.Model
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public string Comment { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public bool Expense { get; set; }

        public int? PlaceId { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }
    }
}
