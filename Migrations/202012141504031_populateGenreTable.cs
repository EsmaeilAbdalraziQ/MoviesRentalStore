namespace Cinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class populateGenreTable : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Genres ON");

            Sql("INSERT INTO Genres (Id, GenreName) VALUES (1, 'Action')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (2, 'Thriller')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (3, 'Family')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (4, 'Romance')");
            Sql("INSERT INTO Genres (Id, GenreName) VALUES (5, 'Comedy')");
        }
        
        public override void Down()
        {
        }
    }
}
