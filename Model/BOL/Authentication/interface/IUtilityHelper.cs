using Microsoft.AspNetCore.Http;

namespace SchoolAPI.Models
{
    public interface IUtilityHelper
    {
        public string GetUserFromRequest(HttpRequest request);
    }
}