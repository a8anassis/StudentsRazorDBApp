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
        string sql = "DELETE FROM Students WHERE Id = @id";

        using SqlConnection connection = _db.GetConnection();
        connection.Open();

        using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@id", id);
        command.ExecuteNonQuery();
    }

    public List<Student> GetAll()
    {
        string sql = "SELECT * FROM Students";
        List<Student> students = [];

        using SqlConnection connection = _db.GetConnection();
        connection.Open();

        using SqlCommand command = new(sql, connection);
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Student student = new()
            {
                Id = (int)reader["Id"],
                Firstname = reader["Firstname"] as string ?? "",
                Lastname = reader["Lastname"] as string ?? ""
            };
            students.Add(student);
        }
        return students;
    }

    public Student? GetById(int id)
    {
        Student? studentToReturn = null;
        string sql = "SELECT * FROM Students WHERE Id = @id";

        using SqlConnection connection = _db.GetConnection();
        connection.Open();

        using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using SqlDataReader reader = command.ExecuteReader();

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
        string sql = "UPDATE Students SET Firstname = @firstname, Lastname = @lastname WHERE Id = @id";

        using SqlConnection connection = _db.GetConnection();
        connection.Open();

        using SqlCommand command = new(sql, connection);
        command.Parameters.AddWithValue("@firstname", student.Firstname);
        command.Parameters.AddWithValue("@lastname", student.Lastname);
        command.Parameters.AddWithValue("@id", student.Id);

        command.ExecuteNonQuery();
    }
}
