namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PKemail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Phones", "Person_PersonId", "dbo.People");
            DropForeignKey("dbo.Photos", "PhotoId", "dbo.People");
            DropIndex("dbo.People", new[] { "Email" });
            DropIndex("dbo.Phones", new[] { "Person_PersonId" });
            DropIndex("dbo.Photos", new[] { "PhotoId" });
            RenameColumn(table: "dbo.Phones", name: "Person_PersonId", newName: "Person_Email");
            DropPrimaryKey("dbo.People");
            DropPrimaryKey("dbo.Photos");
            AlterColumn("dbo.People", "Email", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Phones", "Person_Email", c => c.String(maxLength: 128));
            AlterColumn("dbo.Photos", "PhotoId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.People", "Email");
            AddPrimaryKey("dbo.Photos", "PhotoId");
            CreateIndex("dbo.Phones", "Person_Email");
            CreateIndex("dbo.Photos", "PhotoId");
            AddForeignKey("dbo.Phones", "Person_Email", "dbo.People", "Email");
            AddForeignKey("dbo.Photos", "PhotoId", "dbo.People", "Email");
            DropColumn("dbo.People", "PersonId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "PersonId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Photos", "PhotoId", "dbo.People");
            DropForeignKey("dbo.Phones", "Person_Email", "dbo.People");
            DropIndex("dbo.Photos", new[] { "PhotoId" });
            DropIndex("dbo.Phones", new[] { "Person_Email" });
            DropPrimaryKey("dbo.Photos");
            DropPrimaryKey("dbo.People");
            AlterColumn("dbo.Photos", "PhotoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Phones", "Person_Email", c => c.Int());
            AlterColumn("dbo.People", "Email", c => c.String(nullable: false, maxLength: 60));
            AddPrimaryKey("dbo.Photos", "PhotoId");
            AddPrimaryKey("dbo.People", "PersonId");
            RenameColumn(table: "dbo.Phones", name: "Person_Email", newName: "Person_PersonId");
            CreateIndex("dbo.Photos", "PhotoId");
            CreateIndex("dbo.Phones", "Person_PersonId");
            CreateIndex("dbo.People", "Email", unique: true);
            AddForeignKey("dbo.Photos", "PhotoId", "dbo.People", "PersonId");
            AddForeignKey("dbo.Phones", "Person_PersonId", "dbo.People", "PersonId");
        }
    }
}
