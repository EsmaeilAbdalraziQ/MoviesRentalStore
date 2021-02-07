namespace Cinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMemberShipTypeClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberShipTypes",
                c => new
                    {
                        MemberShipTypeID = c.Int(nullable: false, identity: true),
                        SignUpFee = c.Short(nullable: false),
                        DurationInMonth = c.Byte(nullable: false),
                        DiscountRate = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.MemberShipTypeID);
            
            AddColumn("dbo.Customers", "IsSubscribedToNewsLetter", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "MemberShipTypeID", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "MemberShipTypeID");
            AddForeignKey("dbo.Customers", "MemberShipTypeID", "dbo.MemberShipTypes", "MemberShipTypeID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "MemberShipTypeID", "dbo.MemberShipTypes");
            DropIndex("dbo.Customers", new[] { "MemberShipTypeID" });
            DropColumn("dbo.Customers", "MemberShipTypeID");
            DropColumn("dbo.Customers", "IsSubscribedToNewsLetter");
            DropTable("dbo.MemberShipTypes");
        }
    }
}
