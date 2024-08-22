namespace BookStore.Client.Models;

public class BookSummary
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Author { get; set; }
    public required string Genre { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
}
