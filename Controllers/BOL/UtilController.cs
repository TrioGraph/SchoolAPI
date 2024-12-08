using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Repositories;
using SchoolAPI.Models;
using SchoolAPI.Models.Data;
using SchoolAPI.Models.Lookup;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SchoolAPI.Controllers;
using System.Text;
using System.Linq;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace SchoolAPI.Controllers
{
    [RequestFormLimits(ValueCountLimit = int.MaxValue)]
    [ApiController]
    [Route("[controller]")]
    // [Authorize]
    public class UtilController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SchoolDbContext schoolDbContext;
        private IUtilityHelper utilityHelper;

        private readonly IAuthenticateRepository authenticateRepository;

        public UtilController(IConfiguration configuration, SchoolDbContext SchoolDbContext, IUtilityHelper utilityHelper,
         IAuthenticateRepository authenticateRepository)
        {
            this._configuration = configuration;
            this.schoolDbContext = SchoolDbContext;
            this.utilityHelper = utilityHelper;
            this.authenticateRepository = authenticateRepository;
        }



        [HttpPatch]
        [Route("insertInfo")]
        public string InsertTableByColumns(dynamic info)
        {
            int nextId = 0;
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(info.ToString());
            Dictionary<string, string> insertColumnsList = jsonObj["insertColumnsList"].ToObject<Dictionary<string, string>>();
            string tableName = jsonObj["tableName"];
            int counter = 0;
            if (insertColumnsList != null && tableName != null)
            {
                SqlConnection con = new SqlConnection(this.schoolDbContext.Database.GetDbConnection().ConnectionString);
                con.Open();
                var selectMaxQuery = "SELECT Max(Id) + 1 from [" + tableName + "]";
                SqlCommand command = new SqlCommand(selectMaxQuery, con);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    if (result.ToString().Equals(""))
                    {
                        result = 1;
                    }
                    nextId = int.Parse(result.ToString());
                }

                string userId = utilityHelper.GetUserFromRequest(Request);
                var sql = "INSERT INTO [" + tableName + "] (Id,CreatedBy,CreatedDate,IsActive,";
                var valuesStr = " VALUES (" + nextId + "," + int.Parse(userId) + ", '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "', " + 1 + ", ";

                foreach (var item in insertColumnsList)
                {
                    if (!item.Key.Equals("IsActive", StringComparison.OrdinalIgnoreCase))
                    {
                        if (counter == 0)
                        {
                            sql += "[" + item.Key + "]";
                            valuesStr += "@" + item.Key;
                        }
                        else
                        {
                            sql += ", [" + item.Key + "]";
                            valuesStr += ", @" + item.Key;
                        }
                    }
                    counter++;
                }
                sql += ") " + valuesStr + ")";
                counter = 0;
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                foreach (var item in insertColumnsList)
                {
                    if (!item.Key.Equals("IsActive", StringComparison.OrdinalIgnoreCase))
                    {
                        if (item.Value == null)
                        {
                            command.Parameters.AddWithValue("@" + item.Key, DBNull.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@" + item.Key, item.Value);
                        }
                    }
                }

                command.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (command.ExecuteNonQuery() > 0)
                {
                    con.Close();
                    return "" + nextId;
                }
                else
                {
                    con.Close();
                    return "" + -1;
                }
            }
            return "All Paramerers should be provided and Parameters should not be null";
        }

        [HttpPatch]
        [Route("{updateInfo}")]
        public string UpdateTableByColumns(dynamic info)
        {
            var jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(info.ToString());
            Dictionary<string, string> updateColumnsList = jsonObj["updateColumnsList"].ToObject<Dictionary<string, string>>();
            string tableName = jsonObj["tableName"];
            Dictionary<string, string> primaryKeysList = jsonObj["primaryKeysList"].ToObject<Dictionary<string, string>>();

            string userId = utilityHelper.GetUserFromRequest(Request);
            var sql = "UPDATE [" + tableName + "] SET ";
            sql += "UpdatedBy =" + int.Parse(userId) + ", UpdatedDate = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "'";
            int counter = 0;

            if (updateColumnsList != null && tableName != null && primaryKeysList != null)
            {
                foreach (var item in updateColumnsList)
                {
                    // if (counter == 0)
                    // {
                    //     sql += "[" + item.Key + "] = @" + item.Key;
                    // }
                    // else
                    // {
                    sql += ", [" + item.Key + "] = @" + item.Key;
                    // }
                    counter++;
                }

                // for (int i = 0; i < updateColumnsList.Count; i++)
                // {
                //     if (i == 0)
                //     {
                //         sql += "[" + updateColumnsList[i] + "] = @" + updateColumnsList[i];
                //     }
                //     else
                //     {
                //         sql += ", [" + updateColumnsList[i] + "] = @" + updateColumnsList[i];
                //     }
                // }

                sql += " where ";
                counter = 0;
                foreach (var item in primaryKeysList)
                {
                    if (counter == 0)
                    {
                        sql += "[" + item.Key + "] = @" + item.Key;
                    }
                    else
                    {
                        sql += " AND [" + item.Key + "] = @" + item.Key;
                    }
                    counter++;
                }

                SqlConnection con = new SqlConnection(this.schoolDbContext.Database.GetDbConnection().ConnectionString);
                SqlCommand command = new SqlCommand();
                command.CommandText = sql;
                command.CommandType = CommandType.Text;

                foreach (var item in updateColumnsList)
                {
                    command.Parameters.AddWithValue("@" + item.Key, item.Value);
                }

                foreach (var item in primaryKeysList)
                {
                    command.Parameters.AddWithValue("@" + item.Key, item.Value);
                }
                command.Connection = con;
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                if (command.ExecuteNonQuery() > 0)
                {
                    con.Close();
                    return "" + 0;
                }
                else
                {
                    con.Close();
                    return "" + -1;
                }
            }
            return "All Paramerers should be provided and Parameters should not be null";
        }

        [RequestFormLimits(ValueLengthLimit = int.MaxValue, MultipartBodyLengthLimit = int.MaxValue)]
        [HttpPost, DisableRequestSizeLimit]
        [Route("UploadImage")]
        public async Task<object> UploadImage()
        {
            // try
            // {
            //     IFormCollection formCollection;
            //     formCollection = Request.Form;
            //     var file = formCollection.Files.FirstOrDefault();

            //     var ftpFilePath = _configuration["ConnectionStrings:FilePath"];
            //     var ftpUserName = _configuration["ConnectionStrings:FileUserName"];
            //     var ftpPassword = _configuration["ConnectionStrings:FilePassword"];

            //     Uri uri = new Uri(ftpFilePath);
            //     FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
            //     request.Method = WebRequestMethods.Ftp.UploadFile;
            //     // request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            //     // This example assumes the FTP site uses anonymous logon.
            //     request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
            //     // // Copy the contents of the file to the request stream.
            //     byte[] fileContents;
            //     using (StreamReader sourceStream = new StreamReader(file.OpenReadStream()))
            //     {
            //         fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            //     }

            //     // request.ContentLength = fileContents.Length;
            //     using (Stream requestStream = request.GetRequestStream())
            //     {
            //         // requestStream.Write(file, 0, file.Length);
            //         requestStream.Write(fileContents, 0, fileContents.Length);
            //     }
            //     using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            //     {
            //         Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
            //     }




            //     // using (WebClient client = new WebClient())
            //     // {
            //     //     client.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
            //     //     using (var ftpStream = client.OpenWrite(ftpFilePath))
            //     //     {
            //     //         file.CopyTo(ftpStream);
            //     //     }
            //     // }
            //     return Content("Upload Successful");
            // }
            // catch (Exception ex)
            // {
            //     return Content(ex.Message);
            // }
            #region Old Code



            try
            {
                IFormCollection formCollection;
                formCollection = Request.Form;
                var file = formCollection.Files.FirstOrDefault();
                if (file != null)
                {
                    var folderName = Path.Combine("", "Images");
                    // var folderName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FilePath")["FilePath"];
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                        string[] tempFileName = fileName.Split(".");
                        // tempFileName[0] = GenerateName() + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        tempFileName[0] = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                        fileName = tempFileName[0] + "." + tempFileName[1];
                        var fullPath = Path.Combine(pathToSave, fileName);
                        var dbPath = Path.Combine(folderName, fileName);

                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                        var tableName = formCollection["tableName"].ToString();
                        var tempPrimaryKeysList = formCollection["primaryKeysList"].ToString();
                        var propertyName = formCollection["propertyName"].ToString();

                        dynamic info = new System.Dynamic.ExpandoObject();
                        info.tableName = tableName;
                        info.primaryKeysList = new KeyValuePair<string, string>("primaryKeysList", tempPrimaryKeysList);
                        Dictionary<string, string> tempObj1 = new Dictionary<string, string>();
                        Dictionary<string, string> tempObj2 = new Dictionary<string, string>();
                        tempObj1.Add(propertyName, fileName);
                        tempObj2.Add("Id", tempPrimaryKeysList);
                        info.updateColumnsList = tempObj1;
                        info.primaryKeysList = tempObj2;
                        this.UpdateTableByColumns(Newtonsoft.Json.JsonConvert.SerializeObject(info));
                        return Ok(new { dbPath });
                    }
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }

            return Content("Error Occurred");
            /*
             try
             {

                 // byte[] fileBytes;
                 //     using (var ms = new MemoryStream())
                 //     {
                 //         formFile.CopyTo(ms);
                 //         fileBytes = ms.ToArray();
                 //         // File processing

                 //     }
                 IFormCollection formCollection;
                 formCollection = Request.Form; // sync
                                                // var formCollection = await Request.ReadFormAsync();
                 var file = formCollection.Files.FirstOrDefault();
                 // var folderName = Path.Combine("Resources", "Images");
                 var ftpFilePath = _configuration["ConnectionStrings:FilePath"];
                 var ftpUserName = _configuration["ConnectionStrings:FileUserName"];
                 var ftpPassword = _configuration["ConnectionStrings:FilePassword"];
                 // var folderName = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("FilePath")["FilePath"];
                 // var pathToSave = ftpURL;
                 if (file.Length > 0)
                 {
                     // var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                     // var fullPath = Path.Combine(pathToSave, fileName);
                     // var dbPath = Path.Combine(folderName, fileName);


                     // using (var stream = new FileStream(fullPath, FileMode.Create))
                     // {
                     //     file.CopyTo(stream);
                     // }

                     FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpFilePath);
                     request.Credentials = new NetworkCredential(ftpUserName, ftpPassword);
                     request.Method = WebRequestMethods.Ftp.UploadFile;

                     // using (var stream = new FileStream(fullPath, FileMode.Create))
                     // {
                     //     file.CopyTo(stream);
                     // }

                     using (Stream fileStream = System.IO.File.OpenRead(file));
                     using (Stream ftpStream = request.GetRequestStream())
                     {
                         byte[] buffer = new byte[10240];
                         int read;
                         while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                         {
                             ftpStream.Write(buffer, 0, read);
                             Console.WriteLine("Uploaded {0} bytes", fileStream.Position);
                         }
                     }

                     return Content(dbPath);
                 }
                 return Content("Error occureed during uploading image");
             }
             catch (Exception ex)
             {
                 return Content(ex.Message);
             }
             */
            #endregion
        }

        [HttpGet]
        [Route("GetImageByName")]
        public FileContentResult GetImageByName(string fileName)
        {
            byte[] b = System.IO.File.ReadAllBytes("Images//" + fileName);
            return File(b, "image/jpeg");
        }

        //     public string GenerateName()
        //     {
        //         var random = new Random();
        //         const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //         return new string(Enumerable.Repeat(chars, 10)
        //             .Select(s => s[random.Next(s.Length)]).ToArray());
        //     }



        [HttpGet]
        [Route("GetFarmersLookup")]
        public async Task<IActionResult> GetFarmersLookup(string searchString = "",
        int pageNumber = 1, int pageSize = 50, string sortColumn = "Name", string sortDirection = "ASC")
        {
            SqlConnection con = new SqlConnection(this.schoolDbContext.Database.GetDbConnection().ConnectionString);
            try
            {

                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetFarmersLookup", con);
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@txt", SqlDbType = SqlDbType.VarChar, Value = searchString, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@pageIndex", SqlDbType = SqlDbType.VarChar, Value = pageNumber, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@pageSize", SqlDbType = SqlDbType.VarChar, Value = pageSize, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@sortColumn", SqlDbType = SqlDbType.VarChar, Value = sortColumn, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@sortDirection", SqlDbType = SqlDbType.VarChar, Value = sortDirection, Direction = ParameterDirection.Input });

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataset);
                if (dataset.Tables.Count > 0)
                {
                    var results = dataset.Tables[0].AsEnumerable().
                                      Select(row => new
                                      {
                                          Id = row.Field<int>("Id"),
                                          Farmer_Code = row.Field<string>("Farmer_Code"),
                                          Name = row.Field<string>("Name") + " - " + row.Field<string>("VillageName") + 
                                                " - " + row.Field<string>("MandalName"),
                                          VillageId = row.Field<int>("VillageId"),
                                          VillageName = row.Field<string>("VillageName"),
                                          MandalId = row.Field<int>("MandalId"),
                                          MandalName = row.Field<string>("MandalName"),
                                      });
                    return Ok(results);
                }
                else
                {
                    return Ok("No Data Found");
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

        [HttpGet]
        [Route("GetMandalByVillage")]
        public async Task<IActionResult> GetMandalByVillage(int villageId)
        {
            try
            {
                var results = await schoolDbContext.Villages
                            .Where(d => d.Id == villageId)
                .Select(s => new
                {
                    mandalId = s.MandalId,
                }).ToListAsync();
                return Ok(results);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        [Route("GetVillagesLookup")]
        public async Task<IActionResult> GetVillagesLookup(string searchString = "", int pageNumber = 1, 
        int pageSize = 50, string sortColumn = "Name", string sortDirection = "ASC", string selectedList = "")
        {
            SqlConnection con = new SqlConnection(this.schoolDbContext.Database.GetDbConnection().ConnectionString);
            try
            {

                DataSet dataset = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter();
                //Add the parameter to the Parameters property of SqlCommand object
                adapter.SelectCommand = new SqlCommand("GetVillagesLookup", con);
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@txt", SqlDbType = SqlDbType.VarChar, Value = searchString, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@pageIndex", SqlDbType = SqlDbType.VarChar, Value = pageNumber, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@pageSize", SqlDbType = SqlDbType.VarChar, Value = pageSize, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@sortColumn", SqlDbType = SqlDbType.VarChar, Value = sortColumn, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@sortDirection", SqlDbType = SqlDbType.VarChar, Value = sortDirection, Direction = ParameterDirection.Input });
                adapter.SelectCommand.Parameters.Add(new SqlParameter { ParameterName = "@selectedList", SqlDbType = SqlDbType.VarChar, Value = selectedList, Direction = ParameterDirection.Input });

                adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                adapter.Fill(dataset);
                if (dataset.Tables.Count > 0)
                {
                    var results = dataset.Tables[0].AsEnumerable().
                                      Select(row => new
                                      {
                                          Id = row.Field<int>("VillageId"),
                                          Name = row.Field<string>("VillageName") + " - " + row.Field<string>("MandalName"),
                                          MandalId = row.Field<int>("MandalId"),
                                          MandalName = row.Field<string>("MandalName"),
                                      });
                    return Ok(results);
                }
                else
                {
                    return Ok("No Data Found");
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

        [AllowAnonymous]
        [HttpGet]
        [Route("GetRolePrivilegesByRole")]
        public async Task<string[]> GetRolePrivilegesByRole(int? userTypeId)
        {
                return await authenticateRepository.GetRole_PrivilegesByRole(userTypeId);
        }

         [AllowAnonymous]
        [HttpGet]
        [Route("UpdateRolePrivilegesByRole")]
        public async Task<ActionResult> UpdateRolePrivilegesByRole(string privilegesList,int userTypeId)
        {
            try{
            SqlConnection con = new SqlConnection(this.schoolDbContext.Database.GetDbConnection().ConnectionString);
            con.Open();
             string userId = utilityHelper.GetUserFromRequest(Request);
             SqlCommand cmd = new SqlCommand("UpdateRole_Privileges", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@privilegesList", privilegesList);
            cmd.Parameters.AddWithValue("@userTypeId", userTypeId.ToString());
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex) 
            { 
                // return new HttpResponse(HttpStatusCode.InternalServerError, ex.Message);
                // return new HttpActionResult(HttpStatusCode.InternalServerError, "error message"); // can use any HTTP status code
                return Ok(ex);

            }
            return Ok();
        }
    }
}