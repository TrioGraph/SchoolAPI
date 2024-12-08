using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IStaffRepository
    {
        Task<List<Staff>> GetAllStaff();
        
        Task<IEnumerable<object>> GetStaffLookup();
		
        Task<Staff> GetStaffById(int id);

        Task<Staff> CreateStaff(Staff appsPermissions);

        Task<Staff> UpdateStaff(int Id, Staff role);
    
        Task<Staff> UpdateStaffStatus(int Id);

        Task<IEnumerable<Staff>> DeleteStaff(int roles);
       
        Dictionary<string, object> SearchStaff(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}