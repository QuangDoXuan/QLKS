namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_createDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "CreateDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "CreateDate");
        }
    }
}
