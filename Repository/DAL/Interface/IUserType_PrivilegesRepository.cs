using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IUserType_PrivilegesRepository
    {
        Task<List<UserType_Privileges>> GetAllUserType_Privileges();
        
        Task<IEnumerable<object>> GetUserType_PrivilegesLookup();
		
        Task<UserType_Privileges> GetUserType_PrivilegesById(int id);

        Task<UserType_Privileges> CreateUserType_Privileges(UserType_Privileges appsPermissions);

        Task<UserType_Privileges> UpdateUserType_Privileges(int Id, UserType_Privileges role);
    
        Task<UserType_Privileges> UpdateUserType_PrivilegesStatus(int Id);

        Task<IEnumerable<UserType_Privileges>> DeleteUserType_Privileges(int roles);
       
        Dictionary<string, object> SearchUserType_Privileges(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}