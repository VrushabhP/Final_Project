using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Server.Controllers
{
    public class OAuthController : Controller
    {

        [HttpGet]
        public IActionResult Authorize(
            string respose_type,
            string cliennt_id,
            string redirect_uri,
            string scope,
            string state
            )
        {
            var query = new QueryBuilder();
            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);

            return View(model: query.ToString());
        }

        [HttpPost]
        public IActionResult Authorize(string username,
             string redirectUri, string state
             )
        {
            const string code = "ABCDEFGIJKLMNOPQRSTVWXYZ";

            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);
            return Redirect($"{redirectUri}{query.ToString()}");
        }

        public async Task<IActionResult> Token(
            string grant_type,
            string code,
            string redirect_uri,
            string client_id,
            string refresh_token)
        {
            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);
            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
              new Claim("granny", "cookie")
            };

            var token = new JwtSecurityToken(
                Constants.Issuer,
                Constants.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: grant_type == "refresh_token" ?
                DateTime.Now.AddMinutes(5) :
                DateTime.Now.AddMilliseconds(1),
                signingCredentials
                );
            var access_token = new JwtSecurityTokenHandler().WriteToken(token);

            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "OAuthTutorial",
                refresh_token = "refresTokenSampleValueSomething77"
            };
            var responseJSON = JsonConvert.SerializeObject(responseObject);
            var responseBytes = Encoding.UTF8.GetBytes(responseJSON);

            await Response.Body.WriteAsync(responseBytes, 0, responseBytes.Length);

            return Redirect(redirect_uri);
        }
        [Authorize]
        public IActionResult Validate()
        {
            if (HttpContext.Request.Query.TryGetValue("access_token", out var accessToken))
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
