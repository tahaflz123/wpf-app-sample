using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Data;
using WpfApp1.Model;

namespace WpfApp1
{
    public class UserService
    {

        private UserWPFContext _userContext;
       
        public UserService()
        {
            _userContext = new UserWPFContext();
        }

        public UserService(UserWPFContext context)
        {
            _userContext = context;
        }




        public User findUserByName(string name)
        {
            return _userContext.users.Where(u => u.name == name).FirstOrDefault();
        }

        public User createCustomUser(string name, string surname, string email, string phoneNumber)
        {
            return new User(name, surname, email, phoneNumber);
        }

        public List<User> refreshUserList()
        {
            return findAll();
        }

        public User saveUser(User user)
        {
            if (user is null)
                return null;

            User saved = _userContext.users.Add(user).Entity;
            _userContext.SaveChanges();
            
            return saved;
        }

        public List<User> findAll()
        {
            return _userContext.users.Select(e => e).ToList();
        }

        public void deleteUser(User user)
        {
            if(user == null)
            {
                return;
            }
            _userContext.users.Remove(user);
            _userContext.SaveChanges();
        }
    }
}
