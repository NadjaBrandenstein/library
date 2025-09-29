using System.ComponentModel.DataAnnotations;
using api.Controllers;
using api.Dtos;
using api.Dtos.Requests;
using efscaffold;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class GenreService(MyDbContext dbContext) : ILibraryService<GenreDto, CreateGenreDto, UpdateGenreDto>
{
    public async Task<List<GenreDto>> GetAll()
    {
        return await dbContext.Genres.Select(g => new GenreDto(g)).ToListAsync();
    }
    
    public async Task<GenreDto?> GetById(string id)
    {
        var genre = await dbContext.Genres
            .Include(g => g.Books)
            .FirstOrDefaultAsync(g => g.Id == id);
        return genre == null ? null : new GenreDto(genre);
    }

    public async Task<GenreDto> Create(CreateGenreDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);
        var genre = new Genre
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.Name,
            Createdat = DateTime.UtcNow,
        };
        dbContext.Genres.Add(genre);
        await dbContext.SaveChangesAsync();
        return new GenreDto(genre);
    }

    public async Task<GenreDto?> Update(UpdateGenreDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), true);
        var existingGenre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == dto.Id);
        if (existingGenre == null)
        {
            return null;
        }
        existingGenre.Name = dto.Name;
        dbContext.Genres.Update(existingGenre);
        await dbContext.SaveChangesAsync();
        return new GenreDto(existingGenre);
    }

    public async Task<GenreDto?> Delete(string id)
    {
        var existingGenre = await dbContext.Genres.FirstOrDefaultAsync(g => g.Id == id);
        if (existingGenre == null)
        {
            return null;
        }
        dbContext.Genres.Remove(existingGenre);
        await dbContext.SaveChangesAsync();
        return new GenreDto(existingGenre);
    }

   
}