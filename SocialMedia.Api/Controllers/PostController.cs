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
using AutoMapper;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
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
            // DE PostDto => Post
            var posts = await _postRepository.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);
           /* 
            var postsDto = posts.Select(x => new PostDto // pasamos A DTOS
            {
                PostId = x.PostId,
                Date = x.Date,
                Description = x.Description,
                Image = x.Image,
                UserId = x.UserId

            });
           */

            return Ok(postsDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var posts = await _postRepository.GetPost(id);
            var postsDto = _mapper.Map<PostDto>(posts);

            return Ok(postsDto);
        }


        [HttpPost]//Post
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);
             
            await _postRepository.insertPost(post);
            return Ok(post);
        }

    }
}
