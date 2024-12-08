using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface ITeachers_StatusRepository
    {
        Task<List<Teachers_Status>> GetAllTeachers_Status();
        
        Task<IEnumerable<object>> GetTeachers_StatusLookup();
		
        Task<Teachers_Status> GetTeachers_StatusById(int id);

        Task<Teachers_Status> CreateTeachers_Status(Teachers_Status appsPermissions);

        Task<Teachers_Status> UpdateTeachers_Status(int Id, Teachers_Status role);
    
        Task<Teachers_Status> UpdateTeachers_StatusStatus(int Id);

        Task<IEnumerable<Teachers_Status>> DeleteTeachers_Status(int roles);
       
        Dictionary<string, object> SearchTeachers_Status(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}