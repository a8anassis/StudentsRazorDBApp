using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using StudentsApp.Core;
using StudentsApp.Models;

namespace StudentsApp.DAO;

public class StudentDAOImpl : IStudentDAO
{
    private readonly DBHelper _db;

    public StudentDAOImpl(DBHelper db)
    {
        _db = db;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public List<Student> GetAll()
    {
        throw new NotImplementedException();
    }

    public Student? GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Student? Insert(Student student)
    {
        Student? studentToReturn = null;

        string sql = "INSERT INTO Students (Firstname, Lastname) VALUES (@firstname, @lastname); " +
            "SELECT SCOPE_IDENTITY();";

        using SqlConnection connection = _db.GetConnection();
        connection.Open();

        using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@firstname", student.Firstname);
        command.Parameters.AddWithValue("@lastname", student.Lastname);

        var insertedObject = command.ExecuteScalar();
        int insertedId = 0;

        if (insertedObject != null)
        {
            if (!int.TryParse(insertedObject.ToString(), out insertedId))
            {
                throw new Exception("Error in inserted id");
           
            }
        }

        string sql2 = "SELECT * FROM Students WHERE Id = @studentid";
        using SqlCommand command2 = new(sql2, connection);
        command2.Parameters.AddWithValue("@studentid", insertedId);

        using SqlDataReader reader = command2.ExecuteReader();
        if (reader.Read()) 
        {
            studentToReturn = new Student()
            {
                Id = (int)reader["Id"],
                Firstname = reader["Firstname"] as string ?? "",
                Lastname = reader["Lastname"] as string ?? ""
            };
        }
        return studentToReturn;
    }

    public void Update(Student student)
    {
        throw new NotImplementedException();
    }
}
