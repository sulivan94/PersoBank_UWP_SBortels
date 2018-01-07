using System;

namespace PersoBank_UWP.Model
{
    public class Category
    {
        public int CategoryId { get; set; }
        public decimal? AverageAmount { get; set; }
        public string Label { get; set; }
        public bool Expense { get; set; }
    }

    public class BestCategory
    {
        public string Label { get; set; }
        public int NbTransactions { get; set; }
    }
}
