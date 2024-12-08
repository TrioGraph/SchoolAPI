namespace SchoolAPI.Models
{
    public class UserProfile
    {
        public Users User { get; set; } 
        public Teacher Teacher { get; set; }
        public Student Student { get; set; }
    }
}
