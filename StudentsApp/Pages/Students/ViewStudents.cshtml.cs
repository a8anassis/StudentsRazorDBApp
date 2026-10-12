using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsApp.DTO;
using StudentsApp.Models;
using StudentsApp.Services;

namespace StudentsApp.Pages.Students
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
