namespace Cinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditGenreClassIdToGenreId : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.Genres", "Id", "GenreId");

        }

        public override void Down()
        {
            AddColumn("dbo.Genres", "Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Movies", "GenreId", "dbo.Genres");
            DropPrimaryKey("dbo.Genres");
            DropColumn("dbo.Genres", "GenreId");
            AddPrimaryKey("dbo.Genres", "Id");
            AddForeignKey("dbo.Movies", "GenreId", "dbo.Genres", "Id", cascadeDelete: true);
        }
    }
}
