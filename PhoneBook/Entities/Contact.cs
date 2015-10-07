using PhoneBook.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.Entity
{
    public class Contact : BaseEntityWithID
    {
        //Represent details about contact
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string ImageLocation { get; set; }
        public DateTime BirthDay { get; set; }

        public virtual User User { get; set; }
        
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
    }
}