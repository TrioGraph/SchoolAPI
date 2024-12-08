using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public interface IAuthenticateRepository
    {
        Task<UserProfile> ValidateCredentials(string userName, string password);
        Task<string[]> GetRole_PrivilegesByRole(int? roleId);
        Task<object> GetLookupRecentUpdates();
    }
}