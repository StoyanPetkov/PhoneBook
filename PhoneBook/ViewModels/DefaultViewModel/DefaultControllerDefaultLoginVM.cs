using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Entity;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.ViewModels.DefaultViewModel
{
    public class DefaultControllerDefaultLoginVM
    {
        [Required(ErrorMessage ="Username required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password required")]
        public string Password { get; set; }
    }
}