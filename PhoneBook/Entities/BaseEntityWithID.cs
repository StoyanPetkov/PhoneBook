using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhoneBook.Entity
{
    public class BaseEntityWithID
    {
        //Represent the ID of all entities
        [Key]
        public int Id { get; set; }
    }
}