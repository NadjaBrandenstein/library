using api.Dtos.Requests;
using api.Services;
using efscaffold;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace api.Test;

public class BookServiceTest
{
    private readonly BookService _bookService;
    private readonly MyDbContext _myDbContext;

    public BookServiceTest(BookService bookService, MyDbContext myDbContext)
    {
        _bookService = bookService;
        _myDbContext = myDbContext;
    }
    
    [Fact]
    public async Task GetAll_ShouldReturnBooks()
    {
        // Arrange
        await _bookService.Create(new CreateBookDto
        {
            Title = "Book Title",
            Pages = 400,
            ImageUrl = "https://example.com/bookimage"
        });

        // Act
        var allBooks = await _bookService.GetAll();

        // Assert
        Assert.NotEmpty(allBooks);
    }
    
    [Fact]
    public async Task GetById_ShouldReturnBook()
    {
        // Arrange
        var book = await _bookService.Create(new CreateBookDto
        {
            Title = "Book title",
            Pages = 250,
            ImageUrl = "https://example.com/bookimage.jpg"
        });
        
        // Act
        var fetched = await  _bookService.GetById(book.Id); 
        
        // Assert
        Assert.NotNull(fetched);
        Assert.Equal("Book title", fetched.Title);
    }
    
    [Fact]
    public async Task Create_ShouldAddBook()
    {
        // Arrange
        var dto = new CreateBookDto
        {
            Title = "Book title",
            Pages = 400,
            ImageUrl = "https://example.com/bookimage"
        };

        // Act
        var result = await _bookService.Create(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.Title, result.Title);
        Assert.True(await _myDbContext.Books.AnyAsync(b => b.Id == result.Id));
    }
    
    [Fact]
    public async Task Update_ShouldUpdateBook()
    {
        // Arrange
        var book = await _bookService.Create(new CreateBookDto
        {
            Title = "Old book title",
            Pages = 405,
            ImageUrl = "https://example.com/Bookimage",
        });

        var updateDto = new UpdateBookDto
        {
            Id = book.Id,
            Title = "New book title",
            Pages = 405,
            ImageUrl = "https://example.com/newbookimage"
        };

        //Act
        var updated = await _bookService.Update(updateDto);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal("New book title", updated.Title);
        Assert.Equal(405, updated.Pages);
    }

    [Fact]
    public async Task Delete_ShouldDeleteBook()
    {
        // Arrange
        var book = await _bookService.Create(new CreateBookDto
        {
            Title = "Book title",
            Pages = 405,
            ImageUrl = "https://example.com/bookimage"
        });
        
        // Act
        var deleted = await _bookService.Delete(book.Id);
        
        // Assert
        Assert.NotNull(deleted);
        Assert.Null(await _bookService.GetById(book.Id));
    }
    
}
