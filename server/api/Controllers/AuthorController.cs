using api.Dtos;
using api.Dtos.Requests;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class AuthorController: ControllerBase
{
    private readonly ILibraryService<AuthorDto, CreateAuthorDto, UpdateAuthorDto> _libraryService;

    public AuthorController(ILibraryService<AuthorDto, CreateAuthorDto, UpdateAuthorDto> libraryService)
    {
        _libraryService = libraryService;
    }
    
    [HttpGet(nameof(GetAllAuthors))]
    public async Task<ActionResult<AuthorDto>> GetAllAuthors(AuthorDto dto)
    {
        var authors = await _libraryService.GetAll();
        return Ok(authors);
    }

    [HttpPost(nameof(CreateAuthor))]
    public async Task<ActionResult<AuthorDto>> CreateAuthor([FromBody] CreateAuthorDto dto)
    {
        var result = await _libraryService.Create(dto);
        return CreatedAtAction(nameof(GetAllAuthors), new { Id = result.Id }, result);
    }

    [HttpPatch(nameof(UpdateAuthor))]
    public async Task<ActionResult<AuthorDto>> UpdateAuthor([FromBody] UpdateAuthorDto dto)
    {
        var result = await _libraryService.Update(dto);
        return Ok(result);
    }

    [HttpDelete(nameof(DeleteAuthor))]
    public async Task<ActionResult<AuthorDto>> DeleteAuthor(string id)
    {
        var result = await _libraryService.Delete(id);
        return Ok(result);
    }
    
}