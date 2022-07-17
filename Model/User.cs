using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WpfApp1.Model
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string name { get; set; }

        public string surname { get; set; }


        public string email { get; set; }

        public string phoneNumber { get; set; }

        [JsonIgnore]
        public ICollection<Patient> patients { get; set; }

        [JsonIgnore]
        public ICollection<Order> orders { get; set; }

        public User()
        {

        }

        public User(string name, string surname, string email, string phoneNumber, ICollection<Patient> patients)
        {
            this.Id = Id;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.patients = patients;
        }

        public User(string name, string surname, string email, string phoneNumber)
        {
            this.Id = Id;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.phoneNumber = phoneNumber;
        }

        public User(User user)
        {
            this.Id = user.Id;
            this.name = user.name;
            this.surname = user.surname;
            this.phoneNumber = user.phoneNumber;
            this.email = user.email;
        }

        public override string ToString()
        {
            return Id + "," + name + "," + surname + "," + this.email + "," + this.phoneNumber;
        }

      
    }
}
