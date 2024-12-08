using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IDistrictsRepository
    {
        Task<List<Districts>> GetAllDistricts();
        
        Task<IEnumerable<object>> GetDistrictsLookup();
		
        Task<Districts> GetDistrictsById(int id);

        Task<Districts> CreateDistricts(Districts appsPermissions);

        Task<Districts> UpdateDistricts(int Id, Districts role);
    
        Task<Districts> UpdateDistrictsStatus(int Id);

        Task<IEnumerable<Districts>> DeleteDistricts(int roles);
       
        Dictionary<string, object> SearchDistricts(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}