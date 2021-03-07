using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SocialMedia.Infrastructure.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialMedia.Infrastructure.Repositories
{
    public class PostRepository :IPostRepository
    {
        private readonly SocialMediaContext _context;
        public PostRepository(SocialMediaContext context) 
        {
            _context = context;
        }


        public async Task<Post> GetPost(int id)
        {

            var caja = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == id);
            return caja;

        }

        public async Task<IEnumerable<Post>> GetPosts()
        {
            var caja = await _context.Posts.ToArrayAsync();
            return caja;
        }
        public async Task InsertPost(Post obj)
        {
            _context.Posts.Add(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePost(Post post)
        {
            var currentPost = await GetPost(post.PostId);
            currentPost.Date = post.Date;
            currentPost.Description = post.Description;
            currentPost.Image = post.Image;

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }

        public async Task<bool> DeletePost(int id)
        {
            var currentPost = await GetPost(id);
            _context.Posts.Remove(currentPost);

            int rows = await _context.SaveChangesAsync();
            return rows > 0;
        }
    }

    
}
