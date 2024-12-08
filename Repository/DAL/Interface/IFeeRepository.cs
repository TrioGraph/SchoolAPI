using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IFeeRepository
    {
        Task<List<Fee>> GetAllFee();
        
        Task<IEnumerable<object>> GetFeeLookup();
		
        Task<Fee> GetFeeById(int id);

        Task<Fee> CreateFee(Fee appsPermissions);

        Task<Fee> UpdateFee(int Id, Fee role);
    
        Task<Fee> UpdateFeeStatus(int Id);

        Task<IEnumerable<Fee>> DeleteFee(int roles);
       
        Dictionary<string, object> SearchFee(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}