using api.Dtos.Requests;
using api.Services;
using efscaffold;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace api.Test;

public class GenreServiceTest
{
    private readonly GenreService _genreService;
    private readonly MyDbContext _myDbContext;

    public GenreServiceTest(GenreService genreService, MyDbContext myDbContext)
    {
        _genreService = genreService;
        _myDbContext = myDbContext;
    }
    
    [Fact]
    public async Task GetAll_ShouldReturnGenres()
    {
        // Arrange
        await _genreService.Create(new CreateGenreDto
        {
            Name = "Sci-fi",
        });

        // Act
        var allGenres = await _genreService.GetAll();

        // Assert
        Assert.NotEmpty(allGenres);
    }
    
    [Fact]
    public async Task GetById_ShouldReturnGenre()
    {
        // Arrange
        var genre = await _genreService.Create(new CreateGenreDto
        {
            Name = "Genre"
        });
        
        // Act
        var fetched = await  _genreService.GetById(genre.Id); 
        
        // Assert
        Assert.NotNull(fetched);
        Assert.Equal("Genre", fetched.Name);
    }
    
    [Fact]
    public async Task Create_ShouldAddGenre()
    {
        // Arrange
        var dto = new CreateGenreDto
        {
            Name = "Genre"
        };

        // Act
        var result = await _genreService.Create(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.Name, result.Name);
        Assert.True(await _myDbContext.Genres.AnyAsync(g => g.Id == result.Id));
    }
    
    [Fact]
    public async Task Update_ShouldUpdateGenre()
    {
        // Arrange
        var book = await _genreService.Create(new CreateGenreDto
        {
            Name = "Old genre"
        });

        var updateDto = new UpdateGenreDto
        {
            Id = book.Id,
            Name = "New genre"
        };

        //Act
        var updated = await _genreService.Update(updateDto);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal("New genre", updated.Name);
    }

    [Fact]
    public async Task Delete_ShouldDeleteGenre()
    {
        // Arrange
        var book = await _genreService.Create(new CreateGenreDto
        {
            Name = "Genre"
        });
        
        // Act
        var deleted = await _genreService.Delete(genre.Id);
        
        // Assert
        Assert.NotNull(deleted);
        Assert.Null(await _genreService.GetById(book.Id));
    }
    
}
