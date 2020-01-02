namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_Dob_customer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customer", "Dob", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customer", "Dob");
        }
    }
}
