using Microsoft.AspNetCore.Mvc.RazorPages;

namespace StudentsAppSQL9Dev.Pages.Students
{
    public class SuccessModel : PageModel
    {
        public string? StudentName { get; set; }

        public void OnGet()
        {
            StudentName = TempData["StudentName"] as string;   
        }
    }
}
