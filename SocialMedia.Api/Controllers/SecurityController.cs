using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    [Authorize(Roles =nameof(RoleType.Administrator))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityServices _securityServices;
        private readonly IMapper _mapper;


        public SecurityController(ISecurityServices securityServices, IMapper mapper)
        {
            _securityServices = securityServices;
            _mapper = mapper;
        }



        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<SecurityDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpPost]//Post
        public async Task<IActionResult> Post(SecurityDto securityDto)
        {
            var security = _mapper.Map<Security>(securityDto);
            await _securityServices.RegisterUser(security);
            //RECUPERAMOS EL STRING 
            securityDto = _mapper.Map<SecurityDto>(security);
            var response = new ApiResponse<SecurityDto>(securityDto);
            return Ok(response);
        }

    }
}
