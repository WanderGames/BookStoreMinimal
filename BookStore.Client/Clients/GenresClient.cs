using BookStore.Client.Models;

namespace BookStore.Client.Clients;

public class GenresClient
{
    private readonly Genre[] genres = 
    [
        new(){
            Id = 1,
            Name = "Educational",
        },
        new(){
            Id = 2,
            Name = "Fantasy",
        },
        new(){
            Id = 3,
            Name = "Historical Fiction",
        },
        new(){
            Id = 4,
            Name = "Horror",
        },
        new(){
            Id = 5,
            Name = "Mystery",
        },
        new(){
            Id = 6,
            Name = "Non Fiction",
        },
        new(){
            Id = 7,
            Name = "Romance",
        },
        new(){
            Id = 8,
            Name = "Science Fiction",
        }
    ];

    public Genre[] GetGenres() => genres;
}
