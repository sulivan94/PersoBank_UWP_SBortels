using System;

namespace PersoBank_UWP.Model
{
    public class Account
    {
        public int AccountId { get; set; }
        public string Comment { get; set; }
        public string Name { get; set; }
        public bool Authorization { get; set; }

        public int CustomerId { get; set; }
    }
}
