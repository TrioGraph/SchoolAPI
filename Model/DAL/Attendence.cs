namespace SchoolAPI.Models
{
    public class Attendence
    {
        public int? Id { get; set; }
	  public int? StdEmpId { get; set; }
	  public DateTime? Date { get; set; }
	  public bool? IsPresent { get; set; }
	  public bool? Active { get; set; }
	  

	  
    }
}
