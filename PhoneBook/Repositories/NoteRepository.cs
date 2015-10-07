using PhoneBook.Entities;
using PhoneBook.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Linq.Expressions;

namespace PhoneBook.Repositories
{
    //Represents a class repository that is responsible for CRUD operations of entity - Note
    public class NoteRepository : BaseRepository<Note>
    {
        public NoteRepository() : base()
        { }

        public NoteRepository(UnitOfWork Unitofwork) : base(Unitofwork)
        { }

        public override void Delete(Note item)
        {
            base.Delete(item);
        }

        public override void Save(Note item)
        {
            base.Save(item);
        }

        public override Note GetByID(int id)
        {
            return base.GetByID(id);
        }

        public override List<Note> GetAll(Expression<Func<Note, bool>> filter = null)
        {
            return base.GetAll(filter);
        }
    }
}