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
    public class GendersController : Controller
    {
        private readonly IGendersRepository gendersRepository;
        private readonly IMapper mapper;
        private readonly ILogger<GendersController> _logger;
	private IUtilityHelper utilityHelper;
        public GendersController(IGendersRepository gendersRepository, IMapper mapper, ILogger<GendersController> logger,
	IUtilityHelper utilityHelper)
        {
            this.gendersRepository = gendersRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllGenders")]
        public async Task<IActionResult> GetAllGenders()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var gendersList = await gendersRepository.GetAllGenders();
                _logger.LogInformation($"database call done successfully with {gendersList?.Count()}");
                return Ok(gendersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetGendersById")]
        public async Task<IActionResult> GetGendersById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var gendersList = await gendersRepository.GetGendersById(Id);
                _logger.LogInformation($"database call done successfully with {gendersList?.Id}");
                if (gendersList == null)
                {
                    return NotFound();
                }
                return Ok(gendersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddGenders")]
        public async Task<IActionResult> CreateGenders(Genders GendersDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var genders = new Genders()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var gendersDTO = await gendersRepository.CreateGenders(GendersDetails);
                _logger.LogInformation($"database call done successfully with {gendersDTO?.Id}");
                return Ok(gendersDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateGenders([FromRoute] int Id, [FromBody] Genders updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Genders genders = await gendersRepository.UpdateGenders(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {genders}");
                if (genders == null) 
                { 
                    return NotFound(); 
                }
                return Ok(genders);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateGendersStatus/{Id:int}")]
        public async Task<IActionResult> UpdateGendersStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Genders genders = await gendersRepository.UpdateGendersStatus(Id);
                _logger.LogInformation($"database call done successfully with {genders}");
                if (genders == null) 
                { 
                    return NotFound(); 
                }
                return Ok(genders);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteGenders(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await gendersRepository.DeleteGenders(Id);
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

	  [HttpGet("~/GetGendersLookup")]
        public async Task<IActionResult> GetGendersLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var gendersList = await gendersRepository.GetGendersLookup();
                _logger.LogInformation($"database call done successfully with {gendersList?.Count()}");
                return Ok(gendersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchGenders")]
        public async Task<IActionResult> SearchGenders(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var gendersList = gendersRepository.SearchGenders(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {gendersList?.Count()}");
                return Ok(gendersList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
