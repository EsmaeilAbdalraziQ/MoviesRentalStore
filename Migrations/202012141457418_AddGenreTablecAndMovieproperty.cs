namespace Cinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGenreTablecAndMovieproperty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenreName = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "BirthDate", c => c.DateTime());
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "NumberInStock", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "GenreId", c => c.Int(nullable: false));
            DropColumn("dbo.Movies", "StartDate");
            DropColumn("dbo.Movies", "RunningTime");
            DropColumn("dbo.Movies", "Trailer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Movies", "Trailer", c => c.String());
            AddColumn("dbo.Movies", "RunningTime", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "StartDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Movies", "GenreId");
            DropColumn("dbo.Movies", "NumberInStock");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropColumn("dbo.Movies", "DateAdded");
            DropColumn("dbo.Customers", "BirthDate");
            DropTable("dbo.Genres");
        }
    }
}
