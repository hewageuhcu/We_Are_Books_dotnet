using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Isbn { get; set; }
    public DateTime PublicationDate { get; set; }
}

private static List<Book> _books = new List<Book>()
{
    new Book {Id = 1, Title = "Book 1", Author = "Author 1", Isbn = "123-456-789", PublicationDate = new DateTime(2020, 1, 1)},
    new Book {Id = 2, Title = "Book 2", Author = "Author 2", Isbn = "987-654-321", PublicationDate = new DateTime(2021, 2, 2)}
};

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    [HttpGet]
    public List<Book> Get()
    {
        return _books;
    }

    [HttpGet("{id}")]
    public Book GetById(int id)
    {
        return _books.Find(book => book.Id == id);
    }

    [HttpPost]
    public Book Post(Book book)
    {
        if (book == null || string.IsNullOrWhiteSpace(book.Title) || string.IsNullOrWhiteSpace(book.Author) || string.IsNullOrWhiteSpace(book.Isbn))
            return null;

        _books.Add(book);
        return book;
    }

    [HttpPut("{id}")]
    public Book Put(int id, Book book)
    {
        if (book == null || !_books.Any(b => b.Id == id))
            return null;

        var bookToUpdate = _books.Find(b => b.Id == id);
        bookToUpdate.Title = book.Title;
        bookToUpdate.Author = book.Author;
        bookToUpdate.Isbn = book.Isbn;
        bookToUpdate.PublicationDate = book.PublicationDate;
        return bookToUpdate;
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var bookToDelete = _books.Find(b => b.Id == id);
        if (bookToDelete == null)
            return NotFound();

        _books.Remove(bookToDelete);
        return Ok();
    }
}