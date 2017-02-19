namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtomanyToMany : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.People", "Quicklist_QuicklistId", "dbo.Quicklists");
            DropIndex("dbo.People", new[] { "Quicklist_QuicklistId" });
            CreateTable(
                "dbo.QuicklistPersons",
                c => new
                    {
                        Quicklist_QuicklistId = c.Int(nullable: false),
                        Person_Email = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Quicklist_QuicklistId, t.Person_Email })
                .ForeignKey("dbo.Quicklists", t => t.Quicklist_QuicklistId, cascadeDelete: true)
                .ForeignKey("dbo.People", t => t.Person_Email, cascadeDelete: true)
                .Index(t => t.Quicklist_QuicklistId)
                .Index(t => t.Person_Email);
            
            DropColumn("dbo.People", "Quicklist_QuicklistId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.People", "Quicklist_QuicklistId", c => c.Int());
            DropForeignKey("dbo.QuicklistPersons", "Person_Email", "dbo.People");
            DropForeignKey("dbo.QuicklistPersons", "Quicklist_QuicklistId", "dbo.Quicklists");
            DropIndex("dbo.QuicklistPersons", new[] { "Person_Email" });
            DropIndex("dbo.QuicklistPersons", new[] { "Quicklist_QuicklistId" });
            DropTable("dbo.QuicklistPersons");
            CreateIndex("dbo.People", "Quicklist_QuicklistId");
            AddForeignKey("dbo.People", "Quicklist_QuicklistId", "dbo.Quicklists", "QuicklistId");
        }
    }
}
