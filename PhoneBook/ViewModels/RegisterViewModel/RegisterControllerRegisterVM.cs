using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Entity;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.ViewModels.RegisterViewModel
{
    public class RegisterControllerRegisterVM
    {
        [Required(ErrorMessage ="First name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}