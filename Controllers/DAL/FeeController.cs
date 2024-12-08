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
    public class FeeController : Controller
    {
        private readonly IFeeRepository feeRepository;
        private readonly IMapper mapper;
        private readonly ILogger<FeeController> _logger;
	private IUtilityHelper utilityHelper;
        public FeeController(IFeeRepository feeRepository, IMapper mapper, ILogger<FeeController> logger,
	IUtilityHelper utilityHelper)
        {
            this.feeRepository = feeRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllFee")]
        public async Task<IActionResult> GetAllFee()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var feeList = await feeRepository.GetAllFee();
                _logger.LogInformation($"database call done successfully with {feeList?.Count()}");
                return Ok(feeList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetFeeById")]
        public async Task<IActionResult> GetFeeById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var feeList = await feeRepository.GetFeeById(Id);
                _logger.LogInformation($"database call done successfully with {feeList?.Id}");
                if (feeList == null)
                {
                    return NotFound();
                }
                return Ok(feeList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddFee")]
        public async Task<IActionResult> CreateFee(Fee FeeDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var fee = new Fee()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var feeDTO = await feeRepository.CreateFee(FeeDetails);
                _logger.LogInformation($"database call done successfully with {feeDTO?.Id}");
                return Ok(feeDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateFee([FromRoute] int Id, [FromBody] Fee updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Fee fee = await feeRepository.UpdateFee(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {fee}");
                if (fee == null) 
                { 
                    return NotFound(); 
                }
                return Ok(fee);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateFeeStatus/{Id:int}")]
        public async Task<IActionResult> UpdateFeeStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Fee fee = await feeRepository.UpdateFeeStatus(Id);
                _logger.LogInformation($"database call done successfully with {fee}");
                if (fee == null) 
                { 
                    return NotFound(); 
                }
                return Ok(fee);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteFee(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await feeRepository.DeleteFee(Id);
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

	  [HttpGet("~/GetFeeLookup")]
        public async Task<IActionResult> GetFeeLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var feeList = await feeRepository.GetFeeLookup();
                _logger.LogInformation($"database call done successfully with {feeList?.Count()}");
                return Ok(feeList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchFee")]
        public async Task<IActionResult> SearchFee(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var feeList = feeRepository.SearchFee(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {feeList?.Count()}");
                return Ok(feeList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
