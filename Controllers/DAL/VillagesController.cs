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
    public class VillagesController : Controller
    {
        private readonly IVillagesRepository villagesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<VillagesController> _logger;
	private IUtilityHelper utilityHelper;
        public VillagesController(IVillagesRepository villagesRepository, IMapper mapper, ILogger<VillagesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.villagesRepository = villagesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllVillages")]
        public async Task<IActionResult> GetAllVillages()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var villagesList = await villagesRepository.GetAllVillages();
                _logger.LogInformation($"database call done successfully with {villagesList?.Count()}");
                return Ok(villagesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetVillagesById")]
        public async Task<IActionResult> GetVillagesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var villagesList = await villagesRepository.GetVillagesById(Id);
                _logger.LogInformation($"database call done successfully with {villagesList?.Id}");
                if (villagesList == null)
                {
                    return NotFound();
                }
                return Ok(villagesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddVillages")]
        public async Task<IActionResult> CreateVillages(Villages VillagesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var villages = new Villages()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var villagesDTO = await villagesRepository.CreateVillages(VillagesDetails);
                _logger.LogInformation($"database call done successfully with {villagesDTO?.Id}");
                return Ok(villagesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateVillages([FromRoute] int Id, [FromBody] Villages updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Villages villages = await villagesRepository.UpdateVillages(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {villages}");
                if (villages == null) 
                { 
                    return NotFound(); 
                }
                return Ok(villages);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateVillagesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateVillagesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Villages villages = await villagesRepository.UpdateVillagesStatus(Id);
                _logger.LogInformation($"database call done successfully with {villages}");
                if (villages == null) 
                { 
                    return NotFound(); 
                }
                return Ok(villages);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteVillages(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await villagesRepository.DeleteVillages(Id);
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

	  [HttpGet("~/GetVillagesLookup")]
        public async Task<IActionResult> GetVillagesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var villagesList = await villagesRepository.GetVillagesLookup();
                _logger.LogInformation($"database call done successfully with {villagesList?.Count()}");
                return Ok(villagesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchVillages")]
        public async Task<IActionResult> SearchVillages(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var villagesList = villagesRepository.SearchVillages(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {villagesList?.Count()}");
                return Ok(villagesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
