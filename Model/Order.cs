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

        public Patient createdFor { get; set; }

        public DateTime createdDate { get; set; }

        [JsonIgnore]
        public List<IngredientUsage> ingredientUsages { get; set; }

        public int maxBagSizeWithML { get; set; }

        public Order() { }
        public Order(int Id, User creator, Patient createdFor, DateTime createdDate, int maxBagSizeWithML)
        {
            this.Id = Id;
            this.creator = creator;
            this.createdFor = createdFor;
            this.createdDate = createdDate;
            this.maxBagSizeWithML = maxBagSizeWithML;
        }



        public override string ToString()
        {
            return this.Id + " " + this.creator.Id + " " + this.createdFor.Id + " " + createdDate;
        }


    }
}
