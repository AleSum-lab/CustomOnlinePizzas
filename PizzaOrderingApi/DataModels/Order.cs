using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class Order
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public ICollection<Pizza> Items { get; set; }
        public double DeliveryDistance { get; set; }
        public double DeliveryFee { get; set; }
        public double TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }

    }
}
