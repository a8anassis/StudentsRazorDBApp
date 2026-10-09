using StudentsApp.Models;

namespace StudentsApp.DAO;

public interface IStudentDAO
{
    Student? Insert(Student student);
    void Update(Student student);
    void Delete(int id);
    Student? GetById(int id);
    List<Student> GetAll();
}
