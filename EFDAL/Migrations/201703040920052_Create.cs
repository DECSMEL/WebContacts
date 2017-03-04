namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PersonId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 100),
                        Password = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.PersonId)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        PhoneId = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Number = c.String(maxLength: 50),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhoneId)
                .ForeignKey("dbo.People", t => t.PersonId, cascadeDelete: true)
                .Index(t => t.PersonId);
            
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false),
                        IsPrivate = c.Boolean(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.People", t => t.PhotoId, cascadeDelete: true)
                .Index(t => t.PhotoId);
            
            CreateTable(
                "dbo.Quicklists",
                c => new
                    {
                        QuicklistId = c.Int(nullable: false),
                        Name = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.QuicklistId);
            
            CreateTable(
                "dbo.QuicklistPersons",
                c => new
                    {
                        Quicklist_QuicklistId = c.Int(nullable: false),
                        Person_PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Quicklist_QuicklistId, t.Person_PersonId })
                .ForeignKey("dbo.Quicklists", t => t.Quicklist_QuicklistId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_PersonId, cascadeDelete: true)
                .Index(t => t.Quicklist_QuicklistId)
                .Index(t => t.Person_PersonId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.QuicklistPersons", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.QuicklistPersons", "Quicklist_QuicklistId", "dbo.Quicklists");
            DropForeignKey("dbo.Photos", "PhotoId", "dbo.People");
            DropForeignKey("dbo.Phones", "PersonId", "dbo.People");
            DropIndex("dbo.QuicklistPersons", new[] { "Person_PersonId" });
            DropIndex("dbo.QuicklistPersons", new[] { "Quicklist_QuicklistId" });
            DropIndex("dbo.Photos", new[] { "PhotoId" });
            DropIndex("dbo.Phones", new[] { "PersonId" });
            DropIndex("dbo.People", new[] { "Email" });
            DropTable("dbo.QuicklistPersons");
            DropTable("dbo.Quicklists");
            DropTable("dbo.Photos");
            DropTable("dbo.Phones");
            DropTable("dbo.People");
        }
    }
}
