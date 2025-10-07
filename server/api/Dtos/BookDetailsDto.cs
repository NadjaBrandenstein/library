using efscaffold;

namespace api.Dtos;

public class BookDetailsDto
{
    public BookDetailsDto(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Pages = book.Pages;
        Createdat = book.Createdat;
        ImageUrl = book.Imageurl;

        Genre = book.Genre != null
            ? new GenreDto(book.Genre)
            : null;

        Authors = book.Authors?
            .Select(a => new AuthorDto(a))
            .ToList()
            ?? new List<AuthorDto>();
    }

    public string Id { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public int Pages { get; set; }
    
    public DateTime? Createdat { get; set; }
    
    public string? ImageUrl { get; set; }
    
    public GenreDto? Genre { get; set; }

    public List<AuthorDto> Authors { get; set; } = new();
}