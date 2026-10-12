using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsAppSQL9Dev.Models;
using StudentsAppSQL9Dev.Services;

namespace StudentsAppSQL9Dev.Pages.Students
{
    public class DeleteModel : PageModel
    {
        public List<Error> ErrorArray { get; set; } = [];

        private readonly IStudentService _studentService;

        public DeleteModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public IActionResult OnPost(int id)
        {
            try
            {
                _studentService.DeleteStudent(id);
                TempData["StudentName"] = $"Student with id {id} was successfully deleted.";

                return RedirectToPage("/Students/Success");               
            }
            catch (Exception ex)
            {
                // Handle exceptions
                ErrorArray.Add(new Error { Message = ex.Message });
                return Page();
            }
        }
    }
}
