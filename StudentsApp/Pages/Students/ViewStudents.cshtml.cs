using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsAppSQL9Dev.DTO;
using StudentsAppSQL9Dev.Models;
using StudentsAppSQL9Dev.Services;

namespace StudentsAppSQL9Dev.Pages.Students
{
    public class ViewStudentsModel : PageModel
    {
        public Error ErrorObj { get; set; } = new();

        private readonly IStudentService _studentService;
        public List<StudentReadOnlyDTO> StudentsReadOnlyDTO { get; set; } = [];

        public ViewStudentsModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult OnGet()
        {
            try
            {
                StudentsReadOnlyDTO = _studentService.GetAllStudents();
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ErrorObj = new Error { Message = ex.Message };

            }
            return Page();
        }
    }
}
