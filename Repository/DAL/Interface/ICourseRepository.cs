using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface ICourseRepository
    {
        Task<List<Course>> GetAllCourse();
        
        Task<IEnumerable<object>> GetCourseLookup();
		
        Task<Course> GetCourseById(int id);

        Task<Course> CreateCourse(Course appsPermissions);

        Task<Course> UpdateCourse(int Id, Course role);
    
        Task<Course> UpdateCourseStatus(int Id);

        Task<IEnumerable<Course>> DeleteCourse(int roles);
       
        Dictionary<string, object> SearchCourse(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "");

    }
}