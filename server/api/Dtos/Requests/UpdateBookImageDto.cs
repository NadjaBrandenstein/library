using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public class UpdateBookImageDto
{
    [Required]
    public required string Id { get; set; } = null!;
    
    [MinLength(1)]
    [Required]
    public required string Url { get; set; }
}