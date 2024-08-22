namespace BookStore.Api.Dtos;

public record class BookDetailsDto(
    int Id, 
    string Name, 
    string Author, 
    int GenreId, 
    decimal Price, 
    DateOnly ReleaseDate
);
