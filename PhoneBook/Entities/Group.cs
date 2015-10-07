using PhoneBook.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Entities
{
    public class Group : BaseEntityWithID
    {
        public string GroupName { get; set; }
        public int UserID { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}