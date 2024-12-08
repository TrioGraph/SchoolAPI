namespace SchoolAPI.Models
{
    public class Course
    {
        public int? Id { get; set; }
	  public string? CourseName { get; set; }
	  public string? CourseCode { get; set; }
	  public string? CourseDetails { get; set; }
	  public string? StartDate { get; set; }
	  public string? CourseTimeLength { get; set; }
	  public int? CoursePrice { get; set; }
	  public int? ProfessorId { get; set; }
	  public int? MaxStudentLength { get; set; }
	  public string? ContactNumber { get; set; }
	  public string? AttachmentFile { get; set; }
	  public bool? Active { get; set; }
	  

	  
    }
}
