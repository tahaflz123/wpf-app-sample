using System;
using System.Collections.Generic;
using System.Text;
using WpfApp1.Model;
using WpfApp1.Data;

namespace WpfApp1.Service
{
    public class AuthService
    {
        
        public static string loggedInUserName { get; set; }


        public static User getLoggedInUser(UserWPFContext context)
        {
            var userService = new UserService(context);
            User user = userService.findUserByName(loggedInUserName);
            return user;
        }



        




    }
}
