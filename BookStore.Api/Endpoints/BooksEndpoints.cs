using BookStore.Api.Data;
using BookStore.Api.Dtos;
using BookStore.Api.Entities;
using BookStore.Api.Mapping;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Endpoints;

//this is a static class with extension methods
public static class BooksEndpoints
{
    const string GetBookEndpointName = "Get Book";

    //extends the WebApplication class to we can mapp all our routes in here
    //to make it an extention method we need to add this infront of what we are extending (this WebApplication app)
    public static RouteGroupBuilder MapBooksEndpoints(this WebApplication app)
    {
        //create a group that defines something that is common among all endpoints 
        //ex. this allows us to change the books/{id} to just be /{id} because the / will be books/ thanks to our group
        //this paramater validation method is provided by our minimalapis.extensions nuget package and uses our data annotations we set up to validate the object created
        var group = app.MapGroup("books").WithParameterValidation();

        //GET /books  (gets all books)
        group.MapGet("/", async (BookStoreContext dbContext) =>
            //for each book in our books collections return them as a summarydto
            //we also need to include the genre name so we can get the name since our summary dto shows the name not the genreid
            //we also want to optomize this so entiity framework does not keep track of these since we are just sending them to the client right away (its a get)
            //we need to use await and tolist async since this is an asynchronous call
            await dbContext.Books.Include(book => book.Genre).Select(book => book.ToBookSummaryDto()).AsNoTracking().ToListAsync());

        //GET /books/1   (gets a book with id)
        group.MapGet("/{id}", async (int id, BookStoreContext dbContext) =>
        {
            //Find() is nullable so we can either receive a book or null
            Book? book = await dbContext.Books.FindAsync(id);

            //if book is null return not found else return the book detials dto
            return book is null ? Results.NotFound() : Results.Ok(book.ToBookDetailsDto());

        }).WithName(GetBookEndpointName);

        //POST /books   (creates a new book) and inject our db context
        group.MapPost("/", async (CreateBookDto newBook, BookStoreContext dbContext) =>
        {
            //use our extension method to make our book entity
            Book book = newBook.ToEntity();

            //add the book to our db
            dbContext.Books.Add(book);
            //save the changes by translating the stored data into sql statements wich runs against our db
            await dbContext.SaveChangesAsync();

            //provide a location header to the client so it knows exactly where to find this resource, 
            //we use our with name we created above then pass the id and the object using our toDto extension method to map it
            return Results.CreatedAtRoute(GetBookEndpointName, new { book.Id }, book.ToBookDetailsDto());
        });

        //PUT /books/1   (update a book based on id)
        group.MapPut("/{id}", async (int id, UpdateBookDto updatedBook, BookStoreContext dbContext) =>
        {
            //get the existing game we are updating
            var existingBook = await dbContext.Books.FindAsync(id);

            //find return null if it doesnt find anything
            if (existingBook == null) return Results.NotFound();

            //to update we need to get the entry that is our existing game
            //get the current values and then set them to the values of our updated book and use our to entity extension method to map it
            dbContext.Entry(existingBook).CurrentValues.SetValues(updatedBook.ToEntity(id));
            await dbContext.SaveChangesAsync();

            //since this is an update convention says to just return no content
            return Results.NoContent();

        });

        //DELETE /books/1   (delete a book based on id)
        group.MapDelete("/{id}", async (int id, BookStoreContext dbContext) =>
        {
            //delete the book with the id
            await dbContext.Books.Where(book => book.Id == id).ExecuteDeleteAsync();

            //on a delete the convention is to return a no content
            return Results.NoContent();
        });

        return group;
    }
}
