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
    public class Users_TypesController : Controller
    {
        private readonly IUsers_TypesRepository users_TypesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<Users_TypesController> _logger;
	private IUtilityHelper utilityHelper;
        public Users_TypesController(IUsers_TypesRepository users_TypesRepository, IMapper mapper, ILogger<Users_TypesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.users_TypesRepository = users_TypesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllUsers_Types")]
        public async Task<IActionResult> GetAllUsers_Types()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var users_typesList = await users_TypesRepository.GetAllUsers_Types();
                _logger.LogInformation($"database call done successfully with {users_typesList?.Count()}");
                return Ok(users_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetUsers_TypesById")]
        public async Task<IActionResult> GetUsers_TypesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var users_typesList = await users_TypesRepository.GetUsers_TypesById(Id);
                _logger.LogInformation($"database call done successfully with {users_typesList?.Id}");
                if (users_typesList == null)
                {
                    return NotFound();
                }
                return Ok(users_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddUsers_Types")]
        public async Task<IActionResult> CreateUsers_Types(Users_Types Users_TypesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var users_Types = new Users_Types()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var users_typesDTO = await users_TypesRepository.CreateUsers_Types(Users_TypesDetails);
                _logger.LogInformation($"database call done successfully with {users_typesDTO?.Id}");
                return Ok(users_typesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateUsers_Types([FromRoute] int Id, [FromBody] Users_Types updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Users_Types users_Types = await users_TypesRepository.UpdateUsers_Types(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {users_Types}");
                if (users_Types == null) 
                { 
                    return NotFound(); 
                }
                return Ok(users_Types);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateUsers_TypesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateUsers_TypesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Users_Types users_Types = await users_TypesRepository.UpdateUsers_TypesStatus(Id);
                _logger.LogInformation($"database call done successfully with {users_Types}");
                if (users_Types == null) 
                { 
                    return NotFound(); 
                }
                return Ok(users_Types);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteUsers_Types(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await users_TypesRepository.DeleteUsers_Types(Id);
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

	  [HttpGet("~/GetUsers_TypesLookup")]
        public async Task<IActionResult> GetUsers_TypesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var users_typesList = await users_TypesRepository.GetUsers_TypesLookup();
                _logger.LogInformation($"database call done successfully with {users_typesList?.Count()}");
                return Ok(users_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchUsers_Types")]
        public async Task<IActionResult> SearchUsers_Types(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var users_typesList = users_TypesRepository.SearchUsers_Types(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {users_typesList?.Count()}");
                return Ok(users_typesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
