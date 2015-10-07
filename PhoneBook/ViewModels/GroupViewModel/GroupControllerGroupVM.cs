using PhoneBook.Entities;
using PhoneBook.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PhoneBook.ViewModels.GroupViewModel
{
    public class GroupControllerGroupVM
    {
        public List<Group> GroupList  { get; set; }
        public List<Contact> Contacts { get; set; }
        public int GroupId { get; set; }
        [Required(ErrorMessage ="Group name cant be empty")]
        public string groupName { get; set; }
        public int ContactId { get; set; }
        public int UserId { get; set; }
        public List<SelectListItem> ContactList { get; set; }
    }
}