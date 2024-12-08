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
    public class PrivilegesController : Controller
    {
        private readonly IPrivilegesRepository privilegesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PrivilegesController> _logger;
	private IUtilityHelper utilityHelper;
        public PrivilegesController(IPrivilegesRepository privilegesRepository, IMapper mapper, ILogger<PrivilegesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.privilegesRepository = privilegesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllPrivileges")]
        public async Task<IActionResult> GetAllPrivileges()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var privilegesList = await privilegesRepository.GetAllPrivileges();
                _logger.LogInformation($"database call done successfully with {privilegesList?.Count()}");
                return Ok(privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetPrivilegesById")]
        public async Task<IActionResult> GetPrivilegesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var privilegesList = await privilegesRepository.GetPrivilegesById(Id);
                _logger.LogInformation($"database call done successfully with {privilegesList?.Id}");
                if (privilegesList == null)
                {
                    return NotFound();
                }
                return Ok(privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddPrivileges")]
        public async Task<IActionResult> CreatePrivileges(Privileges PrivilegesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var privileges = new Privileges()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var privilegesDTO = await privilegesRepository.CreatePrivileges(PrivilegesDetails);
                _logger.LogInformation($"database call done successfully with {privilegesDTO?.Id}");
                return Ok(privilegesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdatePrivileges([FromRoute] int Id, [FromBody] Privileges updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Privileges privileges = await privilegesRepository.UpdatePrivileges(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {privileges}");
                if (privileges == null) 
                { 
                    return NotFound(); 
                }
                return Ok(privileges);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdatePrivilegesStatus/{Id:int}")]
        public async Task<IActionResult> UpdatePrivilegesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Privileges privileges = await privilegesRepository.UpdatePrivilegesStatus(Id);
                _logger.LogInformation($"database call done successfully with {privileges}");
                if (privileges == null) 
                { 
                    return NotFound(); 
                }
                return Ok(privileges);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeletePrivileges(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await privilegesRepository.DeletePrivileges(Id);
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

	  [HttpGet("~/GetPrivilegesLookup")]
        public async Task<IActionResult> GetPrivilegesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var privilegesList = await privilegesRepository.GetPrivilegesLookup();
                _logger.LogInformation($"database call done successfully with {privilegesList?.Count()}");
                return Ok(privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchPrivileges")]
        public async Task<IActionResult> SearchPrivileges(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var privilegesList = privilegesRepository.SearchPrivileges(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {privilegesList?.Count()}");
                return Ok(privilegesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
