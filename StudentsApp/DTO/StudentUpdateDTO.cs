using System.ComponentModel.DataAnnotations;

namespace StudentsApp.DTO
{
    public record StudentUpdateDTO
    (
        int Id,

        [property: Required(ErrorMessage = "Firstname is required.")]
        [property: MinLength(2, ErrorMessage = "Firstname must contain at least two characters.")]          
        string? Firstname,

        [property: Required(ErrorMessage = "Lasstname is required.")]
        [property: MinLength(2, ErrorMessage = "Lastname must contain at least two characters.")]
        string? Lastname
    ) : BaseDTO(Id)
    {
        public StudentUpdateDTO() : this(0, string.Empty, string.Empty) 
        { } 
    }
}
