namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_NOP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Room", "NumPerson", c => c.Int(nullable: true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Room", "NumPerson");
        }
    }
}
