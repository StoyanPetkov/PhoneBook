using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Entity;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.ViewModels.PhoneViewModel
{
    public class PhoneControllerPhoneVM
    {
        [Required(ErrorMessage ="Phone number required")]
        public string phone { get; set; }
        public List<Phone> PhoneList { get; set; }
        public string ContactName { get; set; }
        public int ParentContactId { get; set; }
        public int Id { get; set; }
    }
}