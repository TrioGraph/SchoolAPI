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
    public class StaffController : Controller
    {
        private readonly IStaffRepository staffRepository;
        private readonly IMapper mapper;
        private readonly ILogger<StaffController> _logger;
	private IUtilityHelper utilityHelper;
        public StaffController(IStaffRepository staffRepository, IMapper mapper, ILogger<StaffController> logger,
	IUtilityHelper utilityHelper)
        {
            this.staffRepository = staffRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllStaff")]
        public async Task<IActionResult> GetAllStaff()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var staffList = await staffRepository.GetAllStaff();
                _logger.LogInformation($"database call done successfully with {staffList?.Count()}");
                return Ok(staffList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetStaffById")]
        public async Task<IActionResult> GetStaffById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var staffList = await staffRepository.GetStaffById(Id);
                _logger.LogInformation($"database call done successfully with {staffList?.Id}");
                if (staffList == null)
                {
                    return NotFound();
                }
                return Ok(staffList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddStaff")]
        public async Task<IActionResult> CreateStaff(Staff StaffDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var staff = new Staff()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var staffDTO = await staffRepository.CreateStaff(StaffDetails);
                _logger.LogInformation($"database call done successfully with {staffDTO?.Id}");
                return Ok(staffDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateStaff([FromRoute] int Id, [FromBody] Staff updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Staff staff = await staffRepository.UpdateStaff(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {staff}");
                if (staff == null) 
                { 
                    return NotFound(); 
                }
                return Ok(staff);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateStaffStatus/{Id:int}")]
        public async Task<IActionResult> UpdateStaffStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Staff staff = await staffRepository.UpdateStaffStatus(Id);
                _logger.LogInformation($"database call done successfully with {staff}");
                if (staff == null) 
                { 
                    return NotFound(); 
                }
                return Ok(staff);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteStaff(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await staffRepository.DeleteStaff(Id);
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

	  [HttpGet("~/GetStaffLookup")]
        public async Task<IActionResult> GetStaffLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var staffList = await staffRepository.GetStaffLookup();
                _logger.LogInformation($"database call done successfully with {staffList?.Count()}");
                return Ok(staffList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchStaff")]
        public async Task<IActionResult> SearchStaff(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var staffList = staffRepository.SearchStaff(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {staffList?.Count()}");
                return Ok(staffList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
