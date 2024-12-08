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
    public class TeacherController : Controller
    {
        private readonly ITeacherRepository teacherRepository;
        private readonly IMapper mapper;
        private readonly ILogger<TeacherController> _logger;
	private IUtilityHelper utilityHelper;
        public TeacherController(ITeacherRepository teacherRepository, IMapper mapper, ILogger<TeacherController> logger,
	IUtilityHelper utilityHelper)
        {
            this.teacherRepository = teacherRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllTeacher")]
        public async Task<IActionResult> GetAllTeacher()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var teacherList = await teacherRepository.GetAllTeacher();
                _logger.LogInformation($"database call done successfully with {teacherList?.Count()}");
                return Ok(teacherList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetTeacherById")]
        public async Task<IActionResult> GetTeacherById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var teacherList = await teacherRepository.GetTeacherById(Id);
                _logger.LogInformation($"database call done successfully with {teacherList?.Id}");
                if (teacherList == null)
                {
                    return NotFound();
                }
                return Ok(teacherList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddTeacher")]
        public async Task<IActionResult> CreateTeacher(Teacher TeacherDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var teacher = new Teacher()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var teacherDTO = await teacherRepository.CreateTeacher(TeacherDetails);
                _logger.LogInformation($"database call done successfully with {teacherDTO?.Id}");
                return Ok(teacherDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateTeacher([FromRoute] int Id, [FromBody] Teacher updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Teacher teacher = await teacherRepository.UpdateTeacher(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {teacher}");
                if (teacher == null) 
                { 
                    return NotFound(); 
                }
                return Ok(teacher);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateTeacherStatus/{Id:int}")]
        public async Task<IActionResult> UpdateTeacherStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Teacher teacher = await teacherRepository.UpdateTeacherStatus(Id);
                _logger.LogInformation($"database call done successfully with {teacher}");
                if (teacher == null) 
                { 
                    return NotFound(); 
                }
                return Ok(teacher);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteTeacher(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await teacherRepository.DeleteTeacher(Id);
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

	  [HttpGet("~/GetTeacherLookup")]
        public async Task<IActionResult> GetTeacherLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var teacherList = await teacherRepository.GetTeacherLookup();
                _logger.LogInformation($"database call done successfully with {teacherList?.Count()}");
                return Ok(teacherList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchTeacher")]
        public async Task<IActionResult> SearchTeacher(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var teacherList = teacherRepository.SearchTeacher(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {teacherList?.Count()}");
                return Ok(teacherList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
