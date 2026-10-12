using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudentsApp.DTO;
using StudentsApp.Models;
using StudentsApp.Services;

namespace StudentsApp.Pages.Students
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public StudentInsertDTO StudentInsertDTO { get; set; } = new();
        public List<Error> ErrorArray { get; set; } = [];
        
        private readonly IStudentService _studentService;

        public CreateModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public void OnGet()
        {
            // return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            try 
            {
                StudentReadOnlyDTO? studentReadOnlyDTO = _studentService.InsertStudent(StudentInsertDTO);
                
                TempData["StudentName"] = $"{studentReadOnlyDTO?.Firstname}, {studentReadOnlyDTO?.Lastname}" + " was successfully created.";

                // PRG pattern Post-Request-Get
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
