using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IUsers_TypesRepository
    {
        Task<List<Users_Types>> GetAllUsers_Types();
        
        Task<IEnumerable<object>> GetUsers_TypesLookup();
		
        Task<Users_Types> GetUsers_TypesById(int id);

        Task<Users_Types> CreateUsers_Types(Users_Types appsPermissions);

        Task<Users_Types> UpdateUsers_Types(int Id, Users_Types role);
    
        Task<Users_Types> UpdateUsers_TypesStatus(int Id);

        Task<IEnumerable<Users_Types>> DeleteUsers_Types(int roles);
       
        Dictionary<string, object> SearchUsers_Types(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}