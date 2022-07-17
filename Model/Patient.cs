using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WpfApp1.Model
{
    [Table("patients")]
    public class Patient
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }

        public string phoneNumber { get; set; }

        public string email { get; set; }

        public int age { get; set; }

        public string gender { get; set; }
        public User creator { get; set; }

        [JsonIgnore]
        public ICollection<Order> createdOrders { get; set; }

        public Patient() { }
        public Patient(string name, string surname, string phoneNumber, string email, int age, string gender, User creator)
        {
            this.name = name;
            this.surname = surname;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.age = age;
            this.gender = gender;
            this.creator = creator;
        }

        public override string ToString()
        {
            return Id + " " + name + " " + surname + " " + phoneNumber + " "+ email + " " + age + " " + gender;
        }

    }
}
