using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Entity;
using PhoneBook.Repository;

namespace PhoneBook.Services
{
    /// <summary>
    /// This class is called to performe the main authentication
    /// </summary>

    public static class AuthenticationService
    {
        public static User LoggedUser { get; private set; }

        public static bool AuthenticateUser(string username, string password)
        {
            UserRepository userRepository = new UserRepository();
            LoggedUser = userRepository.GetAll(filter: u => u.UserName == username && u.Password == password).FirstOrDefault();
            return LoggedUser != null;
        }
    }
}