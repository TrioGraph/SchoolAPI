using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IUsersRepository
    {
        Task<List<Users>> GetAllUsers();
        
        Task<IEnumerable<object>> GetUsersLookup();
		
        Task<Users> GetUsersById(int id);

        Task<Users> CreateUsers(Users appsPermissions);

        Task<Users> UpdateUsers(int Id, Users role);
    
        Task<Users> UpdateUsersStatus(int Id);

        Task<IEnumerable<Users>> DeleteUsers(int roles);
       
        Dictionary<string, object> SearchUsers(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}