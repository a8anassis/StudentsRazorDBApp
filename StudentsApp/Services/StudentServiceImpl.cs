using AutoMapper;
using StudentsApp.DAO;
using StudentsApp.DTO;
using StudentsApp.Exceptions;
using StudentsApp.Models;
using System.Transactions;

namespace StudentsApp.Services;

public class StudentServiceImpl : IStudentService
{

    private readonly IStudentDAO _studentDAO;
    private readonly IMapper _mapper;
    private readonly ILogger<StudentServiceImpl> _logger;

    public StudentServiceImpl(IStudentDAO studentDAO, IMapper mapper, ILogger<StudentServiceImpl> logger)
    {
        _studentDAO = studentDAO;
        _mapper = mapper;
        _logger = logger;
    }



    public void DeleteStudent(int id)
    {
        try
        {
            using TransactionScope scope = new TransactionScope();

            if (_studentDAO.GetById(id) is null)
            {
                throw new StudentNotFoundException($"Student with id {id} not found.");
            }

            _studentDAO.Delete(id);
            _logger.LogInformation("Student with id {Id} deleted successfully", id);
            scope.Complete();
        }
        catch (StudentNotFoundException ex)
        {
            _logger.LogWarning("Delete failed to find student with id {Id}. {ErrorMessage}", id, ex.Message);
            throw;
        }
        catch (TransactionException ex)
        {
            _logger.LogError("Student delete failed transaction for {Id}. {ErrorMessage}", id, ex.Message);
            throw;
        }
    }

    public List<StudentReadOnlyDTO> GetAllStudents()
    {
        List<StudentReadOnlyDTO> studentReadOnlyDTOs = [];
        List<Student> students = [];
        StudentReadOnlyDTO studentReadOnlyDTO;

  
        students = _studentDAO.GetAll();
        foreach (var student in students)
        {
            studentReadOnlyDTO = _mapper.Map<StudentReadOnlyDTO>(student);
            studentReadOnlyDTOs.Add(studentReadOnlyDTO);
        }
        _logger.LogInformation("All students fetched successfully");
        return studentReadOnlyDTOs;
    }

    public StudentReadOnlyDTO? GetStudent(int id)
    {
        StudentReadOnlyDTO studentReadOnlyDTO = new();

        try
        {
            Student student = _studentDAO.GetById(id) 
                ?? throw new StudentNotFoundException($"Student with id {id} not found");

            studentReadOnlyDTO = _mapper.Map<StudentReadOnlyDTO>(student);
            _logger.LogInformation("Student with id {Id} was fetched.", id);
            return studentReadOnlyDTO;
        }
        catch (StudentNotFoundException ex)
        {
            _logger.LogWarning("Failed to find student with id {Id}. {ErrorMessage}", id, ex.Message);
            throw;
        }
    }

    public StudentReadOnlyDTO? InsertStudent(StudentInsertDTO studentInsertDTO)
    {
        StudentReadOnlyDTO studentReadOnlyDTO = new();

        try
        {
            using TransactionScope scope = new TransactionScope();
            Student student = _mapper.Map<Student>(studentInsertDTO);
            Student? insertedStudent = _studentDAO.Insert(student);
            studentReadOnlyDTO = _mapper.Map<StudentReadOnlyDTO>(insertedStudent);
            _logger.LogInformation("Student {Firstname} {Lastname} inserted successfully",
                studentReadOnlyDTO.Firstname, studentReadOnlyDTO.Lastname);            
            scope.Complete();
            return studentReadOnlyDTO;
        }
        catch (TransactionException ex)
        {
            _logger.LogError("Student insertion failed for {Firstname} {Lastname}. {ErrorMessage}",
                studentInsertDTO.Firstname, studentInsertDTO.Lastname, ex.Message);
            throw;
        }
    }

    public void UpdateStudent(StudentUpdateDTO studentUpdateDTO)
    {
        try
        {
            using TransactionScope scope = new TransactionScope();

            if (_studentDAO.GetById(studentUpdateDTO.Id) is null)
            {
                throw new StudentNotFoundException($"Student with id {studentUpdateDTO.Id} not found.");
            }

            Student student = _mapper.Map<Student>(studentUpdateDTO);
            _studentDAO.Update(student);
            _logger.LogInformation("Student {Firstname} {Lastname} updates successfully",
                studentUpdateDTO.Firstname, studentUpdateDTO.Lastname);
            scope.Complete();
        }
        catch (StudentNotFoundException ex)
        {
            _logger.LogWarning("Update failed to find student {Firstname} {Lastname}. {ErrorMessage}",
                studentUpdateDTO.Firstname, studentUpdateDTO.Lastname, ex.Message);
            throw;
        }
        catch (TransactionException ex)
        {
            _logger.LogError("Student update failed transaction for {Firstname} {Lastname}. {ErrorMessage}",
                studentUpdateDTO.Firstname, studentUpdateDTO.Lastname, ex.Message);
            throw;
        }
    }
}
