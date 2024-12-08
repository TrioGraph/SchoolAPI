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
    public class UsersController : Controller
    {
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UsersController> _logger;
	private IUtilityHelper utilityHelper;
        public UsersController(IUsersRepository usersRepository, IMapper mapper, ILogger<UsersController> logger,
	IUtilityHelper utilityHelper)
        {
            this.usersRepository = usersRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var usersList = await usersRepository.GetAllUsers();
                _logger.LogInformation($"database call done successfully with {usersList?.Count()}");
                return Ok(usersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetUsersById")]
        public async Task<IActionResult> GetUsersById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var usersList = await usersRepository.GetUsersById(Id);
                _logger.LogInformation($"database call done successfully with {usersList?.Id}");
                if (usersList == null)
                {
                    return NotFound();
                }
                return Ok(usersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddUsers")]
        public async Task<IActionResult> CreateUsers(Users UsersDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var users = new Users()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var usersDTO = await usersRepository.CreateUsers(UsersDetails);
                _logger.LogInformation($"database call done successfully with {usersDTO?.Id}");
                return Ok(usersDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateUsers([FromRoute] int Id, [FromBody] Users updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Users users = await usersRepository.UpdateUsers(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {users}");
                if (users == null) 
                { 
                    return NotFound(); 
                }
                return Ok(users);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateUsersStatus/{Id:int}")]
        public async Task<IActionResult> UpdateUsersStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Users users = await usersRepository.UpdateUsersStatus(Id);
                _logger.LogInformation($"database call done successfully with {users}");
                if (users == null) 
                { 
                    return NotFound(); 
                }
                return Ok(users);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteUsers(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await usersRepository.DeleteUsers(Id);
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

	  [HttpGet("~/GetUsersLookup")]
        public async Task<IActionResult> GetUsersLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var usersList = await usersRepository.GetUsersLookup();
                _logger.LogInformation($"database call done successfully with {usersList?.Count()}");
                return Ok(usersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchUsers")]
        public async Task<IActionResult> SearchUsers(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var usersList = usersRepository.SearchUsers(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {usersList?.Count()}");
                return Ok(usersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
