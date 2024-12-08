using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IAttendenceRepository
    {
        Task<List<Attendence>> GetAllAttendence();
        
        Task<IEnumerable<object>> GetAttendenceLookup();
		
        Task<Attendence> GetAttendenceById(int id);

        Task<Attendence> CreateAttendence(Attendence appsPermissions);

        Task<Attendence> UpdateAttendence(int Id, Attendence role);
    
        Task<Attendence> UpdateAttendenceStatus(int Id);

        Task<IEnumerable<Attendence>> DeleteAttendence(int roles);
       
        Dictionary<string, object> SearchAttendence(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}