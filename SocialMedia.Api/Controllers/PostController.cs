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
using SocialMedia.Infrastructure.DTOs;

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
            var postsDto = posts.Select(x => new PostDto // pasamos A DTOS
            {
                PostId = x.PostId,
                Date = x.Date,
                Description = x.Description,
                Image = x.Image,
                UserId = x.UserId

            });
                    
            return Ok(postsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var posts = await _postRepository.GetPost(id);
            var postsDto =  new PostDto // pasamos A DTOS
            {
                PostId = posts.PostId,
                Date = posts.Date,
                Description = posts.Description,
                Image = posts.Image,
                UserId = posts.UserId

            };
            return Ok(postsDto);
        }


        [HttpPost]//Post
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = new Post
            {
                Date = postDto.Date,
                Description = postDto.Description,
                Image = postDto.Image,
                UserId = postDto.UserId
               
            };

             await _postRepository.insertPost(post);
            return Ok(post);
        }

    }
}
