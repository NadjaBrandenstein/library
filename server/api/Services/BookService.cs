using System.ComponentModel.DataAnnotations;
using api.Dtos;
using api.Dtos.Requests;
using efscaffold;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class BookService(MyDbContext dbContext) : ILibraryService<BookDto, CreateBookDto, UpdateBookDto>
{
    public async Task<List<BookDto>> GetAll()
    {
        return await dbContext.Books
            .Include(b => b.Authors)
            .Select(b => new BookDto(b))
            .ToListAsync();
    }

    public async Task<BookDto?> GetById(string id)
    {
        var book = await dbContext.Books
            .Include(b => b.Authors)
            .FirstOrDefaultAsync(b => b.Id == id);
        return book == null ? null : new BookDto(book);
    }
    
    public async Task<BookDto> Create(CreateBookDto dto)
    { 
       Validator.ValidateObject(dto, new ValidationContext(dto), true);
       var book = new Book
        {
            Id = Guid.NewGuid().ToString(),
            Title = dto.Title,
            Pages = dto.Pages,
            Createdat = DateTime.UtcNow,
            Imageurl = dto.ImageUrl
        };
        dbContext.Books.Add(book);
        await dbContext.SaveChangesAsync();
        return new BookDto(book);
    }

    public async Task<BookDto?> Update(UpdateBookDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);
        var existingBook = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == dto.Id);
        if (existingBook == null)
        {
            return null;
        }

        existingBook.Title = dto.Title;
        existingBook.Pages = dto.Pages;
        existingBook.Imageurl = dto.ImageUrl;
        await dbContext.SaveChangesAsync();
        return new BookDto(existingBook);
    }

    public async Task<BookDto?> Delete(string id)
    {
        var existingBook = await dbContext.Books.FirstOrDefaultAsync(b => b.Id == id);
        if (existingBook == null)
        {
            return null;
        }
        dbContext.Books.Remove(existingBook);
        await dbContext.SaveChangesAsync();
        return new  BookDto(existingBook);
    }
}