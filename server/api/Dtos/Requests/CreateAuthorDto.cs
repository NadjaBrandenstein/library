using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public class CreateAuthorDto
{
    [MinLength(1)]
    [Required]
    public required string Name { get; set; } = null!;
}