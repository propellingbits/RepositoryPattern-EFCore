using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Data.Test.DomainModel
{
    public  class Vehicle
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Model { get; set; }

        public string StockNumber { get; set; }

        public string ModelNo { get; set; }

        public int ModelYear { get; set; }

        public string Color { get; set; }

        public float Price { get; set; }

        public int Mileage { get; set; }

        public int DealedId { get; set; }
    }
}
