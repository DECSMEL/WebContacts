namespace EFDAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class birthday : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BirthDays",
                c => new
                    {
                        BirthDayId = c.Int(nullable: false),
                        IsVisible = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false, storeType: "date"),
                        PersonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BirthDayId)
                .ForeignKey("dbo.People", t => t.BirthDayId, cascadeDelete: true)
                .Index(t => t.BirthDayId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BirthDays", "BirthDayId", "dbo.People");
            DropIndex("dbo.BirthDays", new[] { "BirthDayId" });
            DropTable("dbo.BirthDays");
        }
    }
}
