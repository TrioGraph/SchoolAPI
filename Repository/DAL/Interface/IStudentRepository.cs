using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudent();
        
        Task<IEnumerable<object>> GetStudentLookup();
		
        Task<Student> GetStudentById(int id);

        Task<Student> CreateStudent(Student appsPermissions);

        Task<Student> UpdateStudent(int Id, Student role);
    
        Task<Student> UpdateStudentStatus(int Id);

        Task<IEnumerable<Student>> DeleteStudent(int roles);
       
        Dictionary<string, object> SearchStudent(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}