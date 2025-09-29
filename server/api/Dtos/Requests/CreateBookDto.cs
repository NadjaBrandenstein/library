using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public class CreateBookDto
{
    [Range(1, int.MaxValue)]
    [Required]
    public int Pages { get; set; }
    
    [MinLength(1)]
    [Required]
    public required string Title { get; set; } = null!;
}