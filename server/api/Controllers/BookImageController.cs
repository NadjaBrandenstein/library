using api.Dtos;
using api.Dtos.Requests;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class BookImageController : ControllerBase
{
    private readonly ILibraryService<BookImageDto, CreateBookImageDto, UpdateBookImageDto> _libraryService;

    public BookImageController(ILibraryService<BookImageDto, CreateBookImageDto, UpdateBookImageDto> libraryService)
    {
        _libraryService = libraryService;
    }

    [HttpGet(nameof(GetAllBookImages))]
    public async Task<ActionResult<List<BookImageDto>>> GetAllBookImages()
    {
        var bookimages = await _libraryService.GetAll();
        return Ok(bookimages);
    }

    [HttpPost(nameof(CreateBookImage))]
    public async Task<ActionResult<BookImageDto>> CreateBookImage(CreateBookImageDto dto)
    {
        var result = await _libraryService.Create(dto);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpPatch(nameof(UpdateBookImage))]
    public async Task<ActionResult<BookImageDto>> UpdateBookImage(UpdateBookImageDto dto)
    {
        var result = await _libraryService.Update(dto);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

    [HttpDelete(nameof(DeleteBookImage))]
    public async Task<ActionResult<BookImageDto>> DeleteBookImage(string id)
    {
        var result = await _libraryService.Delete(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }

}