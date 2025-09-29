using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public class UpdateBookDto
{
    public string Id { get; set; } = null!;
    
    [Range(1, int.MaxValue)]
    [Required]
    public int Pages { get; set; }
    
    [MinLength(1)]
    [Required]
    public required string Title { get; set; } = null!;
}