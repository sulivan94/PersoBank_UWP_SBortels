using System;

namespace PersoBank_UWP.Model
{
    public class ExpensesGestionModel
    {
        public string Name { get; set; }
        public decimal? Average { get; set; }
        public bool IsCategory { get; set; }
        public int IdModel { get; set; }
    }
}
