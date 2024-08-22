namespace BookStore.Api.Entities;

//this is a class entity for entity framework
public class Genre
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
