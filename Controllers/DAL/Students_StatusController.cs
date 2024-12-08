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
    public class Students_StatusController : Controller
    {
        private readonly IStudents_StatusRepository students_StatusRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Students_StatusController> _logger;
	private IUtilityHelper utilityHelper;
        public Students_StatusController(IStudents_StatusRepository students_StatusRepository, IMapper mapper, ILogger<Students_StatusController> logger,
	IUtilityHelper utilityHelper)
        {
            this.students_StatusRepository = students_StatusRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllStudents_Status")]
        public async Task<IActionResult> GetAllStudents_Status()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var students_statusList = await students_StatusRepository.GetAllStudents_Status();
                _logger.LogInformation($"database call done successfully with {students_statusList?.Count()}");
                return Ok(students_statusList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetStudents_StatusById")]
        public async Task<IActionResult> GetStudents_StatusById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var students_statusList = await students_StatusRepository.GetStudents_StatusById(Id);
                _logger.LogInformation($"database call done successfully with {students_statusList?.Id}");
                if (students_statusList == null)
                {
                    return NotFound();
                }
                return Ok(students_statusList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddStudents_Status")]
        public async Task<IActionResult> CreateStudents_Status(Students_Status Students_StatusDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var students_Status = new Students_Status()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var students_statusDTO = await students_StatusRepository.CreateStudents_Status(Students_StatusDetails);
                _logger.LogInformation($"database call done successfully with {students_statusDTO?.Id}");
                return Ok(students_statusDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateStudents_Status([FromRoute] int Id, [FromBody] Students_Status updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Students_Status students_Status = await students_StatusRepository.UpdateStudents_Status(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {students_Status}");
                if (students_Status == null) 
                { 
                    return NotFound(); 
                }
                return Ok(students_Status);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateStudents_StatusStatus/{Id:int}")]
        public async Task<IActionResult> UpdateStudents_StatusStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Students_Status students_Status = await students_StatusRepository.UpdateStudents_StatusStatus(Id);
                _logger.LogInformation($"database call done successfully with {students_Status}");
                if (students_Status == null) 
                { 
                    return NotFound(); 
                }
                return Ok(students_Status);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteStudents_Status(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await students_StatusRepository.DeleteStudents_Status(Id);
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

	  [HttpGet("~/GetStudents_StatusLookup")]
        public async Task<IActionResult> GetStudents_StatusLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var students_statusList = await students_StatusRepository.GetStudents_StatusLookup();
                _logger.LogInformation($"database call done successfully with {students_statusList?.Count()}");
                return Ok(students_statusList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchStudents_Status")]
        public async Task<IActionResult> SearchStudents_Status(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var students_statusList = students_StatusRepository.SearchStudents_Status(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {students_statusList?.Count()}");
                return Ok(students_statusList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
