using System;

namespace PersoBank_UWP.Model
{
    public class Budget
    {
        public int BudgetId { get; set; }
        public decimal? AmountAlert { get; set; }
        public string Label { get; set; }
        public DateTime BeginingDate { get; set; }
        public decimal Amount { get; set; }
        public int Duration { get; set; }

        public int AccountId { get; set; }
        public int CategoryId { get; set; }
    }
}
