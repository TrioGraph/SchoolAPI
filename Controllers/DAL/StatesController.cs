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
    public class StatesController : Controller
    {
        private readonly IStatesRepository statesRepository;
        private readonly IMapper mapper;
        private readonly ILogger<StatesController> _logger;
	private IUtilityHelper utilityHelper;
        public StatesController(IStatesRepository statesRepository, IMapper mapper, ILogger<StatesController> logger,
	IUtilityHelper utilityHelper)
        {
            this.statesRepository = statesRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllStates")]
        public async Task<IActionResult> GetAllStates()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var statesList = await statesRepository.GetAllStates();
                _logger.LogInformation($"database call done successfully with {statesList?.Count()}");
                return Ok(statesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetStatesById")]
        public async Task<IActionResult> GetStatesById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var statesList = await statesRepository.GetStatesById(Id);
                _logger.LogInformation($"database call done successfully with {statesList?.Id}");
                if (statesList == null)
                {
                    return NotFound();
                }
                return Ok(statesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddStates")]
        public async Task<IActionResult> CreateStates(States StatesDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var states = new States()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var statesDTO = await statesRepository.CreateStates(StatesDetails);
                _logger.LogInformation($"database call done successfully with {statesDTO?.Id}");
                return Ok(statesDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateStates([FromRoute] int Id, [FromBody] States updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                States states = await statesRepository.UpdateStates(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {states}");
                if (states == null) 
                { 
                    return NotFound(); 
                }
                return Ok(states);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateStatesStatus/{Id:int}")]
        public async Task<IActionResult> UpdateStatesStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                States states = await statesRepository.UpdateStatesStatus(Id);
                _logger.LogInformation($"database call done successfully with {states}");
                if (states == null) 
                { 
                    return NotFound(); 
                }
                return Ok(states);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteStates(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await statesRepository.DeleteStates(Id);
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

	  [HttpGet("~/GetStatesLookup")]
        public async Task<IActionResult> GetStatesLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var statesList = await statesRepository.GetStatesLookup();
                _logger.LogInformation($"database call done successfully with {statesList?.Count()}");
                return Ok(statesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchStates")]
        public async Task<IActionResult> SearchStates(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var statesList = statesRepository.SearchStates(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {statesList?.Count()}");
                return Ok(statesList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
