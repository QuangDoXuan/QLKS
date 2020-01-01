namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update_booking : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookRoomDetails",
                c => new
                    {
                        BookRoomID = c.Guid(nullable: false),
                        RoomID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.BookRoomID, t.RoomID });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.BookRoomDetails");
        }
    }
}
