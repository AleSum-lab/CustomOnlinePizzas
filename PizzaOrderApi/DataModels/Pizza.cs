using System;
using System.Collections.Generic;
using System.Text;

namespace DataModels
{
    public class Pizza
    {
        public int Id { get; set; }
        public IDictionary<string, double> Ingredients { get; set; }
        public double BasePrice { get; set; }
        public double TotalPrice { get; set; }

    }
}
