namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quiclist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.QuicklistPersons", "Quicklist_QuicklistId", "dbo.Quicklists");
            DropForeignKey("dbo.QuicklistPersons", "Person_PersonId", "dbo.People");
            DropIndex("dbo.QuicklistPersons", new[] { "Quicklist_QuicklistId" });
            DropIndex("dbo.QuicklistPersons", new[] { "Person_PersonId" });
            AddColumn("dbo.People", "Quicklist_QuicklistId", c => c.Int());
            CreateIndex("dbo.People", "Quicklist_QuicklistId");
            AddForeignKey("dbo.People", "Quicklist_QuicklistId", "dbo.Quicklists", "QuicklistId");
            DropTable("dbo.QuicklistPersons");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuicklistPersons",
                c => new
                    {
                        Quicklist_QuicklistId = c.Int(nullable: false),
                        Person_PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Quicklist_QuicklistId, t.Person_PersonId });
            
            DropForeignKey("dbo.People", "Quicklist_QuicklistId", "dbo.Quicklists");
            DropIndex("dbo.People", new[] { "Quicklist_QuicklistId" });
            DropColumn("dbo.People", "Quicklist_QuicklistId");
            CreateIndex("dbo.QuicklistPersons", "Person_PersonId");
            CreateIndex("dbo.QuicklistPersons", "Quicklist_QuicklistId");
            AddForeignKey("dbo.QuicklistPersons", "Person_PersonId", "dbo.People", "PersonId", cascadeDelete: true);
            AddForeignKey("dbo.QuicklistPersons", "Quicklist_QuicklistId", "dbo.Quicklists", "QuicklistId", cascadeDelete: true);
        }
    }
}
