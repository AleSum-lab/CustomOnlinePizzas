using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels
{
    public class Order
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public IList<Pizza> Items { get; set; } = new List<Pizza>();
        public double DeliveryDistance { get; set; }
        public double DeliveryFee { get; set; }
        public double TotalPrice {
            get
            {
                var totalPrice = DeliveryFee;
                foreach (var item in Items)
                {
                    totalPrice += item.TotalPrice;
                }

                return totalPrice;
            }
        }
        

    }
}
