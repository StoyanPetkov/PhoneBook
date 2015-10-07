using PhoneBook.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace PhoneBook.ViewModels.ContactViewModel
{
    public class ContactControllerEditContactVM
    {
        [Required(ErrorMessage = "Full name required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "E-Mail required")]
        public string Email { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string ImageLocation { get; set; }
        public DateTime BirthDay { get; set; }

        [FileSize(307200)]
        [FileTypes("jpg,jpeg,png")]
        public HttpPostedFileBase file { get; set; }
    }
}