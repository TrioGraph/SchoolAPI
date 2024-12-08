using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models.Data;
using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SchoolAPI.Repositories
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly SchoolDbContext schoolDbContext;

        public AuthenticateRepository(SchoolDbContext SchoolDbContext)
        {
            this.schoolDbContext = SchoolDbContext;
        }
        public async Task<UserProfile> ValidateCredentials(string userName, string password)
        {
            Student student = new Student();
            Teacher teacher = new Teacher();
            Users user = schoolDbContext.Users.FirstOrDefault(x => x.UserName.Equals(userName) && x.Password.Equals(password));
            if (user.UserType == 3)
            {
                student = schoolDbContext.Student.FirstOrDefault(s => s.UserId == user.Id);
            }
            else
            {
               teacher = schoolDbContext.Teacher.FirstOrDefault(s => s.UserId == user.Id);
            }
            return new UserProfile()
            {
                User= user,
                Student = student,
                Teacher = teacher
            };
        }

        public async Task<string[]> GetRole_PrivilegesByRole(int? roleId)
        {
            string[] result = string.Join(",", schoolDbContext.UserType_Privileges
           .Where(a => a.UserTypeId == roleId)
           .Select(p => p.Privilege_Id.ToString())).Split(',');

            return result;

        }

        public async Task<object> GetLookupRecentUpdates()
        {
            SqlConnection con = new SqlConnection(this.schoolDbContext.Database.GetDbConnection().ConnectionString);
            try
            {
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("LookupTablesRecentUpdates", con);
                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataset);
                if (dataset.Tables.Count > 0)
                {
                    var results = dataset.Tables[0].AsEnumerable().
                                      Select(row => new
                                      {
                                          LookupTableName = row.Field<int>("LookupTableName"),
                                          MaxDate = row.Field<string>("MaxDate"),
                                      });
                    return results;
                }
                else
                {
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                // _logger.LogError(ex, ex.ToString());
                throw;
            }
            finally
            {
                con.Close();
            }
        }

    }
}