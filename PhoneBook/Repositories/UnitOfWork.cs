using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PhoneBook.Entity;

namespace PhoneBook.Repository
{
    public class UnitOfWork : IDisposable
    {
        private PhoneBookContext context = new PhoneBookContext();

        private DbContextTransaction trans = null;

        public DbContext Context { get; private set; }

        //Represents Base(empty) constructor. Transaction begin here
        public UnitOfWork()
        {
            this.trans = context.Database.BeginTransaction();
            this.Context = context;
        }

        //Represents a method that commits all changes in the database
        public void Commit()
        {
            if (this.trans != null)
            {
                this.trans.Commit();
                this.trans = null;
            }
        }

        //Represents a method that rolls back the transaction and cancels all changes
        public void RollBack()
        {
            if (this.trans != null)
            {
                this.trans.Rollback();
                this.trans = null;
            }
        }

        //Represents a method that dispose the context of the database
        public void Dispose()
        {
            Commit();
            context.Dispose();
        }
    }
}