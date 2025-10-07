using api.Dtos;
using efscaffold;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class BookDetailsService(MyDbContext dbContext)
{
    public async Task<List<BookDetailsDto>> getAllBookDetails()
    {
        return await dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Genre)
            .Select(b => new BookDetailsDto(b))
            .ToListAsync();
    }

    public async Task<BookDetailsDto?> getBookDetailsById(string id)
    {
        var bookDetails = await dbContext.Books
            .Include(b => b.Authors)
            .Include(b => b.Genre)
            .FirstOrDefaultAsync(b => b.Id == id);
        return  bookDetails == null ? null: new BookDetailsDto(bookDetails);
    }
}