using PhoneBook.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.Entity
{
    public class User : BaseEntityWithID
    {
        //Represent names and credentials for users
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Group> Groups { get; set; }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + UserName;
        }
    }
}