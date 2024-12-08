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
    public class DistrictsController : Controller
    {
        private readonly IDistrictsRepository districtsRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DistrictsController> _logger;
	private IUtilityHelper utilityHelper;
        public DistrictsController(IDistrictsRepository districtsRepository, IMapper mapper, ILogger<DistrictsController> logger,
	IUtilityHelper utilityHelper)
        {
            this.districtsRepository = districtsRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllDistricts")]
        public async Task<IActionResult> GetAllDistricts()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var districtsList = await districtsRepository.GetAllDistricts();
                _logger.LogInformation($"database call done successfully with {districtsList?.Count()}");
                return Ok(districtsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetDistrictsById")]
        public async Task<IActionResult> GetDistrictsById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var districtsList = await districtsRepository.GetDistrictsById(Id);
                _logger.LogInformation($"database call done successfully with {districtsList?.Id}");
                if (districtsList == null)
                {
                    return NotFound();
                }
                return Ok(districtsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddDistricts")]
        public async Task<IActionResult> CreateDistricts(Districts DistrictsDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var districts = new Districts()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var districtsDTO = await districtsRepository.CreateDistricts(DistrictsDetails);
                _logger.LogInformation($"database call done successfully with {districtsDTO?.Id}");
                return Ok(districtsDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateDistricts([FromRoute] int Id, [FromBody] Districts updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Districts districts = await districtsRepository.UpdateDistricts(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {districts}");
                if (districts == null) 
                { 
                    return NotFound(); 
                }
                return Ok(districts);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateDistrictsStatus/{Id:int}")]
        public async Task<IActionResult> UpdateDistrictsStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Districts districts = await districtsRepository.UpdateDistrictsStatus(Id);
                _logger.LogInformation($"database call done successfully with {districts}");
                if (districts == null) 
                { 
                    return NotFound(); 
                }
                return Ok(districts);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteDistricts(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await districtsRepository.DeleteDistricts(Id);
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

	  [HttpGet("~/GetDistrictsLookup")]
        public async Task<IActionResult> GetDistrictsLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var districtsList = await districtsRepository.GetDistrictsLookup();
                _logger.LogInformation($"database call done successfully with {districtsList?.Count()}");
                return Ok(districtsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchDistricts")]
        public async Task<IActionResult> SearchDistricts(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var districtsList = districtsRepository.SearchDistricts(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {districtsList?.Count()}");
                return Ok(districtsList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
