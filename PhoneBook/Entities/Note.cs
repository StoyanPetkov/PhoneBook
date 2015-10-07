using PhoneBook.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.Entities
{
    public class Note : BaseEntityWithID
    {
        public int ContactId { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }

        public virtual Contact Contact { get; set; }
        public override string ToString()
        {
            return this.Description + ": " + this.Text;
        }
    }
}