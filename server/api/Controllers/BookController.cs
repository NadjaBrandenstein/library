using api.Dtos;
using api.Dtos.Requests;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
public class BookController: ControllerBase
{
    private readonly ILibraryService<BookDto, CreateBookDto, UpdateBookDto> _libraryService;

    public BookController(ILibraryService<BookDto, CreateBookDto, UpdateBookDto> libraryService)
    {
        _libraryService = libraryService;
    }
    
    [HttpGet(nameof(GetAllBooks))]
    public async Task<ActionResult<List<BookDto>>> GetAllBooks()
    {
        var books = await _libraryService.GetAll();
        return Ok(books);
    }

    [HttpGet("GetBookById/{id}")]
    public async Task<ActionResult<BookDto>> GetBookById(string id)
    {
        var book = await _libraryService.GetById(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }
    
    [HttpPost(nameof(CreateBook))]
    public async Task<ActionResult<BookDto>> CreateBook ([FromBody] CreateBookDto dto)
    {
        var result = await _libraryService.Create(dto);
        return CreatedAtAction(nameof(GetAllBooks), new  { Id = result.Id }, result);
    }
    
    [HttpPatch(nameof(UpdateBook))]
    public async Task<ActionResult<BookDto>> UpdateBook([FromBody] UpdateBookDto dto)
    {
        var result = await _libraryService.Update(dto);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpDelete(nameof(DeleteBook))]
    public async Task<ActionResult<BookDto>> DeleteBook(string id)
    {
        var result = await _libraryService.Delete(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
}