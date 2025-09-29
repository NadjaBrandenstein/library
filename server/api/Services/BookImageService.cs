using System.ComponentModel.DataAnnotations;
using api.Dtos;
using api.Dtos.Requests;
using efscaffold;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class BookImageService(MyDbContext dbContext) : ILibraryService<BookImageDto, CreateBookImageDto, UpdateBookImageDto>
{
    public async Task<List<BookImageDto>> GetAll()
    {
        return await dbContext.Bookimages.Select(b => new BookImageDto(b)).ToListAsync();
    }

    public async Task<BookImageDto?> GetById(string id)
    {
        var bookImage = await dbContext.Bookimages
            .Include(b => b.Book)
            .FirstOrDefaultAsync(b => b.Id == id);
        return  bookImage == null ? null : new BookImageDto(bookImage);
    }
    
    public async Task<BookImageDto> Create(CreateBookImageDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);
        var bookimage = new Bookimage
        {
            Id = Guid.NewGuid().ToString(),
            Url = dto.Url,
            Createdat = DateTime.UtcNow,
        };
        dbContext.Bookimages.Add(bookimage);
        await dbContext.SaveChangesAsync();
        return new BookImageDto(bookimage);
    }

    public async Task<BookImageDto?> Update(UpdateBookImageDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);
        var existingBookimage = await dbContext.Bookimages.FirstOrDefaultAsync(b => b.Id == dto.Id);
        if (existingBookimage == null)
        {
            return null;
        }
        existingBookimage.Url = dto.Url;
        dbContext.Bookimages.Update(existingBookimage);
        await dbContext.SaveChangesAsync();
        return new BookImageDto(existingBookimage);
    }

    public async Task<BookImageDto?> Delete(string id)
    {
        var existingBookimage = await dbContext.Bookimages.FirstOrDefaultAsync(b => b.Id == id);
        if (existingBookimage == null)
        {
            return null;
        }
        dbContext.Bookimages.Remove(existingBookimage);
        await dbContext.SaveChangesAsync();
        return new BookImageDto(existingBookimage);
    }
}