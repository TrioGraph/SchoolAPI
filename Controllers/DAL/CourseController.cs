using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Repositories;
using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class CourseController : Controller
    {
        private readonly ICourseRepository courseRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CourseController> _logger;
	private IUtilityHelper utilityHelper;
        public CourseController(ICourseRepository courseRepository, IMapper mapper, ILogger<CourseController> logger,
	IUtilityHelper utilityHelper)
        {
            this.courseRepository = courseRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllCourse")]
        public async Task<IActionResult> GetAllCourse()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var courseList = await courseRepository.GetAllCourse();
                _logger.LogInformation($"database call done successfully with {courseList?.Count()}");
                return Ok(courseList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetCourseById")]
        public async Task<IActionResult> GetCourseById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var courseList = await courseRepository.GetCourseById(Id);
                _logger.LogInformation($"database call done successfully with {courseList?.Id}");
                if (courseList == null)
                {
                    return NotFound();
                }
                return Ok(courseList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddCourse")]
        public async Task<IActionResult> CreateCourse(Course CourseDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var course = new Course()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var courseDTO = await courseRepository.CreateCourse(CourseDetails);
                _logger.LogInformation($"database call done successfully with {courseDTO?.Id}");
                return Ok(courseDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateCourse([FromRoute] int Id, [FromBody] Course updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Course course = await courseRepository.UpdateCourse(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {course}");
                if (course == null) 
                { 
                    return NotFound(); 
                }
                return Ok(course);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateCourseStatus/{Id:int}")]
        public async Task<IActionResult> UpdateCourseStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Course course = await courseRepository.UpdateCourseStatus(Id);
                _logger.LogInformation($"database call done successfully with {course}");
                if (course == null) 
                { 
                    return NotFound(); 
                }
                return Ok(course);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteCourse(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await courseRepository.DeleteCourse(Id);
                _logger.LogInformation($"database call done successfully with {deletedItem}");
                if (deletedItem == null)
                {
                    return NotFound();
                }
                return Ok(deletedItem);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpGet("~/GetCourseLookup")]
        public async Task<IActionResult> GetCourseLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var courseList = await courseRepository.GetCourseLookup();
                _logger.LogInformation($"database call done successfully with {courseList?.Count()}");
                return Ok(courseList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchCourse")]
        public async Task<IActionResult> SearchCourse(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
        bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "")
        {
            try
            {
                _logger.LogInformation($"Start");
                if (searchText == "null")
                {
                    searchText = "";
                }
		string userId = utilityHelper.GetUserFromRequest(Request);
                var courseList = courseRepository.SearchCourse(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {courseList?.Count()}");
                return Ok(courseList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
