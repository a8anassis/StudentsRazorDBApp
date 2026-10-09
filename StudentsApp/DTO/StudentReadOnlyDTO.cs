namespace StudentsApp.DTO
{
    public record StudentReadOnlyDTO(
        int Id,
        string? Firstname,
        string? Lastname
        
    ) : BaseDTO(Id)
    {
        public StudentReadOnlyDTO() : this(0, string.Empty, string.Empty) { }
    }
}
