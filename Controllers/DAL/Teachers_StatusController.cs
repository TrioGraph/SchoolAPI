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
    public class Teachers_StatusController : Controller
    {
        private readonly ITeachers_StatusRepository teachers_StatusRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Teachers_StatusController> _logger;
	private IUtilityHelper utilityHelper;
        public Teachers_StatusController(ITeachers_StatusRepository teachers_StatusRepository, IMapper mapper, ILogger<Teachers_StatusController> logger,
	IUtilityHelper utilityHelper)
        {
            this.teachers_StatusRepository = teachers_StatusRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllTeachers_Status")]
        public async Task<IActionResult> GetAllTeachers_Status()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var teachers_statusList = await teachers_StatusRepository.GetAllTeachers_Status();
                _logger.LogInformation($"database call done successfully with {teachers_statusList?.Count()}");
                return Ok(teachers_statusList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetTeachers_StatusById")]
        public async Task<IActionResult> GetTeachers_StatusById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var teachers_statusList = await teachers_StatusRepository.GetTeachers_StatusById(Id);
                _logger.LogInformation($"database call done successfully with {teachers_statusList?.Id}");
                if (teachers_statusList == null)
                {
                    return NotFound();
                }
                return Ok(teachers_statusList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddTeachers_Status")]
        public async Task<IActionResult> CreateTeachers_Status(Teachers_Status Teachers_StatusDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var teachers_Status = new Teachers_Status()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var teachers_statusDTO = await teachers_StatusRepository.CreateTeachers_Status(Teachers_StatusDetails);
                _logger.LogInformation($"database call done successfully with {teachers_statusDTO?.Id}");
                return Ok(teachers_statusDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateTeachers_Status([FromRoute] int Id, [FromBody] Teachers_Status updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Teachers_Status teachers_Status = await teachers_StatusRepository.UpdateTeachers_Status(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {teachers_Status}");
                if (teachers_Status == null) 
                { 
                    return NotFound(); 
                }
                return Ok(teachers_Status);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateTeachers_StatusStatus/{Id:int}")]
        public async Task<IActionResult> UpdateTeachers_StatusStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Teachers_Status teachers_Status = await teachers_StatusRepository.UpdateTeachers_StatusStatus(Id);
                _logger.LogInformation($"database call done successfully with {teachers_Status}");
                if (teachers_Status == null) 
                { 
                    return NotFound(); 
                }
                return Ok(teachers_Status);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteTeachers_Status(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await teachers_StatusRepository.DeleteTeachers_Status(Id);
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

	  [HttpGet("~/GetTeachers_StatusLookup")]
        public async Task<IActionResult> GetTeachers_StatusLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var teachers_statusList = await teachers_StatusRepository.GetTeachers_StatusLookup();
                _logger.LogInformation($"database call done successfully with {teachers_statusList?.Count()}");
                return Ok(teachers_statusList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchTeachers_Status")]
        public async Task<IActionResult> SearchTeachers_Status(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var teachers_statusList = teachers_StatusRepository.SearchTeachers_Status(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {teachers_statusList?.Count()}");
                return Ok(teachers_statusList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
