using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace WpfApp1.Model
{
    public class Ingredient
    {


        public int Id { get; set; }

        public string name { get; set; }

        public DateTime createdDate { get; set; }

        [JsonIgnore]
        public List<IngredientUsage> usagedBy { get; set; }


        public Ingredient() { }

        public Ingredient(int Id, string name, DateTime createdDate)
        {
            this.Id = Id;
            this.name = name;
            this.createdDate = createdDate;
        }

        public override string ToString()
        {
            return  this.Id + " " + this.name + " " + this.createdDate;
        }


    }
}
