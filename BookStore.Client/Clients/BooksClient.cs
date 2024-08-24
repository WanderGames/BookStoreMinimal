using BookStore.Client.Models;

namespace BookStore.Client.Clients;

public class BooksClient
{   
    //make this readonly because we will not be destroying or recreating this list
    //only adding to and removing from
    private readonly List<BookSummary> books = 
    [
        new() {
            Id = 1,
            Name = "The Whispering Grove",
            Author = "Ella Dorsey",
            Genre = "Fantasy",
            Price = 15.99m,
            ReleaseDate = new DateOnly(2023, 3, 14)
        },
        new() {
            Id = 2,
            Name = "Shadows of Tomorrow",
            Author = "Mark Rivers",
            Genre = "Science Fiction",
            Price = 18.50m,
            ReleaseDate = new DateOnly(2023, 6, 22)
        },
        new() {
            Id =3,
            Name = "Echoes of the Past",
            Author = "Julia Harper",
            Genre = "Historical Fiction",
            Price = 12.75m,
            ReleaseDate = new DateOnly(2024, 1, 10)
        },
        new() {
            Id = 4,
            Name = "Mystery in the Mist",
            Author = "David Collins",
            Genre = "Mystery",
            Price = 14.25m,
            ReleaseDate = new DateOnly(2023, 11, 5)
        },
        new() {
            Id = 5,
            Name = "Waves of Change",
            Author = "Emily Clarke",
            Genre = "Romance",
            Price = 16.00m,
            ReleaseDate = new DateOnly(2024, 2, 20)
        }  
    ];

    private readonly Genre[] genres = new GenresClient().GetGenres();

    //use a collection expression [.. books] to return this list as an array, this is just like
    //saying books.ToArray()
    public BookSummary[] GetBooks() => [.. books];

    public void AddBook(BookDetails book)
    {   
        //this will throw an exception if genre id is null or white space
        ArgumentException.ThrowIfNullOrWhiteSpace(book.GenreId);
        //find the genre based on the genre id from the book details and parse it to an int
        var genre = genres.Single(genre => genre.Id == int.Parse(book.GenreId));

        var bookSummary = new BookSummary
        {
            Id = books.Count + 1,
            Name  = book.Name,
            Author = book.Author,
            Genre = genre.Name,
            Price = book.Price,
            ReleaseDate = book.ReleaseDate
        };

        books.Add(bookSummary);
    }

}
