namespace Cinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateMemberShipType : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT MemberShipTypes ON");
            Sql("INSERT INTO MemberShipTypes (MemberShipTypeID, SignUpFee, DurationInMonth, DiscountRate) VALUES (1,0,0,0)");
            Sql("INSERT INTO MemberShipTypes (MemberShipTypeID, SignUpFee, DurationInMonth, DiscountRate) VALUES (2,30,1,10)");
            Sql("INSERT INTO MemberShipTypes (MemberShipTypeID, SignUpFee, DurationInMonth, DiscountRate) VALUES (3,90,3,15)");
            Sql("INSERT INTO MemberShipTypes (MemberShipTypeID, SignUpFee, DurationInMonth, DiscountRate) VALUES (4,150,12,20)");
        }
        
        public override void Down()
        {
        }
    }
}
