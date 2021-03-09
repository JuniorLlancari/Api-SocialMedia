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
 using AutoMapper;
using SocialMedia.Api.Response;
using SocialMedia.Core.DTOs;

namespace SocialMedia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        //  private readonly IPostRepository _postRepository;
        private readonly IPostService _postService;

        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        /*
        [HttpGet]
        public  async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();
            return Ok(posts);
        }*/


        [HttpGet]
        public IActionResult GetPosts()
        {
            // DE PostDto => Post
            var posts = _postService.GetPosts();
            var postsDto = _mapper.Map<IEnumerable<PostDto>>(posts);


            var response = new ApiResponse<IEnumerable< PostDto>>(postsDto);
            return Ok(response);



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

         }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var posts = await _postService.GetPost(id);
            var postDto = _mapper.Map<PostDto>(posts);

             var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }


        [HttpPost]//Post
        public async Task<IActionResult> Post(PostDto postDto)
        {
            var post = _mapper.Map<Post>(postDto);      
            await _postService.InsertPost(post);
            //RECUPERAMOS EL STRING 
            postDto= _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(postDto);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,PostDto postDto)
        {           
            var post = _mapper.Map<Post>(postDto);
            post.Id = id;
            var result=await _postService.UpdatePost(post);
            var Dto = _mapper.Map<PostDto>(post);
            var response = new ApiResponse<PostDto>(Dto);
            return Ok(response);            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Put(int id)
        {
             
            var result=await _postService.DeletePost(id);
            var response = new ApiResponse<bool>(result);
            return Ok(response);
        }



    }
}
