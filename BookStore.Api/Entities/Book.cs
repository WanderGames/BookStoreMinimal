namespace BookStore.Api.Entities;
//we are using sqllite in this project
public class Book
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    //since this is for entity framework table we need to set up the genre id and then the genre table itself so we use the id as the relation
    public int GenreId { get; set; }
    public Genre? Genre { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
