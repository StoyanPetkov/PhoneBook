using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.Entity
{
    public class Phone : BaseEntityWithID
    {
        //Represent phones of contact
        public int ContactId { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Contact Contact { get; set; }

        public override string ToString()
        {
            return this.PhoneNumber;
        }
    }
}