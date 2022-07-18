using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
namespace WpfApp1.Model
{
    public class Order
    {

        public int Id { get; set; }

        public User creator { get; set; }

        public DateTime createdDate { get; set; }

        [JsonIgnore]
        public List<IngredientUsage> ingredientUsages { get; set; }


        public Order() { }
        public Order(User creator, DateTime createdDate)
        {
            this.creator = creator;
            this.createdDate = createdDate;
        }



        public override string ToString()
        {
            return this.Id + " " + this.creator.Id + " " + createdDate;
        }


    }
}
