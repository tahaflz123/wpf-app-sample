using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp1.Model
{
    public class IngredientUsage
    {

        public int Id { get; set; }

        public Ingredient Ingredient { get; set; }

        public Order order { get; set; }

        public int usedMl { get; set; }


        public IngredientUsage(){}
        public IngredientUsage(Ingredient ingredient, Order order, int usedMl)
        {
            this.Ingredient = ingredient;
            this.order = order;
            this.usedMl = usedMl;
        }

        public override string ToString()
        {
            return Id + " " + usedMl;
        }
    }
}
