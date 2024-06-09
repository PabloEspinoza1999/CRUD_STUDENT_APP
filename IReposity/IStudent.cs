using APPLICATION_WEB_LAB_INV.Models;

namespace INV_CASSANDRA.IReposity
{
    public interface IStudent
    {
        List<student> GetAllStudents();
        student GetStudentById(Guid id);
        void UpdateStudent(student student);
        void DeleteStudent(Guid id);
        void InsertStudent(student student);

    }
}
