using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IStudents_StatusRepository
    {
        Task<List<Students_Status>> GetAllStudents_Status();
        
        Task<IEnumerable<object>> GetStudents_StatusLookup();
		
        Task<Students_Status> GetStudents_StatusById(int id);

        Task<Students_Status> CreateStudents_Status(Students_Status appsPermissions);

        Task<Students_Status> UpdateStudents_Status(int Id, Students_Status role);
    
        Task<Students_Status> UpdateStudents_StatusStatus(int Id);

        Task<IEnumerable<Students_Status>> DeleteStudents_Status(int roles);
       
        Dictionary<string, object> SearchStudents_Status(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}