using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PhoneBook.Entity;
using PhoneBook.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PhoneBook
{
    public class PhoneBookContext : DbContext
    {
        public PhoneBookContext() : base("name=PhoneBookContext")
        { }

        public DbSet<User> User { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            //modelBuilder.Entity<Contact>()
            //    .HasMany(p => p.Groups)
            //    .WithMany(t => t.Contacts)
            //    .Map(mc =>
            //    {
            //        mc.ToTable("ContactJoinContactGroup");
            //        mc.MapLeftKey("ContactId");
            //        mc.MapRightKey("ContactGroupId");
            //    });
        }
    }
}