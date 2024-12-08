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
    public class AttendenceController : Controller
    {
        private readonly IAttendenceRepository attendenceRepository;
        private readonly IMapper mapper;
        private readonly ILogger<AttendenceController> _logger;
	private IUtilityHelper utilityHelper;
        public AttendenceController(IAttendenceRepository attendenceRepository, IMapper mapper, ILogger<AttendenceController> logger,
	IUtilityHelper utilityHelper)
        {
            this.attendenceRepository = attendenceRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllAttendence")]
        public async Task<IActionResult> GetAllAttendence()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var attendenceList = await attendenceRepository.GetAllAttendence();
                _logger.LogInformation($"database call done successfully with {attendenceList?.Count()}");
                return Ok(attendenceList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetAttendenceById")]
        public async Task<IActionResult> GetAttendenceById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var attendenceList = await attendenceRepository.GetAttendenceById(Id);
                _logger.LogInformation($"database call done successfully with {attendenceList?.Id}");
                if (attendenceList == null)
                {
                    return NotFound();
                }
                return Ok(attendenceList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddAttendence")]
        public async Task<IActionResult> CreateAttendence(Attendence AttendenceDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var attendence = new Attendence()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var attendenceDTO = await attendenceRepository.CreateAttendence(AttendenceDetails);
                _logger.LogInformation($"database call done successfully with {attendenceDTO?.Id}");
                return Ok(attendenceDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateAttendence([FromRoute] int Id, [FromBody] Attendence updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Attendence attendence = await attendenceRepository.UpdateAttendence(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {attendence}");
                if (attendence == null) 
                { 
                    return NotFound(); 
                }
                return Ok(attendence);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateAttendenceStatus/{Id:int}")]
        public async Task<IActionResult> UpdateAttendenceStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Attendence attendence = await attendenceRepository.UpdateAttendenceStatus(Id);
                _logger.LogInformation($"database call done successfully with {attendence}");
                if (attendence == null) 
                { 
                    return NotFound(); 
                }
                return Ok(attendence);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteAttendence(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await attendenceRepository.DeleteAttendence(Id);
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

	  [HttpGet("~/GetAttendenceLookup")]
        public async Task<IActionResult> GetAttendenceLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var attendenceList = await attendenceRepository.GetAttendenceLookup();
                _logger.LogInformation($"database call done successfully with {attendenceList?.Count()}");
                return Ok(attendenceList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchAttendence")]
        public async Task<IActionResult> SearchAttendence(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var attendenceList = attendenceRepository.SearchAttendence(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {attendenceList?.Count()}");
                return Ok(attendenceList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
