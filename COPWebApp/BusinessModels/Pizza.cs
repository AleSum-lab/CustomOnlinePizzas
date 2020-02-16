using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels
{
    public class Pizza
    {
        public int Id { get; set; }        
        public double BasePrice { get; set; }
        public double TotalPrice { get; set; }
        public IList<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public string Description
        {
            get
            {
                string result = "Pizza with ";

                for (int i = 0; i < Ingredients.Count; i++)
                {
                    if(i == Ingredients.Count -1)
                    {
                        result += $"{Ingredients[i].ToString()}.";
                    }
                    else
                    {
                        result += $"{Ingredients[i].ToString()}, ";
                    }
                }
                return result;
            }
        }

    }

    public enum Ingredient
    {
        TomatoSauce,
        MozzarellaCheese,
        Ham,
        Kebab
    }
}
