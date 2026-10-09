using System.ComponentModel.DataAnnotations;

namespace StudentsApp.DTO;

public record StudentInsertDTO
(
    [property: Required(ErrorMessage = "Firstname is required.")]
    [property: MinLength(2, ErrorMessage = "Firstname must contain at least two characters.")]
    string? Firstname,

    [property: Required(ErrorMessage = "Lasstname is required.")]
    [property: MinLength(2, ErrorMessage = "Lastname must contain at least two characters.")]
    string? Lastname
)
{ 
    public StudentInsertDTO() : this(string.Empty, string.Empty) 
    { }
}

