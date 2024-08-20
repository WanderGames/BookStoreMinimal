namespace BookStore.Api.Dtos;

public record class UpdateBookDto(
    string Name, 
    string Author, 
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate
);
