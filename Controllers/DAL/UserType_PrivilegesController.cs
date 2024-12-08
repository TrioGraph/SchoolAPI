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
    public class UserType_PrivilegesController : Controller
    {
        private readonly IUserType_PrivilegesRepository userType_PrivilegesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UserType_PrivilegesController> _logger;
	private IUtilityHelper utilityHelper;
        public UserType_PrivilegesController(IUserType_PrivilegesRepository userType_PrivilegesRepository, IMapper mapper, ILogger<UserType_PrivilegesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.userType_PrivilegesRepository = userType_PrivilegesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllUserType_Privileges")]
        public async Task<IActionResult> GetAllUserType_Privileges()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var usertype_privilegesList = await userType_PrivilegesRepository.GetAllUserType_Privileges();
                _logger.LogInformation($"database call done successfully with {usertype_privilegesList?.Count()}");
                return Ok(usertype_privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetUserType_PrivilegesById")]
        public async Task<IActionResult> GetUserType_PrivilegesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var usertype_privilegesList = await userType_PrivilegesRepository.GetUserType_PrivilegesById(Id);
                _logger.LogInformation($"database call done successfully with {usertype_privilegesList?.Id}");
                if (usertype_privilegesList == null)
                {
                    return NotFound();
                }
                return Ok(usertype_privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddUserType_Privileges")]
        public async Task<IActionResult> CreateUserType_Privileges(UserType_Privileges UserType_PrivilegesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var userType_Privileges = new UserType_Privileges()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var usertype_privilegesDTO = await userType_PrivilegesRepository.CreateUserType_Privileges(UserType_PrivilegesDetails);
                _logger.LogInformation($"database call done successfully with {usertype_privilegesDTO?.Id}");
                return Ok(usertype_privilegesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateUserType_Privileges([FromRoute] int Id, [FromBody] UserType_Privileges updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                UserType_Privileges userType_Privileges = await userType_PrivilegesRepository.UpdateUserType_Privileges(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {userType_Privileges}");
                if (userType_Privileges == null) 
                { 
                    return NotFound(); 
                }
                return Ok(userType_Privileges);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateUserType_PrivilegesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateUserType_PrivilegesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                UserType_Privileges userType_Privileges = await userType_PrivilegesRepository.UpdateUserType_PrivilegesStatus(Id);
                _logger.LogInformation($"database call done successfully with {userType_Privileges}");
                if (userType_Privileges == null) 
                { 
                    return NotFound(); 
                }
                return Ok(userType_Privileges);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteUserType_Privileges(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await userType_PrivilegesRepository.DeleteUserType_Privileges(Id);
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

	  [HttpGet("~/GetUserType_PrivilegesLookup")]
        public async Task<IActionResult> GetUserType_PrivilegesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var usertype_privilegesList = await userType_PrivilegesRepository.GetUserType_PrivilegesLookup();
                _logger.LogInformation($"database call done successfully with {usertype_privilegesList?.Count()}");
                return Ok(usertype_privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchUserType_Privileges")]
        public async Task<IActionResult> SearchUserType_Privileges(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var usertype_privilegesList = userType_PrivilegesRepository.SearchUserType_Privileges(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {usertype_privilegesList?.Count()}");
                return Ok(usertype_privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
