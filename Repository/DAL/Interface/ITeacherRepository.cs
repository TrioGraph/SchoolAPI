using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface ITeacherRepository
    {
        Task<List<Teacher>> GetAllTeacher();
        
        Task<IEnumerable<object>> GetTeacherLookup();
		
        Task<Teacher> GetTeacherById(int id);

        Task<Teacher> CreateTeacher(Teacher appsPermissions);

        Task<Teacher> UpdateTeacher(int Id, Teacher role);
    
        Task<Teacher> UpdateTeacherStatus(int Id);

        Task<IEnumerable<Teacher>> DeleteTeacher(int roles);
       
        Dictionary<string, object> SearchTeacher(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}