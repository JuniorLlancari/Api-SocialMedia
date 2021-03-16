using SocialMedia.Core.Entities;
using SocialMedia.Core.Exceptions;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialMedia.Core.QueryFilters;
using SocialMedia.Core.CustomEntities;

namespace SocialMedia.Core.Services
{


    public class PostService : IPostService
    {

        private readonly IUnitOfWork _unitOfWork;

        //antes 02
        /*
        private readonly IRepository<Post> _postRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Comment> _commentRepository;*/

        //antes 01
        // private readonly IPostRepository _postRepository;
        // private readonly IUserRepository _userRepository;


        public PostService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Post> GetPost(int id)
        {
            return await _unitOfWork.PostRepository.GetById(id);
        }

        public PagedList<Post> GetPosts(PostQueryFilter filter)
        {
            var posts= _unitOfWork.PostRepository.GetAll();
            if (filter.UserId != null)
            {
                posts = posts.Where(x => x.UserId == filter.UserId); 
            }
            if (filter.Date != null)
            {
                posts = posts.Where(x => x.Date.ToShortDateString() == filter.Date?.ToShortDateString());
            }
            if (filter.Description != null)
            {
                posts = posts.Where(x => x.Description.ToLower().Contains(filter.Description.ToLower()));
            }

            var pagePost = PagedList<Post>.Create(posts, filter.PageNumber, filter.PageSize);

            return pagePost;

        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);

            if (user == null)
            {
                throw new BusinessException("User does not exist");
            }
            var userPost = await _unitOfWork.PostRepository.GetPostsByUser(post.UserId);
            if (userPost.Count() < 7)
            {
                var lastPost = userPost.OrderByDescending(x=>x.Date).FirstOrDefault();
                if ((DateTime.Now- lastPost.Date).TotalDays < 7)
                {
                    throw new BusinessException("You are not able to publish");
                }
            }

            if (post.Description.Contains("Sexo")){
                throw new BusinessException("Content not allowed");
            }


            await _unitOfWork.PostRepository.Add(post);
            await _unitOfWork.SaveChangeAsync();
        }
        public async Task<bool> DeletePost(int id)
        {
              await _unitOfWork.PostRepository.Delete(id);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }

        public async Task<bool> UpdatePost(Post post)
        {
              _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangeAsync();
            return true;
        }
    }
}
