using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Cookies"),
                new Claim("summer", "Cookie")
            };
            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;
            var signingCredentials = new SigningCredentials(key,algorithm);

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audience,
                Claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(5),
                signingCredentials);

            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new { access_token = tokenJson });
        }

    }
}