using System.ComponentModel.DataAnnotations;

namespace BookStore.Api.Dtos;

public record class CreateBookDto(
    //define data anotations and we use the MinmalApis.Extensions nuget package to use these as validation when doing requests
    [Required][StringLength(50)] string Name, 
    [Required][StringLength(30)] string Author, 
    int GenreId, 
    [Range(1,100)] decimal Price, 
    DateOnly ReleaseDate
);