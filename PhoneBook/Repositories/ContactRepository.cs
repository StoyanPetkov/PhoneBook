using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Entity;
using System.Linq.Expressions;
using PhoneBook.Services;

namespace PhoneBook.Repository
{
    //Represents a class repository that is responsible for the CRUD operation of the entity - Contact
    public class ContactRepository : BaseRepository<Contact>
    {
        public ContactRepository():base()
        { }

        public ContactRepository(UnitOfWork unitOfWork):base(unitOfWork)
        { }

        public override void Save(Contact contact)
        {
            base.Save(contact);
        }

        public override List<Contact> GetAll(Expression<Func<Contact, bool>> filter = null)
        {
            List<Contact> contactList = base.GetAll(filter);
            return contactList;
        }

        public override Contact GetByID(int id)
        {
            Contact contact = base.GetByID(id);

            if (contact != null)
            {
                try
                {
                    DbSet.Attach(contact);
                }

                catch { }
            }
            return contact;
        }

        public override void Delete(Contact item)
        {
            base.Delete(item);
        }
    }
}