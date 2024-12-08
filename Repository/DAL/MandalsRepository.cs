using System.Data;
using Microsoft.Data.SqlClient;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Models.Data;
using SchoolAPI.Models;
using SchoolAPI.Models.Lookup;

namespace SchoolAPI.Repositories
{
    public class MandalsRepository : IMandalsRepository
    {
        private readonly SchoolDbContext SchoolDbContext;

        public MandalsRepository(SchoolDbContext SchoolDbContext)
        {
            this.SchoolDbContext = SchoolDbContext;
        }

        public async Task<List<Mandals>> GetAllMandals()
        {
            return await SchoolDbContext.Mandals.OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<IEnumerable<object>> GetMandalsLookup()
        {
            return await SchoolDbContext.Mandals
            .Select(s => new
            {
                Id = s.Id,
                Name = s.Name
            }).OrderByDescending(d => d.Id).ToListAsync();
        }

        public async Task<Mandals> GetMandalsById(int id)
        {
            return await SchoolDbContext.Mandals.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Mandals> CreateMandals(Mandals mandals)
        {
            mandals.Id = GetNextId();
            await SchoolDbContext.AddRangeAsync(mandals);
            await SchoolDbContext.SaveChangesAsync();

            return this.SchoolDbContext.Mandals.First(a => a.Id == mandals.Id);
        }

        public async Task<Mandals> UpdateMandals(int Id, Mandals mandals)
        {
            var mandalsDetails = await SchoolDbContext.Mandals.FirstOrDefaultAsync(x => x.Id == Id);
            if (mandalsDetails == null)
            {
                return null;
            }
            System.Reflection.PropertyInfo[] propertiesInfo = mandalsDetails.GetType().GetProperties();
            foreach(System.Reflection.PropertyInfo propInfo in propertiesInfo)
            {
                propInfo.SetValue(mandalsDetails, mandals.GetType().GetProperty(propInfo.Name).GetValue(mandals));
            }
            mandalsDetails.Id = Id;
            await SchoolDbContext.SaveChangesAsync();
            return this.SchoolDbContext.Mandals.First(a => a.Id == mandalsDetails.Id);
        }

	  public async Task<IEnumerable<Mandals>> DeleteMandals(int id)
      {
            var list = await SchoolDbContext.Mandals.Where(s => id == s.Id).ToListAsync();
            SchoolDbContext.Mandals.RemoveRange(list);
            await SchoolDbContext.SaveChangesAsync();
            return list;
      }

	public async Task<Mandals> UpdateMandalsStatus(int Id)
        {
            var mandalsDetails = await SchoolDbContext.Mandals.FirstOrDefaultAsync(x => x.Id == Id);
            if (mandalsDetails == null)
            {
                return null;
            }
            mandalsDetails.Active = false;
            await SchoolDbContext.SaveChangesAsync();
            return this.SchoolDbContext.Mandals.First(a => a.Id == mandalsDetails.Id);
        }

      private int GetNextId()
      {
        int? maxId = SchoolDbContext.Mandals.Max(p => p.Id);
        if (maxId == null)
        {
            maxId = 0;
        }
        return ((int)maxId + 3);
      }

public Dictionary<string, object> SearchMandals(int userId, string searchString, int pageNumber, int pageSize, string sortColumn, string sortDirection, bool isColumnSearch = false, string columnName = "", string columnDataType = "", string operatorType = "", string value1 = "", string value2 = "")
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {
                // string ConnectionString = "Server=162.215.230.14;Database=a1685bx6_farm;User Id=lboils;Password=lboils_user;TrustServerCertificate=True";
                string ConnectionString = this.SchoolDbContext.Database.GetDbConnection().ConnectionString;
                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetSearchMandals", new SqlConnection(ConnectionString));
		adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@userId", SqlDbType = SqlDbType.VarChar, Value = userId, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@txt", SqlDbType = SqlDbType.VarChar, Value = searchString, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@pageIndex", SqlDbType = SqlDbType.VarChar, Value = pageNumber, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@pageSize", SqlDbType = SqlDbType.VarChar, Value = pageSize, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@sortColumn", SqlDbType = SqlDbType.VarChar, Value = sortColumn, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@sortDirection", SqlDbType = SqlDbType.VarChar, Value = sortDirection, Direction = ParameterDirection.Input });
		adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@isColumnSearch", SqlDbType = SqlDbType.Bit, Value = isColumnSearch, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@columnName", SqlDbType = SqlDbType.VarChar, Value = columnName, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@columnDataType", SqlDbType = SqlDbType.VarChar, Value = columnDataType, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@operator", SqlDbType = SqlDbType.VarChar, Value = operatorType, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@value1", SqlDbType = SqlDbType.VarChar, Value = value1, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@value2", SqlDbType = SqlDbType.VarChar, Value = value2, Direction = ParameterDirection.Input });

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataset);
                Dictionary<string, string> tempRecord = new Dictionary<string, string>();
                List<object> record = new List<object>();
                string propertyName = "";
                // record = new KeyValuePair<string, string>();
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    tempRecord = new Dictionary<string, string>();
                    var obj1 = new ExpandoObject();
                    foreach (DataColumn dc in dataset.Tables[0].Columns)
                    {
                        propertyName = dc.ColumnName;
                        tempRecord.Add(dc.ColumnName, dr[dc.ColumnName] == null ? "" : Convert.ToString(dr[dc.ColumnName]));
                    }
                    record.Add(tempRecord);
                }
                result.Add("data", record);
                result.Add("TotalRecordsCount", dataset.Tables[1].Rows[0]["TotalRowCount"]);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception Occurred: {ex.Message}");
            }

            return result;
        }
}
}