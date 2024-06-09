using APPLICATION_WEB_LAB_INV.Models;
using INV_CASSANDRA.Connection;
using INV_CASSANDRA.IReposity;

namespace INV_CASSANDRA.Reposity
{
    public class StudentReposity : IStudent
    {
        readonly ApplicationConnection _Service;
        public StudentReposity(ApplicationConnection Connection)
        {
            _Service = Connection;
        }


        public void InsertStudent(student student)
        {
            
                var _session = _Service.Connect();
                var query = "INSERT INTO student (id, first_name, last_name, age, address, phone, email) " +
                            "VALUES (?, ?, ?, ?, ?, ?, ?)";

                var preparedStatement = _session.Prepare(query);
                var boundStatement = preparedStatement.Bind(Guid.NewGuid(), student.FirstName, student.LastName, student.Age, student.Address, student.Phone, student.Email);

                _session.Execute(boundStatement);
                _Service.Close();
            
        }

        public List<student> GetAllStudents()
        {
           
                var _session = _Service.Connect();
                var query = "SELECT * FROM student";
                var rowSet = _session.Execute(query);
                var studentList = new List<student>();

                foreach (var row in rowSet)
                {
                    var student = new student
                    {
                        Id = row.GetValue<Guid>("id"),
                        FirstName = row.GetValue<string>("first_name"),
                        LastName = row.GetValue<string>("last_name"),
                        Age = row.GetValue<int>("age"),
                        Address = row.GetValue<string>("address"),
                        Phone = row.GetValue<string>("phone"),
                        Email = row.GetValue<string>("email")
                    };

                    studentList.Add(student);
                }
                _Service.Close();
                return studentList;
            
        }

        public student GetStudentById(Guid id)
        {
            
                var _session = _Service.Connect();
                var query = "SELECT * FROM student WHERE id = ?";
                var preparedStatement = _session.Prepare(query);
                var boundStatement = preparedStatement.Bind(id);
                var row = _session.Execute(boundStatement).FirstOrDefault();

                if (row != null)
                {
                    var student = new student
                    {
                        Id = row.GetValue<Guid>("id"),
                        FirstName = row.GetValue<string>("first_name"),
                        LastName = row.GetValue<string>("last_name"),
                        Age = row.GetValue<int>("age"),
                        Address = row.GetValue<string>("address"),
                        Phone = row.GetValue<string>("phone"),
                        Email = row.GetValue<string>("email")
                    };
                    _Service.Close();
                    return student;
                }

                return null;
           
        }

        public void UpdateStudent(student student)
        {
            
                var _session = _Service.Connect();
                var query = "UPDATE student SET first_name = ?, last_name = ?, age = ?, address = ?, phone = ?, email = ? " +
                            "WHERE id = ?";

                var preparedStatement = _session.Prepare(query);
                var boundStatement = preparedStatement.Bind(student.FirstName, student.LastName, student.Age, student.Address, student.Phone, student.Email, student.Id);

                _session.Execute(boundStatement);
                _Service.Close();
           
        }

  
        public void DeleteStudent(Guid id)
        {            
                var _session = _Service.Connect();
                var query = "DELETE FROM student WHERE id = ?";
                var preparedStatement = _session.Prepare(query);
                var boundStatement = preparedStatement.Bind(id);

                _session.Execute(boundStatement);
                _Service.Close();   
        }

      
    }

     
}
