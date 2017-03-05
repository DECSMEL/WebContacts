namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class privateField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.People", "IsPrivatePhoto", c => c.Boolean(nullable: false));
            AddColumn("dbo.People", "IsPrivateBirthDay", c => c.Boolean(nullable: false));
            DropColumn("dbo.BirthDays", "IsVisible");
            DropColumn("dbo.BirthDays", "PersonId");
            DropColumn("dbo.Photos", "IsPrivate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Photos", "IsPrivate", c => c.Boolean(nullable: false));
            AddColumn("dbo.BirthDays", "PersonId", c => c.Int(nullable: false));
            AddColumn("dbo.BirthDays", "IsVisible", c => c.Boolean(nullable: false));
            DropColumn("dbo.People", "IsPrivateBirthDay");
            DropColumn("dbo.People", "IsPrivatePhoto");
        }
    }
}
