using api.Dtos.Requests;
using api.Services;
using efscaffold;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace api.Test;

public class AuthorServiceTest : IClassFixture<TestFixture>
{
    private readonly AuthorService _authorService;
    private readonly MyDbContext _dbContext;

    public AuthorServiceTest(TestFixture fixture)
    {
        _authorService = fixture.AuthorService;
        _dbContext = fixture.DbContext;
    }
    
    [Fact]
    public async Task GetAll_ShouldReturnAuthors()
    {
        // Arrange
        await _authorService.Create(new CreateAuthorDto { Name = "Karl Heinz Müller" });

        // Act
        var allAuthors = await _authorService.GetAll();

        // Assert
        Assert.NotEmpty(allAuthors);
    }
    
    [Fact]
    public async Task GetById_ShouldReturnAuthor()
    {
        // Arrange
        var author = await _authorService.Create(new CreateAuthorDto
        {
            Name = "Karl Heinz Müller",
        });
        
        // Act
        var fetched = await  _authorService.GetById(author.Id); 
        
        // Assert
        Assert.NotNull(fetched);
        Assert.Equal("Karl Heinz Müller", fetched.Name);
    }
    
    [Fact]
    public async Task Create_ShouldAddAuthor()
    {
        // Arrange
        var dto = new CreateAuthorDto
        {
            Name = "Karl Heinz Müller",
        };

        // Act
        var result = await _authorService.Create(dto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(dto.Name, result.Name);
        Assert.True(await _dbContext.Authors.AnyAsync(a => a.Id == result.Id));
    }
    
    [Fact]
    public async Task Update_ShouldUpdateAuthor()
    {
        // Arrange
        var author = await _authorService.Create(new CreateAuthorDto
        {
            Name = "New author name",
        });

        var updateDto = new UpdateAuthorDto
        {
            Id = author.Id,
            Name = "Karl Heinz Schneider",
        };

        //Act
        var updated = await _authorService.Update(updateDto);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal("Karl Heinz Schneider", updated.Name);
    }

    [Fact]
    public async Task Delete_ShouldDeleteAuthor()
    {
        // Arrange
        var author = await _authorService.Create(new CreateAuthorDto
        {
            Name = "Karl Heinz Schneider",
        });
        
        // Act
        var deleted = await _authorService.Delete(author.Id);
        
        // Assert
        Assert.NotNull(deleted);
        Assert.Null(await _authorService.GetById(author.Id));
    }
    
}
