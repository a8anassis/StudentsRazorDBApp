using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace StudentsApp.DTO
{
    public abstract record BaseDTO
    (
        [property: BindRequired]
        int Id
    )
    {
        public BaseDTO() : this(0) 
        { }
    }
}
