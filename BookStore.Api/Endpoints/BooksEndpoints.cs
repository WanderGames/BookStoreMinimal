using BookStore.Api.Dtos;

namespace BookStore.Api.Endpoints;

//this is a static class with extension methods
public static class BooksEndpoints
{
    const string GetBookEndpointName = "Get Book";
    private static readonly List<BookDto> books = [
        new (
            1,
            "Name Of The Wind",
            "Patrick Rothfuss",
            "Fantasy",
            15.99m,
            new DateOnly(2009, 05, 15)),
        new (
            2,
            "Fourth Wing",
            "Rebecca Yarros",
            "Fantasy",
            10.99m,
            new DateOnly(2023, 05, 02)),
        new (
            3,
            "Apps and Services with .NET 8",
            "Mark J. Price",
            "Nonfiction",
            29.99m,
            new DateOnly(2023, 12, 12))
    ];


    //extends the WebApplication class to we can mapp all our routes in here
    //to make it an extention method we need to add this infront of what we are extending (this WebApplication app)
    public static RouteGroupBuilder MapBooksEndpoints(this WebApplication app)
    {   
        //create a group that defines something that is common among all endpoints 
        //ex. this allows us to change the books/{id} to just be /{id} because the / will be books/ thanks to our group
        var group = app.MapGroup("books");

        //GET /books  (gets all books)
        group.MapGet("/", () => books);

        //GET /books/1   (gets a book with id)
        group.MapGet("/{id}", (int id) =>
        {
            //Find() is nullable so we can either receive a book or null
            BookDto? book = books.Find(book => book.Id == id);

            //if book is null return not found else return the book
            return book is null ? Results.NotFound() : Results.Ok(book);

        }).WithName(GetBookEndpointName);

        //POST /books   (creates a new book)
        group.MapPost("/", (CreateBookDto newBook) =>
        {
            //create a book using our new book that was passed in
            BookDto book = new(
                books.Count + 1,
                newBook.Name,
                newBook.Author,
                newBook.Genre,
                newBook.Price,
                newBook.ReleaseDate
            );

            //add the book to our list of books
            books.Add(book);

            //provide a location header to the client so it knows exactly where to find this resource, 
            //we use our with name we created above then pass the id and the object
            return Results.CreatedAtRoute(GetBookEndpointName, new { book.Id }, book);
        });

        //PUT /books/1   (update a book based on id)
        group.MapPut("/{id}", (int id, UpdateBookDto updatedBook) =>
        {
            //get the index of the book based on its id
            var index = books.FindIndex(book => book.Id == id);

            //FindIndex() will return a -1 if the book isnt found
            if (index == -1) return Results.NotFound();

            //to update we basically create a new book at the existing id (ie overwrite it with the updated changes)
            books[index] = new BookDto(
                id,
                updatedBook.Name,
                updatedBook.Author,
                updatedBook.Genre,
                updatedBook.Price,
                updatedBook.ReleaseDate
            );

            //since this is an update convention says to just return no content
            return Results.NoContent();

        });

        //DELETE /books/1   (delete a book based on id)
        group.MapDelete("/{id}", (int id) =>
        {
            //delete the book with the id
            books.RemoveAll(book => book.Id == id);

            //on a delete the convention is to return a no content
            return Results.NoContent();
        });

        return group;
    }
}
