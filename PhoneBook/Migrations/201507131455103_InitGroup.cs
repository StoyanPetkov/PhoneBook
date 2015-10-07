namespace PhoneBook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FullName = c.String(),
                        Email = c.String(),
                        ImageLocation = c.String(),
                        BirthDay = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        Description = c.String(),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.GroupContacts",
                c => new
                    {
                        Group_Id = c.Int(nullable: false),
                        Contact_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.Contact_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .ForeignKey("dbo.Contacts", t => t.Contact_Id)
                .Index(t => t.Group_Id)
                .Index(t => t.Contact_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Notes", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.Groups", "UserID", "dbo.Users");
            DropForeignKey("dbo.Contacts", "UserId", "dbo.Users");
            DropForeignKey("dbo.GroupContacts", "Contact_Id", "dbo.Contacts");
            DropForeignKey("dbo.GroupContacts", "Group_Id", "dbo.Groups");
            DropIndex("dbo.GroupContacts", new[] { "Contact_Id" });
            DropIndex("dbo.GroupContacts", new[] { "Group_Id" });
            DropIndex("dbo.Phones", new[] { "ContactId" });
            DropIndex("dbo.Notes", new[] { "ContactId" });
            DropIndex("dbo.Groups", new[] { "UserID" });
            DropIndex("dbo.Contacts", new[] { "UserId" });
            DropTable("dbo.GroupContacts");
            DropTable("dbo.Phones");
            DropTable("dbo.Notes");
            DropTable("dbo.Users");
            DropTable("dbo.Groups");
            DropTable("dbo.Contacts");
        }
    }
}
