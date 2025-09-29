using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Requests;

public class CreateGenreDto
{
    [MinLength(1)]
    [Required]
    public required string Name { get; set; }
}