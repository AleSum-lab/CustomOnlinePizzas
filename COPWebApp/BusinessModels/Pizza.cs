using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessModels
{
    public class Pizza
    {
        private string description;
        public int Id { get; set; }        
        public double BasePrice { get; set; }
        public double TotalPrice { get; set; }
        public IList<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public string Description
        {
            get
            {
                if (Ingredients.Count == 0)
                {
                    return description;
                }
                else
                {
                    description = "Pizza with ";

                    for (int i = 0; i < Ingredients.Count; i++)
                    {
                        if (i == Ingredients.Count - 1)
                        {
                            description += $"{Ingredients[i].ToString()}.";
                        }
                        else
                        {
                            description += $"{Ingredients[i].ToString()}, ";
                        }
                    }
                    return description;
                }
                
            }
            set => description = value;
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
