using efscaffold;

namespace api.Dtos;

public class BookDto
{
    public BookDto(Book book)
    {
        Id = book.Id;
        Title = book.Title;
        Pages = book.Pages;
        Createdat = book.Createdat;
        Genreid = book.Genreid;
        AuthorsIds = book.Authors?.Select(a => a.Id).ToList() ?? new  List<string>();
        ImageUrl = book.Imageurl;
    }

    public string Id { get; set; } = null!;
    
    public string Title { get; set; } = null!;
    
    public int Pages { get; set; }
    
    public DateTime? Createdat { get; set; }
    
    public string? Genreid { get; set; }

    public List<string> AuthorsIds { get; set; } 
    
    public string? ImageUrl { get; set; }
}