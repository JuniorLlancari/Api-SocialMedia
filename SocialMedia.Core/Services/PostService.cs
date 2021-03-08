using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

        public async  Task<IEnumerable<Post>> GetPosts()
        {
            return await _unitOfWork.PostRepository.GetAll();
        }

        public async Task InsertPost(Post post)
        {
            var user = await _unitOfWork.UserRepository.GetById(post.UserId);

            if (user == null)
            {
                throw new Exception("User does not exist");
            }

            if (post.Description.Contains("Sexo")){
                throw new Exception("Content not allowed");
            }


            await _unitOfWork.PostRepository.Add(post);
        }
        public async Task<bool> DeletePost(int id)
        {
              await _unitOfWork.PostRepository.Delete(id);
            return true;
        }

        public async Task<bool> UpdatePost(Post post)
        {
             await _unitOfWork.PostRepository.Update(post);
            return true;
        }
    }
}
