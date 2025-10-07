using api.Dtos;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookDetailsController : ControllerBase
{
    private readonly BookDetailsService _bookDetailsService; 
    
    public BookDetailsController(BookDetailsService bookDetailsService)
    {
        _bookDetailsService = bookDetailsService;
    }

    [HttpGet(nameof(getAllBookDetails))]
    public async Task<ActionResult<List<BookDetailsDto>>> getAllBookDetails()
    {
        return await _bookDetailsService.getAllBookDetails();
    }

    [HttpGet("GetBookDetailsById/{id}")]
    public async Task<ActionResult<BookDetailsDto>> getBookDetailsById(string id)
    {
        var bookDetails = await _bookDetailsService.getBookDetailsById(id);
        if (bookDetails == null)
        {
            return NotFound();
        }
        return Ok(bookDetails);
    }
}