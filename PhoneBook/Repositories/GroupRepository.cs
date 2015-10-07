using PhoneBook.Entities;
using PhoneBook.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace PhoneBook.Repositories
{
    public class GroupRepository : BaseRepository<Group>
    {
        public GroupRepository() : base()
        { }

        public GroupRepository(UnitOfWork Unitofwork) : base(Unitofwork)
        { }

        public override void Save(Group item)
        {
            base.Save(item);
        }

        public override void Delete(Group item)
        {
            base.Delete(item);
        }

        public override List<Group> GetAll(Expression<Func<Group, bool>> filter = null)
        {
            return base.GetAll(filter);
        }

        public override Group GetByID(int id)
        {
            return base.GetByID(id);
        }
    }
}