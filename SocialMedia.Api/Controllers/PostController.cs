using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocialMedia.Api;
using SocialMedia.Infrastructure.Repositories;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Entities;

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

        /*
        [HttpGet]
        public  async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            return Ok(posts);
        }*/


        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postRepository.GetPosts();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var posts = await _postRepository.GetPost(id);
            return Ok(posts);
        }


        [HttpPost]
        public async Task<IActionResult> Post(Post obj)
        {
             await _postRepository.insertPost(obj);
            return Ok(obj);
        }

    }
}
