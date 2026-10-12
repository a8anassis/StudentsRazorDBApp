using StudentsApp.DTO;

namespace StudentsApp.Services
{
    public interface IStudentService
    {
        StudentReadOnlyDTO? InsertStudent(StudentInsertDTO studentInsertDTO);
        void UpdateStudent(StudentUpdateDTO studentUpdateDTO);
        void DeleteStudent(int id);
        StudentReadOnlyDTO? GetStudent(int id);
        List<StudentReadOnlyDTO> GetAllStudents();
    }
}
