using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IPrivilegesRepository
    {
        Task<List<Privileges>> GetAllPrivileges();
        
        Task<IEnumerable<object>> GetPrivilegesLookup();
		
        Task<Privileges> GetPrivilegesById(int id);

        Task<Privileges> CreatePrivileges(Privileges appsPermissions);

        Task<Privileges> UpdatePrivileges(int Id, Privileges role);
    
        Task<Privileges> UpdatePrivilegesStatus(int Id);

        Task<IEnumerable<Privileges>> DeletePrivileges(int roles);
       
        Dictionary<string, object> SearchPrivileges(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}