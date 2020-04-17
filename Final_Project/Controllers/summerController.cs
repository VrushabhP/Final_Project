﻿using Final_Project;
using Final_Project.Data;using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using Microsoft.IdentityModel.Tokens;using System;using System.Collections.Generic;using System.IdentityModel.Tokens.Jwt;using System.Linq;using System.Security.Claims;using System.Text;using System.Threading.Tasks;namespace summer.Controllers{    [Route("api/[controller]")]    [ApiController]    [Authorize]    public class summerController : ControllerBase    {        private readonly AppDbContext _context;        public summerController(AppDbContext context)        {            _context = context;        }

        // GET: api/summer
        [HttpGet]        public async Task<ActionResult<IEnumerable<Summer>>> Getsummer()        {            List<Summer> summer = await _context.summer.ToListAsync();            if (summer == null)            {                return new List<Summer>();            }            else            {                return summer;            }        }

        // GET: api/summer/Pass_ID
        [HttpGet("{id}")]        public async Task<ActionResult<Summer>> Getsummer(float id)        {            var summer = await _context.summer.FindAsync(id);            if (summer == null)            {                return NotFound();            }            return summer;        }

        public IActionResult Authenticate()        {            var Claims = new[]            {                new Claim(JwtRegisteredClaimNames.Sub, "un1"),                new Claim("summer", "Cookie")            };            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);            var key = new SymmetricSecurityKey(secretBytes);            var algorithm = SecurityAlgorithms.HmacSha256;            var signingCredentials = new SigningCredentials(key, algorithm);            var token = new JwtSecurityToken(                Constants.Issuer,                Constants.Audience,                Claims,                notBefore: DateTime.Now,                expires: DateTime.Now.AddHours(1),                signingCredentials);            var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);            return Ok(new { access_token = tokenJson });        }    }}