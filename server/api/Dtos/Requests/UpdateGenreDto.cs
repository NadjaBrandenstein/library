using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public class UpdateGenreDto
{
    public string Id { get; set; } = null!;
    
    [MinLength(1)]
    [Required]
    public required string Name { get; set; }
}