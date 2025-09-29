using api.Dtos;
using api.Dtos.Requests;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class GenreController : ControllerBase
{
    private readonly ILibraryService<GenreDto, CreateGenreDto, UpdateGenreDto> _libraryService;

    public GenreController(ILibraryService<GenreDto, CreateGenreDto, UpdateGenreDto> libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpGet(nameof(GetAllGenres))]
    public async Task<ActionResult<List<GenreDto>>> GetAllGenres()
    {
        var genres = await _libraryService.GetAll();
        return Ok(genres);
    }

    [HttpPost(nameof(CreateGenre))]
    public async Task<ActionResult<GenreDto>> CreateGenre([FromBody] CreateGenreDto dto)
    {
        var result = await _libraryService.Create(dto);
        return CreatedAtAction(nameof(GetAllGenres), new  { Id = result.Id }, result);
    }

    [HttpPatch(nameof(UpdateGenre))]
    public async Task<ActionResult<GenreDto>> UpdateGenre([FromBody] UpdateGenreDto dto)
    {
        var result = await _libraryService.Update(dto);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpDelete(nameof(DeleteGenre))]
    public async Task<ActionResult<GenreDto>> DeleteGenre(string id)
    {
        var result = await _libraryService.Delete(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

}