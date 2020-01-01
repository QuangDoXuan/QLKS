namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_book_room : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BookRoom", name: "RoomID", newName: "Room_RoomID");
            RenameIndex(table: "dbo.BookRoom", name: "IX_RoomID", newName: "IX_Room_RoomID");
            AddColumn("dbo.BookRoom", "Status", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookRoom", "Status");
            RenameIndex(table: "dbo.BookRoom", name: "IX_Room_RoomID", newName: "IX_RoomID");
            RenameColumn(table: "dbo.BookRoom", name: "Room_RoomID", newName: "RoomID");
        }
    }
}
