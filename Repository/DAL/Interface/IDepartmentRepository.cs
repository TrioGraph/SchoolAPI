using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllDepartment();
        
        Task<IEnumerable<object>> GetDepartmentLookup();
		
        Task<Department> GetDepartmentById(int id);

        Task<Department> CreateDepartment(Department appsPermissions);

        Task<Department> UpdateDepartment(int Id, Department role);
    
        Task<Department> UpdateDepartmentStatus(int Id);

        Task<IEnumerable<Department>> DeleteDepartment(int roles);
       
        Dictionary<string, object> SearchDepartment(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}