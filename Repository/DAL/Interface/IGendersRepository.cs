using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IGendersRepository
    {
        Task<List<Genders>> GetAllGenders();
        
        Task<IEnumerable<object>> GetGendersLookup();
		
        Task<Genders> GetGendersById(int id);

        Task<Genders> CreateGenders(Genders appsPermissions);

        Task<Genders> UpdateGenders(int Id, Genders role);
    
        Task<Genders> UpdateGendersStatus(int Id);

        Task<IEnumerable<Genders>> DeleteGenders(int roles);
       
        Dictionary<string, object> SearchGenders(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}