using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IMandalsRepository
    {
        Task<List<Mandals>> GetAllMandals();
        
        Task<IEnumerable<object>> GetMandalsLookup();
		
        Task<Mandals> GetMandalsById(int id);

        Task<Mandals> CreateMandals(Mandals appsPermissions);

        Task<Mandals> UpdateMandals(int Id, Mandals role);
    
        Task<Mandals> UpdateMandalsStatus(int Id);

        Task<IEnumerable<Mandals>> DeleteMandals(int roles);
       
        Dictionary<string, object> SearchMandals(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}