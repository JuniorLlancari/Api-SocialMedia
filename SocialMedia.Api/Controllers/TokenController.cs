using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using SocialMedia.Core.Interfaces;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISecurityServices _securityServices; 

        public TokenController(IConfiguration configuration, ISecurityServices securityServices)
        {
            _configuration = configuration;
            _securityServices = securityServices;
        }

        [HttpPost]
        public async Task <IActionResult> Authentication(UserLogin login)
        {
            var isValid = await IsValidUser(login);
            if (isValid.Item1)
            {
                var token = GenerateToken(isValid.Item2);
                return Ok(new { token});
            }
            return NotFound();

        }
        private async Task <(bool,Security)> IsValidUser(UserLogin login)
        {
             var user= await _securityServices.GetLoginByCredentials(login);
            return (user!=null, user);
        }
        private string GenerateToken(Security security)
        {
            //Header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name,security.UserName),
                new Claim("User",security.User),
               new Claim(ClaimTypes.Role,security.Role.ToString())

            };

            //Paylod

            var payload = new JwtPayload
            (
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claims,
                DateTime.Now,
                DateTime.Now.AddMinutes(10)
            );



            var token = new JwtSecurityToken(header, payload);




            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
