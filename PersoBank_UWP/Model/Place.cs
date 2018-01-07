using System;

namespace PersoBank_UWP.Model
{
    public class Place
    {
        public int PlaceId { get; set; }
        public decimal? AverageAmount { get; set; }
        public string Street { get; set; }
        public int? StreetNumber { get; set; }
        public int? PostalCode { get; set; }
        public string City { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
    }

    public class BestPlace
    {
        public string Name { get; set; }
        public int NbTransactions { get; set; }
    }
}
