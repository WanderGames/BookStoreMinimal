using BookStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Api.Data;

//inherit from DbContext with is a entityframework class that represents a session with the db and can be used to query and save
// we need to use the bd context options as well and pass in our created BookStoreContent class we are in
public class BookStoreContext(DbContextOptions<BookStoreContext> options) : DbContext(options)
{
    //db set is an object that can be used to query and save the object passed in, so in this case book
    public DbSet<Book> Books => Set<Book>();    //in this case genre
    public DbSet<Genre> Genres => Set<Genre>();

    //this method is executed as soon as a migration is executed
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {   
        //since our genres are static data we can just put them in the db in this method
        //only do this for very simple static data
        modelBuilder.Entity<Genre>().HasData(
            new {Id = 1, Name = "Fantasy",},
            new {Id = 2, Name = "NonFiction",},
            new {Id = 3, Name = "Horror",},
            new {Id = 4, Name = "Science Fiction"},
            new {Id = 5, Name = "Educational"},
            new {Id = 6, Name = "Kids and Family"}
        );
    }
}
