namespace DomainTest.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Property",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        AgencyCode = c.String(),
                        Name = c.String(),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Property");
        }
    }
}
