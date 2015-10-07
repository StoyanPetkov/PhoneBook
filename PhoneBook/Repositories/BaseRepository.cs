using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Entity;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace PhoneBook.Repository
{
    public class BaseRepository<T> where T : BaseEntityWithID,new()
    {
        //Represents the context of the database.
        public DbContext Context { get; set; }

        //Represents a virtual table of the database.
        protected IDbSet<T> DbSet { get; set; }

        //Represents instance of the class - UnitOfWork.
        public UnitOfWork UnitOfWork { get; set; }

        //Represents base(empty) constructor of the base repository.
        public BaseRepository()
        {
            this.Context = new PhoneBookContext();
            this.DbSet = this.Context.Set<T>();
        }

        //Represents a constructor that accepts UnitOfWork class as a parameter.
        //This constructor is called when we have to save data in more than one tables.
        public BaseRepository(UnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentException("An instance of the unitOfWork is null", "unitOfWork");
            }

            this.Context = unitOfWork.Context;
            this.DbSet = this.Context.Set<T>();
            this.UnitOfWork = unitOfWork;
        }

        public IObjectContextAdapter GetObjectContextAdapter()
        {
            return (IObjectContextAdapter)this.Context;
        }

        /// <summary>
        /// Represents a method that gets an object, selected by Id
        /// </summary>
        /// <param name="id">Represent the ID of a certain object</param>
        /// <returns></returns>
        /// 
        public virtual T GetByID(int id)
        {
            return DbSet.Find(id);
        }

        //Represents a method that returns a list of all objects
        public virtual List<T> GetAll(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.ToList();
        }

        #region CRUD
        ///Represents a method that inserts an object into the database.
        ///<param name="Item">Represents an item that will be added into database.
        private void Insert(T item)
        {
            this.DbSet.Add(item);
            Context.SaveChanges();
        }

        /// <summary>
        /// Represents a method that updates an object into database.
        /// </summary>
        /// <param name="item">Represents an item that will be updated into database</param>
        private void Update(T item)
        {
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
        }

        /// <summary>
        /// Represents a method that deletes an object from the database.
        /// </summary>
        /// <param name="item">Represents an item that will be deleted from the database</param>
        public virtual void Delete(T item)
        {
            DbSet.Remove(item);
            Context.SaveChanges();
        }

        /// <summary>
        /// Represents a method that saves an object into database.
        /// </summary>
        /// <param name="item">Represents an object that will be saved into database</param>
        public virtual void Save(T item)
        {
            if (item.Id <= 0)
            {
                Insert(item);
            }
            else
            {
                Update(item);
            }
        }
        #endregion

        //Represents the method that will dispose the context of the database.
        public virtual void Dispose()
        {
            if (this.Context != null)
            {
                this.Context.Dispose();
            }
        }
    }
}