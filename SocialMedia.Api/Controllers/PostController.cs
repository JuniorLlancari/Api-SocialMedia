using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia.Api;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Core.Interfaces;
namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok(_postRepository.GetPosts());
        }





    }
}
