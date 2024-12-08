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
    public class MandalsController : Controller
    {
        private readonly IMandalsRepository mandalsRepository;
        private readonly IMapper mapper;
        private readonly ILogger<MandalsController> _logger;
	private IUtilityHelper utilityHelper;
        public MandalsController(IMandalsRepository mandalsRepository, IMapper mapper, ILogger<MandalsController> logger,
	IUtilityHelper utilityHelper)
        {
            this.mandalsRepository = mandalsRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllMandals")]
        public async Task<IActionResult> GetAllMandals()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var mandalsList = await mandalsRepository.GetAllMandals();
                _logger.LogInformation($"database call done successfully with {mandalsList?.Count()}");
                return Ok(mandalsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetMandalsById")]
        public async Task<IActionResult> GetMandalsById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var mandalsList = await mandalsRepository.GetMandalsById(Id);
                _logger.LogInformation($"database call done successfully with {mandalsList?.Id}");
                if (mandalsList == null)
                {
                    return NotFound();
                }
                return Ok(mandalsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddMandals")]
        public async Task<IActionResult> CreateMandals(Mandals MandalsDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var mandals = new Mandals()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var mandalsDTO = await mandalsRepository.CreateMandals(MandalsDetails);
                _logger.LogInformation($"database call done successfully with {mandalsDTO?.Id}");
                return Ok(mandalsDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateMandals([FromRoute] int Id, [FromBody] Mandals updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Mandals mandals = await mandalsRepository.UpdateMandals(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {mandals}");
                if (mandals == null) 
                { 
                    return NotFound(); 
                }
                return Ok(mandals);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateMandalsStatus/{Id:int}")]
        public async Task<IActionResult> UpdateMandalsStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Mandals mandals = await mandalsRepository.UpdateMandalsStatus(Id);
                _logger.LogInformation($"database call done successfully with {mandals}");
                if (mandals == null) 
                { 
                    return NotFound(); 
                }
                return Ok(mandals);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteMandals(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await mandalsRepository.DeleteMandals(Id);
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

	  [HttpGet("~/GetMandalsLookup")]
        public async Task<IActionResult> GetMandalsLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var mandalsList = await mandalsRepository.GetMandalsLookup();
                _logger.LogInformation($"database call done successfully with {mandalsList?.Count()}");
                return Ok(mandalsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchMandals")]
        public async Task<IActionResult> SearchMandals(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var mandalsList = mandalsRepository.SearchMandals(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {mandalsList?.Count()}");
                return Ok(mandalsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
