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
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly IMapper mapper;
        private readonly ILogger<StudentController> _logger;
	private IUtilityHelper utilityHelper;
        public StudentController(IStudentRepository studentRepository, IMapper mapper, ILogger<StudentController> logger,
	IUtilityHelper utilityHelper)
        {
            this.studentRepository = studentRepository;
            mapper = mapper;
            _logger = logger;
	    this.utilityHelper = utilityHelper;
        }

        [HttpGet("~/GetAllStudent")]
        public async Task<IActionResult> GetAllStudent()
        {
            try
            {
                _logger.LogInformation($"Start ");
                var studentList = await studentRepository.GetAllStudent();
                _logger.LogInformation($"database call done successfully with {studentList?.Count()}");
                return Ok(studentList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet]
        [ActionName("GetStudentById")]
        public async Task<IActionResult> GetStudentById(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var studentList = await studentRepository.GetStudentById(Id);
                _logger.LogInformation($"database call done successfully with {studentList?.Id}");
                if (studentList == null)
                {
                    return NotFound();
                }
                return Ok(studentList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpPost("~/AddStudent")]
        public async Task<IActionResult> CreateStudent(Student StudentDetails)
        {
            try
            {
                _logger.LogInformation($"Start ");
                // var student = new Student()
                // {
                    // Name = addAuthRolesRequest.Name,
                    // ApplicationId = addAuthRolesRequest.ApplicationId,
                    // Status = addAuthRolesRequest.Status
                // };
                var studentDTO = await studentRepository.CreateStudent(StudentDetails);
                _logger.LogInformation($"database call done successfully with {studentDTO?.Id}");
                return Ok(studentDTO);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

	  [HttpPut]
        [Route("{Id:int}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] int Id, [FromBody] Student updateRequest)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Student student = await studentRepository.UpdateStudent(Id, updateRequest);
                _logger.LogInformation($"database call done successfully with {student}");
                if (student == null) 
                { 
                    return NotFound(); 
                }
                return Ok(student);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }


        [HttpPut]
        [Route("UpdateStudentStatus/{Id:int}")]
        public async Task<IActionResult> UpdateStudentStatus([FromRoute] int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                Student student = await studentRepository.UpdateStudentStatus(Id);
                _logger.LogInformation($"database call done successfully with {student}");
                if (student == null) 
                { 
                    return NotFound(); 
                }
                return Ok(student);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpDelete]
        [Route("{Id:int}")]
        public async Task<IActionResult> DeleteStudent(int Id)
        {
            try
            {
                _logger.LogInformation($"Start with {Id}");
                var deletedItem = await studentRepository.DeleteStudent(Id);
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

	  [HttpGet("~/GetStudentLookup")]
        public async Task<IActionResult> GetStudentLookup()
        {
            try
            {
                _logger.LogInformation($"Start");
                var studentList = await studentRepository.GetStudentLookup();
                _logger.LogInformation($"database call done successfully with {studentList?.Count()}");
                return Ok(studentList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }

        [HttpGet("~/SearchStudent")]
        public async Task<IActionResult> SearchStudent(string searchText = "null", int pageNumber = 1, int pageSize = 10, string sortColumn = "Id", string sortOrder = "DESC",
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
                var studentList = studentRepository.SearchStudent(int.Parse(userId),searchText, pageNumber, pageSize, sortColumn, sortOrder,
                        isColumnSearch, columnDataType, operatorType, value1, value2);
                _logger.LogInformation($"database call done successfully with {studentList?.Count()}");
                return Ok(studentList);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, ex.ToString());
                throw;
            }
        }
     
    }
}
