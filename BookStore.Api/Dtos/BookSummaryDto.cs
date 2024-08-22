namespace BookStore.Api.Dtos;

public record class BookSummaryDto(
    int Id, 
    string Name, 
    string Author, 
    string Genre, 
    decimal Price, 
    DateOnly ReleaseDate
);
