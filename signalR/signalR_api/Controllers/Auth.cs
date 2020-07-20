using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using signalR_api.Models;

namespace signalR_api.Controllers
{
    [ApiController]
    public class Auth : ControllerBase
    {
        private string GetClaimByName(string typeName)
        {
            var contextUser= HttpContext.Request.Headers["Authorization"].ToString();
            var sub = contextUser.Substring(7);
            var tokenHandler = new JwtSecurityTokenHandler();
            var readedToken = tokenHandler.ReadJwtToken(sub);
            var tokClaims = readedToken.Payload.Claims.Where(c => c.Type == typeName);

            var result = "";

            foreach (var item in tokClaims)
            {
                result += item;
            }
            return result;
        }
        
        private readonly IConfiguration _configuration;

        public Auth(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        
        [Route("[controller]/[action]")]
        public IActionResult TokenGiver(string userId, Device deviceRole)
        {
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JWTKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return basic user info and authentication token
            
            var conf = _configuration["JWTKey"];
            var result = deviceRole;
            return Ok(tokenString);
        }

        [Authorize(Roles = "Pos")]
        [HttpPost]
        [Route("[controller]/[action]")]
        public async Task<ActionResult> LinkGenerator()
        {
            //var role = GetClaimByName("role");
            // var result = contextUser.HasClaim(c => c.Type == "Role");
         return Ok();
        }
    }
    
    
}

