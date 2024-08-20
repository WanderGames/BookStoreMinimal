namespace BookStore.Api.Dtos;

public record class CreateBookDto(
    string Name, 
    string Author, 
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate
);