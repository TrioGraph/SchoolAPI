using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IVillagesRepository
    {
        Task<List<Villages>> GetAllVillages();
        
        Task<IEnumerable<object>> GetVillagesLookup();
		
        Task<Villages> GetVillagesById(int id);

        Task<Villages> CreateVillages(Villages appsPermissions);

        Task<Villages> UpdateVillages(int Id, Villages role);
    
        Task<Villages> UpdateVillagesStatus(int Id);

        Task<IEnumerable<Villages>> DeleteVillages(int roles);
       
        Dictionary<string, object> SearchVillages(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}