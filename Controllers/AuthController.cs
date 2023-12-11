using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NSTask.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NSTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public IActionResult Post([FromBody] Login login)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("The Model is not Valid");
            }
            if (login.UserName.ToLower() != "mehdi" || login.Password.ToLower() != "1")
            {
                return Unauthorized();
            }
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("OurVerifyTopLearn"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOption = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                claims: new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,login.UserName),
                    new Claim(ClaimTypes.Role,"Admin")
                },
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials
                );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOption);

            return Ok(new { token = tokenString });

        }
    }
}

