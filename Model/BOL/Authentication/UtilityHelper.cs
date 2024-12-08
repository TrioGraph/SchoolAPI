using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace SchoolAPI.Models
{
    public class UtilityHelper : IUtilityHelper
    {
        private readonly ILogger<UtilityHelper> _logger;
        public UtilityHelper(ILogger<UtilityHelper> logger)
        {
            _logger = logger;

        }

        public string GetUserFromRequest(HttpRequest request)
        {
            var accessToken = request.Headers["Authorization"];
            // var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");
            if (accessToken != "")
            {
                string[] tokenArray = accessToken.ToString().Split("Bearer ");
                if (tokenArray.Length == 2)
                {
                    var token = accessToken.ToString().Split("Bearer ")[1];
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var validationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("this is my custom Secret key for authentication")),
                        ValidateIssuer = false, // You can set this to true if you want to validate the issuer
                        ValidateAudience = false, // You can set this to true if you want to validate the audience
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero // You can adjust the acceptable clock skew
                    };
                    try
                    {
                        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                        dynamic claimObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(principal.Claims.First().Value);
                        var privilegesList = claimObject.Privileges;
                        string userId = claimObject.Employee["Id"].ToString();
                        return userId;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, ex.ToString());
                    }
                    return null;
                }
            }
            return null;
        }
    }
}