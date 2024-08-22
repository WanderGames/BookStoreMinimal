using BookStore.Api.Dtos;
using BookStore.Api.Entities;

namespace BookStore.Api.Mapping;

public static class BookMapping
{
    //this extension method maps a createbookdto into a book entity
    public static Book ToEntity(this CreateBookDto book)
    {
        //create and return a book using our new book that was passed in
        return new Book()
        {
            Name = book.Name,
            Author = book.Author,
            GenreId = book.GenreId,
            Price = book.Price,
            ReleaseDate = book.ReleaseDate
        };
    }

    //this extension method maps a updatebookdto into a book entity
    //since this is an update map we need the id of what we are updating
    public static Book ToEntity(this UpdateBookDto book, int id)
    {
        //create and return a book using our new book that was passed in
        return new Book()
        {
            Id = id,
            Name = book.Name,
            Author = book.Author,
            GenreId = book.GenreId,
            Price = book.Price,
            ReleaseDate = book.ReleaseDate
        };
    }

    //this extension method maps a book entity into a booksummardto
    public static BookSummaryDto ToBookSummaryDto(this Book book)
    {
        //since genre in required we know it will never be null here so use the ! to tell our ide this is not null
        return new(
            book.Id,
            book.Name,
            book.Author,
            book.Genre!.Name,
            book.Price,
            book.ReleaseDate
        );
    }

     //this extension method maps a book entity into a bookdetaildto
    public static BookDetailsDto ToBookDetailsDto(this Book book)
    {
        return new(
            book.Id,
            book.Name,
            book.Author,
            book.GenreId,
            book.Price,
            book.ReleaseDate
        );
    }
}
