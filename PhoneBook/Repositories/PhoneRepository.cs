using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PhoneBook.Entity;

namespace PhoneBook.Repository
{
    //Represents a class repository that is responsible for CRUD operations of entity Phone
    public class PhoneRepository : BaseRepository<Phone>
    {
        public PhoneRepository():base()
        { }

        public PhoneRepository(UnitOfWork unitOfWork):base(unitOfWork)
        { }

        public override void Save(Phone number)
        {
            base.Save(number);
        }

        public override List<Phone> GetAll(Expression<Func<Phone, bool>> filter = null)
        {
            return base.GetAll(filter);
        }

        public override Phone GetByID(int id)
        {
            return base.GetByID(id);
        }

        public override void Delete(Phone item)
        {
            base.Delete(item);
        }
    }
}