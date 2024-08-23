namespace BookStore.Client.Models;

public class BookDetails
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    public string? GenreId { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
