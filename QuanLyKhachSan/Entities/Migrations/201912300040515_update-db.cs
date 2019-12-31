namespace Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookRoom",
                c => new
                    {
                        BookRoomID = c.Guid(nullable: false),
                        BookRoomNo = c.String(maxLength: 10),
                        StartDate = c.DateTime(storeType: "date"),
                        EndDate = c.DateTime(storeType: "date"),
                        CreateDate = c.DateTime(storeType: "date"),
                        IsCancel = c.Boolean(),
                        Deposit = c.Decimal(precision: 18, scale: 2),
                        CustomerID = c.Guid(),
                        RoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.BookRoomID)
                .ForeignKey("dbo.Room", t => t.RoomID)
                .Index(t => t.RoomID);
            
            CreateTable(
                "dbo.Payment",
                c => new
                    {
                        PaymentID = c.Guid(nullable: false),
                        PaymentNo = c.String(maxLength: 10),
                        Date = c.DateTime(storeType: "date"),
                        TotalCost = c.Decimal(precision: 18, scale: 2),
                        BookRoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.PaymentID)
                .ForeignKey("dbo.BookRoom", t => t.BookRoomID)
                .Index(t => t.BookRoomID);
            
            CreateTable(
                "dbo.Room",
                c => new
                    {
                        RoomID = c.Guid(nullable: false),
                        RoomNo = c.String(maxLength: 10),
                        RoomName = c.String(maxLength: 100),
                        NoP = c.String(maxLength: 10),
                        Price = c.Decimal(precision: 18, scale: 2),
                        Floor = c.Int(),
                        StatusStay = c.String(maxLength: 50),
                        Status = c.String(maxLength: 50),
                        TypeRoomID = c.Guid(),
                    })
                .PrimaryKey(t => t.RoomID)
                .ForeignKey("dbo.TypeRoom", t => t.TypeRoomID)
                .Index(t => t.TypeRoomID);
            
            CreateTable(
                "dbo.Equipment",
                c => new
                    {
                        RoomID = c.Guid(nullable: false),
                        DeviceID = c.Guid(nullable: false),
                        Quantity = c.Int(),
                    })
                .PrimaryKey(t => new { t.RoomID, t.DeviceID })
                .ForeignKey("dbo.Device", t => t.DeviceID, cascadeDelete: true)
                .ForeignKey("dbo.Room", t => t.RoomID, cascadeDelete: true)
                .Index(t => t.RoomID)
                .Index(t => t.DeviceID);
            
            CreateTable(
                "dbo.Device",
                c => new
                    {
                        DeviceID = c.Guid(nullable: false),
                        DeviceNo = c.String(maxLength: 10),
                        DeviceName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.DeviceID);
            
            CreateTable(
                "dbo.TypeRoom",
                c => new
                    {
                        TypeRoomID = c.Guid(nullable: false),
                        TypeRoomNo = c.String(maxLength: 10),
                        TypeRoomName = c.String(maxLength: 100),
                        BasicPrice = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TypeRoomID);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerID = c.Guid(nullable: false),
                        CustomerNo = c.String(maxLength: 10),
                        Name = c.String(maxLength: 100),
                        Gender = c.Boolean(),
                        Address = c.String(),
                        Phone_Fax = c.String(maxLength: 10),
                        Email = c.String(maxLength: 100),
                        IdentityCard = c.String(maxLength: 12),
                        Nationality = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.CustomerID);
            
            CreateTable(
                "dbo.Employee",
                c => new
                    {
                        EmployeeID = c.Guid(nullable: false),
                        EmployeeNo = c.String(maxLength: 10),
                        Name = c.String(maxLength: 100),
                        Gender = c.Boolean(),
                        Address = c.String(),
                        Birthday = c.DateTime(storeType: "date"),
                        Phone = c.String(maxLength: 11),
                        IdentityCard = c.String(maxLength: 12),
                        Email = c.String(maxLength: 100),
                        Position = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.EmployeeID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Room", "TypeRoomID", "dbo.TypeRoom");
            DropForeignKey("dbo.Equipment", "RoomID", "dbo.Room");
            DropForeignKey("dbo.Equipment", "DeviceID", "dbo.Device");
            DropForeignKey("dbo.BookRoom", "RoomID", "dbo.Room");
            DropForeignKey("dbo.Payment", "BookRoomID", "dbo.BookRoom");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Equipment", new[] { "DeviceID" });
            DropIndex("dbo.Equipment", new[] { "RoomID" });
            DropIndex("dbo.Room", new[] { "TypeRoomID" });
            DropIndex("dbo.Payment", new[] { "BookRoomID" });
            DropIndex("dbo.BookRoom", new[] { "RoomID" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Employee");
            DropTable("dbo.Customer");
            DropTable("dbo.TypeRoom");
            DropTable("dbo.Device");
            DropTable("dbo.Equipment");
            DropTable("dbo.Room");
            DropTable("dbo.Payment");
            DropTable("dbo.BookRoom");
        }
    }
}
