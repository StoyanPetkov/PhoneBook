using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PhoneBook.Entity;
using System.Linq.Expressions;

namespace PhoneBook.Repository
{
    //Represents class repository that is responsible for CRUD operation of the entity - User
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository():base()
        { }

        public UserRepository(UnitOfWork unitOfWork):base(unitOfWork)
        { }

        public override void Save(User user)
        {
            base.Save(user);
        }

        public override User GetByID(int id)
        {
            User user = base.GetByID(id);

            if (user != null)
            {
                try
                {
                    DbSet.Attach(user);
                }

                catch { }
            }
            return user;
        }

        public override List<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            List<User> users = base.GetAll(filter);
            if (users != null && users.Count > 0)
            {
                foreach (User user in users)
                {
                    try
                    {
                        DbSet.Attach(user);
                    }
                    catch { }
                }
            }
            return users;
        }

        public override void Delete(User item)
        {
            base.Delete(item);
        }
    }
}