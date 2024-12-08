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
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;
        private readonly ILogger<DepartmentController> _logger;
	private IUtilityHelper utilityHelper;
        public DepartmentController(IDepartmentRepository departmentRepository, IMapper mapper, ILogger<DepartmentController> logger,
	IUtilityHelper utilityHelper)
        {
            this.departmentRepository = departmentRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllDepartment")]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var departmentList = await departmentRepository.GetAllDepartment();
                _logger.LogInformation($"database call done successfully with {departmentList?.Count()}");
                return Ok(departmentList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetDepartmentById")]
        public async Task<IActionResult> GetDepartmentById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var departmentList = await departmentRepository.GetDepartmentById(Id);
                _logger.LogInformation($"database call done successfully with {departmentList?.Id}");
                if (departmentList == null)
                {
                    return NotFound();
                }
                return Ok(departmentList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddDepartment")]
        public async Task<IActionResult> CreateDepartment(Department DepartmentDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var department = new Department()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var departmentDTO = await departmentRepository.CreateDepartment(DepartmentDetails);
                _logger.LogInformation($"database call done successfully with {departmentDTO?.Id}");
                return Ok(departmentDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateDepartment([FromRoute] int Id, [FromBody] Department updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Department department = await departmentRepository.UpdateDepartment(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {department}");
                if (department == null) 
                { 
                    return NotFound(); 
                }
                return Ok(department);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateDepartmentStatus/{Id:int}")]
        public async Task<IActionResult> UpdateDepartmentStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Department department = await departmentRepository.UpdateDepartmentStatus(Id);
                _logger.LogInformation($"database call done successfully with {department}");
                if (department == null) 
                { 
                    return NotFound(); 
                }
                return Ok(department);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await departmentRepository.DeleteDepartment(Id);
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

	  [HttpGet("~/GetDepartmentLookup")]
        public async Task<IActionResult> GetDepartmentLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var departmentList = await departmentRepository.GetDepartmentLookup();
                _logger.LogInformation($"database call done successfully with {departmentList?.Count()}");
                return Ok(departmentList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchDepartment")]
        public async Task<IActionResult> SearchDepartment(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var departmentList = departmentRepository.SearchDepartment(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {departmentList?.Count()}");
                return Ok(departmentList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
