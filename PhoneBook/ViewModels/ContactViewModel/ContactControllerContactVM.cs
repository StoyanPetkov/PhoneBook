using System;
using System.Collections.Generic;
using System.Web;
using PhoneBook.Entity;
using PhoneBook.Attributes;

namespace PhoneBook.ViewModels.ContactViewModel
{
    public class ContactControllerContactVM
    {
        public List<Contact> ContactList { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }
        public int? number { get; set; }
        public int UserId { get; set; }
        public bool FullNameIsChecked { get; set; }
        public string LookingFor { get; set; }
        public string ImageLocation { get; set; }
        public DateTime BirthDay { get; set; }

        [FileSize(307200)]
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase file { get; set; }
    }
}